/****************************Common*****************************/

$(window).on('resize', function () {
    jPage.resize();
});

var jPage = (function ($) {
    var page = {};

    //根据窗口大小调整背景
    page.resize = function () {
        $(".content").css('height', $(window).height() + $(window).scrollTop());
    };

    //判断表单是否合法
    page.isValidForm = function (form) {
        if (form.find('.form-init').length > 0) {
            alert('表单信息不完整，请检查后重新提交！');
            return false;
        }
        else if (form.find('.form-error').length > 0) {
            alert('表单信息输入有误，请检查后重新提交！')
            return false;
        }

        return true;
    };

    //展现搜索结果
    page.showSearch = function (result) {
        if (result.length != 0) {
            $('#js-table').html(result);
            $(".pagination").pagination($('#js-table tbody').find('tr').length);
            $("#js-table tbody tr:gt(" + (pageSize - 1) + ")").hide().end();
        }
        else {
            $('#js-table').empty();
            $('.pagination').empty().html('没有相关信息！');
        }
    }

    //获得当前的Controller名，如.../Module/Index，则controller为Module
    var beforeController = location.href.substring(0, location.href.lastIndexOf('/'));
    var controller = beforeController.substring(beforeController.lastIndexOf('/') + 1).capitalize();

    page.getController = function () {
        return controller;
    }

    page.getUrl = function () {
        if (!arguments[0]) {
            alert('Ajax操作Url无效')
        }
        else {
            return '/' + controller + '/' + arguments[0];
        }
    }

    //选中表格行的ID
    var checkId;

    page.getCheckId = function () {
        return checkId;
    }

    page.setCheckId = function (id) {
        checkId = id;
    }

    //删除表格行的ID
    var deleteId = [];

    page.setDeleteId = function (id) {
        if (deleteId.length > 0) {
            deleteId = [];
        }
        deleteId.push(id);
    }

    page.getDeleteId = function () {
        return deleteId;
    }

    return page;
})(jQuery);

/****************************分页相关*****************************/

var paging = {
    pageIndex: 0,
    init: function (index) {
        $(".pagination").pagination(dataCount, { items_per_page: pageSize, current_page: index + 1 || 0 });
    },
    show: function (index) {
        if (~ ~dataCount > ~ ~pageSize) {
            this.init(index);
            $('.pagination').showIfHide();
            $('#page' + index).trigger('click');
        }
        else {
            $('.pagination').hide();
        }
    },
    reset: function () {
        var change = arguments[0] || -1;  //+为添加，-为删除
        dataCount = ~ ~dataCount + change;
        var newIndex = Math.ceil(dataCount / pageSize) - 1;
        //删除
        if (change < 0) {
            //当前数据不超过一页时，重新请求数据
            if (newIndex == 0) {
                $.post(jPage.getUrl('Index'), { "pageIndex": 1 })
                 .done(function (result) {
                     $('#js-table').html(result);
                     $('.pagination').hide();
                     bindTable();
                 });
            }
            else {
                $('.pagination').showIfHide();
                //本页数据全部删除，隐藏当前页码，并跳到前一页
                if (newIndex < paging.pageIndex) {
                    --paging.pageIndex;
                    $('#page' + paging.pageIndex).hide();
                    this.show(newIndex);
                }
                //重新请求本页数据
                else {
                    this.show(paging.pageIndex);
                }
            }
        }
        //添加
        else {
            //当前数据不超过一页时，重新请求数据
            if (newIndex == 0) {
                $.post(jPage.getUrl('Index'), { "pageIndex": 1 })
                 .done(function (result) {
                     $('#js-table').html(result);
                     $('.pagination').hide();
                     bindTable();
                 });
            }
            else {
                this.show(newIndex);
            }
        }
    }
};

/***********************************************************************/

$(document).ajaxError(function (evt, xhr) {
    try {
        var json = $.parseJSON(xhr.responseText);
        //from AjaxExceptionAttribute
        alert(json.errorMessage);
    } catch (e) {
        alert('未知错误');
    }
});

$(document).ready(function () {
    jPage.resize();
    paging.show();
    bindTable();

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
});

function bindTable() {
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
    });

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
            jPage.setCheckId($(this).find('td').eq(0).html());
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
                            url: jPage.getUrl('Modify'),
                            contentType: 'application/json',
                            dataType: 'html',
                            //IE6、IE7默认不支持JSON.stringify，可以引入Scripts/my/json2.js解决此问题
                            data: JSON.stringify(dataInfo)
                        }).done(function (result) {
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
            jPage.setDeleteId($(this).find('td').eq(0).html());
        }
    });

    var isSelectAll = false;

    //表格全选CheckBox点击事件
    $('#js-check-all').on('click', function () {
        isSelectAll = !isSelectAll;
        $('#js-table').find('.js-check-cell').attr('checked', isSelectAll);
    });
};

/****************************弹出模态窗口*****************************/

//模态窗口展现事件
$('.modal').on('shown', function (e) {
    var modal = $(this);
    modal.css('margin-top', (modal.outerHeight() / 2) * -1.3)
         .css('margin-left', (modal.outerWidth() / 2) * -1);
    return this;
});

//确定删除按钮点击事件
$('#js-btn-modal-delete').bind('click', function () {
    $.ajax({
        type: 'POST',
        url: jPage.getUrl('Delete'),
        contentType: 'application/json',
        dataType: 'html',
        data: JSON.stringify(jPage.getDeleteId())
    }).done(function () {
        paging.reset(-1);
    });
});

//确定编辑按钮点击事件
$('#js-btn-modal-edit').on('click', function () {
    var form = $('#js-edit-form');
    if (jPage.isValidForm(form)) {
        $.ajax({
            type: 'POST',
            url: jPage.getUrl('Update'),
            data: form.serialize()
        }).done(function () {
            paging.show(paging.pageIndex);
        });
    }
});

/****************************工具栏按钮*****************************/

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
        jPage.resize();
    });
    $('#js-div-add').show();
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
                url: jPage.getUrl('Delete'),
                contentType: 'application/json',
                dataType: 'html',
                data: JSON.stringify(ids)
            }).done(function () {
                paging.reset(-ids.length);
            });
        }
    }
    else {
        alert('请选中需要删除的行');
    }
});

//工具栏刷新按钮点击事件
$('#js-btn-toolbar-refresh').on('click', function () {
    $.post(jPage.getUrl('Index'))
     .done(function (result) {
         $('#js-table').html(result);
         paging.show();
     });
});

//工具栏搜索按钮点击事件
$('#js-btn-search').on('click', function () {
    if ($('#js-input-search').val() == '') {
        alert('请输入搜索的内容');
    }
    else {
        $.post(jPage.getUrl('Search'), { 'name': $('#js-input-search').val() })
         .done(function (result) {
             jPage.showSearch(result);
         });
    }
});

/****************************添加表单*****************************/

//确定按钮点击事件
$('#js-btn-form-add').on('click', function () {
    var form = $(this).closest('form');
    if (jPage.isValidForm(form)) {
        $.post(jPage.getUrl('Create'), form.serialize())
         .done(function () {
             paging.reset(1);
             $('body').animate({ scrollTop: -$('#js-div-add').height() }, 'fast');
         });
    }
});

//取消按钮点击事件
$('#js-btn-form-cancel').on('click', function () {
    $('body').animate({ scrollTop: -$('#js-div-add').height() }, 'fast');
    $('#js-div-add').hide();
});