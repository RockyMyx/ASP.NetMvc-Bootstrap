/****************************Common*****************************/

//表格隔行变色
jQuery.fn.alternateRowColors = function () {
    $("tbody tr:odd", this).removeClass("even").addClass("odd");
    $("tbody tr:even", this).removeClass("odd").addClass("even");
    return this;
}

//字符串首字母大写
String.prototype.capitalize = function () {
    return this.replace(/^\w/, function (s) {
        return s.toUpperCase();
    });
};

//表单验证
function isValidForm(form) {
    if (form.find('.form-init').length > 0) {
        alert('表单信息不完整，请检查后重新提交！');
        return false;
    }
    else if (form.find('.form-error').length > 0) {
        alert('表单信息输入有误，请检查后重新提交！')
        return false;
    }
    return true;
}

//展现搜索结果
function showSearch(result) {
    if (result.length != 0) {
        $('#js-table').html(result);
        $(".pagination").pagination($('#js-table tbody').find('tr').length);
        $("#js-table tbody tr:gt(" + hideSearch + ")").hide().end();
    }
    else {
        $('#js-table').empty();
        $('.pagination').empty().html('没有相关信息！');
    }
}

function showPagination() {
    if (~ ~dataCount > ~ ~pageSize) {
        initPagination(dataCount);
    }
}

//模态窗口展现事件
$('.modal').on('shown', function (e) {
    var modal = $(this);
    modal.css('margin-top', (modal.outerHeight() / 2) * -1.3)
         .css('margin-left', (modal.outerWidth() / 2) * -1);
    return this;
});

//根据窗口大小调整背景
function resize() {
    $('.content').css('height', $(window).height() + $(window).scrollTop());
}

$(window).on('resize', function () {
    resize();
});

$(document).ready(function () {
    /*************************调整窗口大小，初始化分页按钮*************************/
    resize();
    showPagination();

    /****************************搜索标签提示文字处理*****************************/
    var input = $('input.input-place');
    var placeholder = input.attr('placeholder');
    if ($.browser.msie && !$.support.style) {
        input.val(placeholder);
    }

    input.on('focus', function () {
        $(this).removeClass('italic');
        $(this).attr('placeholder', '');
    });

    input.on('blur', function () {
        if ($(this).val().length == 0) {
            $(this).addClass('italic');
            $(this).attr('placeholder', placeholder);
        }
    });

    /****************************表格排序处理*****************************/
    $("#js-table").each(function () {
        $table = $(this);
        $table.alternateRowColors();
        $("th", $table).each(function (column) {
            var findSortKey;
            if ($(this).is(".sort-alpha")) {
                findSortKey = function ($cell) {
                    return $cell.find(".sort-key").text().toUpperCase() + " " + $cell.text().toUpperCase();
                };
            }
            else if ($(this).is(".sort-numeric")) {
                findSortKey = function ($cell) {
                    var key = parseFloat($cell.text().replace(/^[^\d.]*/, " "));
                    return isNaN(key) ? 0 : key;
                };
            }
            else if ($(this).is(".sort-date")) {
                findSortKey = function ($cell) {
                    return Date.parse('1 ' + $cell.text());
                };
            }

            if (findSortKey) {
                $(this).hover(
				    function () {
				        $(this).addClass("hover");
				    },
				    function () {
				        $(this).removeClass("hover");
				    }
                ).click(function () {
                    var newDirection = 1;
                    if ($(this).is(".sorted-asc")) {
                        newDirection = -1;
                    }
                    var rows = $table.find("tbody > tr").get();
                    $.each(rows, function (index, row) {
                        row.sortKey = findSortKey($(row).children("td").eq(column));
                    });
                    rows.sort(function (a, b) {
                        if (a.sortKey < b.sortKey) return -newDirection;
                        if (a.sortKey > b.sortKey) return newDirection;
                        return 0;
                    });
                    $.each(rows, function (index, row) {
                        $table.children("tbody").append(row);
                        row.sortKey = null;
                    });
                    $table.find("th").removeClass("sorted-asc").removeClass("sorted-desc");
                    var $sortHead = $table.find('th').filter(":nth-child(" + (column + 1) + ")");
                    if (newDirection == 1) {
                        $sortHead.addClass("sorted-asc");
                    }
                    else {
                        $sortHead.addClass("sorted-desc");
                    }
                    $table.find('td').removeClass("sorted").filter(":nth-child(" + (column + 1) + ")").addClass("sorted");
                    $table.alternateRowColors();
                });
            };
        });

        bindTable();
    });
});

//获得当前的Controller名，如/Module/Index，则thisModel为Module
var removeController = location.href.substring(0, location.href.lastIndexOf('/'));
var thisModel = removeController.substring(removeController.lastIndexOf('/') + 1).capitalize();
var checkId, deleteId = [];

function bindTable() {
    var currentTd, input;
    var key, content;
    var beforeEditInfo = [];
    var dataInfo = {};

    $('#js-table').find('th').click(function (e) {
        if ($(this).get(0).className.indexOf('sort') != -1) {
            if (!$(this).hasClass('sort-down')) {
                $(this).removeClass('sort-up')
                   .addClass('sort-down');
            }
            else {
                $(this).removeClass('sort-down')
                   .addClass('sort-up');
            }
        }
    });

    $('#js-table').find('tr').click(function (e) {
        //点击选择框
        if (e.target.type == 'checkbox' && e.target.checked) {
            checkId = $(this).find('td').eq(0).html();
        }
        //点击编辑图标
        if (e.target.className == 'icon-pencil') {
            if ($('.icon-pencil').attr('type') == 'table') {
                var tdLength = $(this).find('td').length;
                var tds = $(this).find('td');
                dataInfo["ID"] = tds.eq(0).html();
                beforeEditInfo.push(tds.eq(0).html());

                //生成编辑信息表单
                for (var i = 1; i < tdLength - 1; i++) {
                    currentTd = tds.eq(i);
                    key = currentTd.attr('key');
                    content = currentTd.attr('content');
                    beforeEditInfo.push(currentTd.html());
                    if (currentTd.attr('class') == 'js-readonly') {
                        input = '<input type="text" value="' + content + '" style="width:150px" readonly />';
                    }
                    else {
                        content = content === undefined ? "" : content;
                        if (dataInfo[key] === undefined) {
                            dataInfo[key] = content;
                        }
                        if (currentTd.attr('class') == 'js-bool') {
                            var shouldChecked = dataInfo[currentTd.attr('key')] == 1 ? "checked" : "";
                            input = '<input type="checkbox" class="edit-check" content="' + currentTd.attr('content') + '" key="' + key + '" ' + shouldChecked + '/>';
                        }
                        else if (currentTd.attr('class') == 'js-check') {
                            input = '<input type="checkbox" disabled />';
                        }
                        else {
                            input = '<input type="text" class="edit-txt" value="' + content + '" style="width:150px" key="' + key + '" validate="' + currentTd.attr('type') + '" />';
                        }
                    }

                    currentTd.html(input);
                }

                //表格行选择CheckBox点击事件
                $('.edit-check').on('click', function () {
                    var ischecked = $(this).is(':checked') ? 1 : 0;
                    dataInfo[$(this).attr('key')] = ischecked;
                    $(this).attr('content', ischecked);
                });

                //编辑框获得焦点事件
                var oldtxt;
                $('.edit-txt').on('focus', function () {
                    oldtxt = $(this).val();
                });

                //编辑框失去焦点事件
                var isCommit = true;
                $('.edit-txt').on('blur', function () {
                    var newtxt;
                    //依赖于myx.validate.js
                    var validateType = $(this).attr('validate');
                    if (mValidate[validateType]) {
                        if (!mValidate[validateType]($(this).val())) {
                            isCommit = false;
                            $('<span style="background-image:url(/Images/error.png);width:16px;height:16px;display:inline-block;margin-left:5px;" title="' + validationTip[validateType] + '"></span>').insertAfter($(this));
                        }
                        else {
                            $(this).next().hide();
                            newtxt = $(this).val();
                            if (newtxt != oldtxt) {
                                dataInfo[$(this).attr('key')] = newtxt;
                            }
                        }
                    }
                    newtxt = $(this).val();
                    if (newtxt != oldtxt) {
                        dataInfo[$(this).attr('key')] = newtxt;
                    }
                });

                //生成编辑按钮
                $(this).find('td').eq(tdLength - 1).html('<input type="button" id="edit-sure" class="btn btn-small btn-primary" value="确定" /><input type="button" id="edit-cancel" class="btn btn-small btn-primary" value="取消" />');

                //取消编辑按钮点击事件
                $('#edit-cancel').on('click', function () {
                    var count = tds.length - 1;
                    for (var i = 0; i < count; i++) {
                        tds.eq(i).html(beforeEditInfo[i]);
                    }
                    resetEditButton();
                });

                //重置按钮
                function resetEditButton() {
                    tds.eq(tds.length - 1).html('<a href="javascript:;"><i class="icon-pencil"></i></a><a href="#ModalDel" role="button" data-toggle="modal"><i class="icon-remove"></i></a>');
                }

                //确定编辑按钮点击事件
                $('#edit-sure').on('click', function () {
                    if (isCommit) {
                        $.ajax({
                            type: 'POST',
                            url: '/' + thisModel + '/Modify',
                            contentType: 'application/json',
                            dataType: 'html',
                            //IE6、IE7默认不支持JSON.stringify，可以引入Scripts/my/json2.js解决此问题
                            data: JSON.stringify(dataInfo),
                            success: function (result) {
                                result = JSON.parse(result);
                                var count = tds.length - 1;
                                var currentContent;
                                for (var i = 0; i < count; i++) {
                                    currentTd = tds.eq(i);
                                    currentContent = result[currentTd.attr('key')]
                                    if (currentContent !== undefined) {
                                        if (currentTd.attr('class') == 'js-bool') {
                                            if (dataInfo[currentTd.attr('key')] == 1) {
                                                currentTd.html('<img src="../../Images/enable.png" />');
                                            }
                                            else {
                                                currentTd.html('<img src="../../Images/disable.png" />');
                                            }
                                        }
                                        else {
                                            currentTd.attr('content', currentContent);
                                            currentTd.html(currentContent);
                                        }
                                    }
                                    else {
                                        currentTd.html(beforeEditInfo[i]);
                                    }
                                }
                                resetEditButton();
                            }
                        });
                    }
                    else {
                        alert("请填写相关信息后提交修改！");
                    }
                });
            }
            if ($('.icon-pencil').attr('type') == 'window') {
                var editForm = $('#js-edit-form');
                editForm.mValidate({ isInline: true, isHideInit: true, tipStyle: 'title' });
                var id = $(this).find('td').eq(0).html();
                $(".js-loading").show();
                FillEditInfo(id);
                $('#ModalEdit').modal('show');
            }
        }
        //表格行删除按钮点击事件
        else if (e.target.className == 'icon-remove') {
            deleteId = [];
            deleteId.push($(this).find('td').eq(0).html());
        }
    });

    /****************************弹出模态窗口*****************************/

    //确定删除按钮点击事件
    $('#js-btn-modal-delete').unbind('click').bind('click', function () {
        $.ajax({
            type: 'POST',
            url: '/' + thisModel + '/Delete',
            contentType: 'application/json',
            dataType: 'html',
            data: JSON.stringify(deleteId),
            success: function () {
                resetPagination(-1);
            }
        });
    });

    //确定编辑按钮点击事件
    $('#js-btn-modal-edit').on('click', function () {
        var form = $('#js-edit-form');
        //myx.page.js
        if (isValidForm(form)) {
            $.post('/' + thisModel + '/Update', form.serialize())
             .done(function () {
                 initPagination(dataCount);
             });
        }
    });

    /****************************工具栏按钮*****************************/

    var isSelectAll = false;

    //表格全选CheckBox点击事件
    $('#js-check-all').on('click', function () {
        isSelectAll = !isSelectAll;
        $('#js-table').find('.js-check-cell').attr('checked', isSelectAll);
    });

    //工具栏删除按钮点击事件
    //unbind为了解决弹出多次confirm的问题
    $('#js-btn-toolbar-delete').unbind('click').bind('click', function (e) {
        var trs = $('.table tbody').find('tr');
        var deletetd, ids = [];
        for (var i = 0; i < trs.length; i++) {
            deletetd = trs.eq(i);
            if (deletetd.find('.js-check-cell').is(':checked')) {
                ids.push(deletetd.find('td').eq(0).html());
            }
        }
        if (ids.length != 0) {
            if (confirm('您确定要删除吗？删除后不可恢复！')) {
                $.ajax({
                    type: 'POST',
                    url: '/' + thisModel + '/Delete',
                    contentType: 'application/json',
                    dataType: 'html',
                    data: JSON.stringify(ids),
                    success: function () {
                        resetPagination(-ids.length);
                    }
                });
            }
        }
        else {
            alert('请选中需要删除的行');
        }
    });
}

//工具栏刷新按钮点击事件
$('#js-btn-toolbar-refresh').on('click', function () {
    $.post('/' + thisModel + '/Index', function (result) {
        $('#js-table').html(result);
        initPagination(dataCount);
    });
});

//工具栏添加按钮点击事件
$('#js-btn-toolbar-add').on('click', function () {
    //重置隐藏控件
    var hideElments = $('#js-add-form').find('.hide');
    for (var i = 0; i < hideElments.length; i++) {
        hideElments[i].style.display = 'none';
    }

    $('#js-add-form').mValidate();
    var scrollHeight = ~ ~$('#js-grid').height() + 'px';
    $('body').css('height', ~ ~$(document).height() + ~ ~$('#js-div-add').height() + 'px');
    $('body').animate({ scrollTop: scrollHeight }, 'fast', function () {
        resize();
    });
    $('#js-div-add').show();
});

//工具栏搜索按钮点击事件
$('#js-btn-search').on('click', function () {
    if ($('#js-input-search').val() == '') {
        alert('请输入搜索的内容');
    }
    else {
        $.post('/' + thisModel + '/Search', { 'name': $('#js-input-search').val() }, function (result) {
            showSearch(result);
        });
    }
});

/****************************添加表单*****************************/

//确定按钮点击事件
$('#js-btn-form-add').on('click', function () {
    var form = $(this).closest('form');
    if (isValidForm(form)) {
        $.post('/' + thisModel + '/Create', form.serialize())
         .done(function () {
             resetPagination(1);
             $('body').animate({ scrollTop: -$('#js-div-add').height() }, 'fast');
         });
    }
});

//取消按钮点击事件
$('#js-btn-form-cancel').on('click', function () {
    $('#js-div-add').hide();
    $('body').css('height', $(window).height());
    $('body').animate({ scrollTop: -$('#js-div-add').height() }, 'fast', function () {
        resize();
    });
});

/****************************分页相关*****************************/

var pageIndex = 0;

function setPageIndex(index) {
    pageIndex = index;
}

function initPagination(count) {
    $(".pagination").pagination(count);
}

function resetTrigger(index) {
    $(".pagination").pagination(dataCount);
    $('#page' + index).trigger('click');
}

function resetPagination() {
    var change = arguments[0] || -1;
    dataCount = ~ ~dataCount + change;
    var newIndex = Math.ceil(dataCount / pageSize) - 1;
    //删除后跳到相应页
    if (change < 0) {
        if (newIndex == 0) {
            $.post('/Role/Index', { pageIndex: 1 })
             .done(function (result) {
                 $('#js-table').html(result);
                 pageIndex = 0;
                 $('.pagination').hide();
             });
        }
        else {
            $('.pagination').show();
            if (newIndex < pageIndex) {
                --pageIndex;
                $('#page' + pageIndex).hide();
                resetTrigger(newIndex);
            }
            else {
                resetTrigger(pageIndex);
            }
        }
    }
    //添加后跳到相应页
    else {
        if (newIndex > pageIndex) {
            ++pageIndex;
            resetTrigger(newIndex);
        }
        else {
            resetTrigger(pageIndex);
        }
    }
}