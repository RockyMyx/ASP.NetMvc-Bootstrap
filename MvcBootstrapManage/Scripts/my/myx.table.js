jQuery.fn.alternateRowColors = function () {
    $("tbody tr:odd", this).removeClass("even").addClass("odd");
    $("tbody tr:even", this).removeClass("odd").addClass("even");
    return this;
}

String.prototype.capitalize = function () {
    return this.replace(/^\w/, function (s) {
        return s.toUpperCase();
    });
};

function resize() {
    $('.content').css('height', $(window).height() + $(window).scrollTop());
}

$(window).on('resize', function () {
    resize();
});

$(document).ready(function () {
    resize();
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
            else if ($(this).is(".sort-data")) {
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

var removeController = location.href.substring(0, location.href.lastIndexOf('/'));
var thisModel = removeController.substring(removeController.lastIndexOf('/') + 1).capitalize();

function bindTable() {
    var deleteId = [];
    var currentTd, input;
    var key, content;
    var beforeEditInfo = [];
    var dataInfo = {};
    $('#js-table').find('tr').click(function (e) {
        if (e.target.className == 'icon-pencil') {
            var tdLength = $(this).find('td').length;
            var tds = $(this).find('td');
            dataInfo["ID"] = tds.eq(0).html();
            beforeEditInfo.push(tds.eq(0).html());
            var isCommit = true;

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

            var oldtxt;
            $('.edit-txt').on('focus', function () {
                oldtxt = $(this).val();
            });

            $('.edit-txt').on('blur', function () {
                //依赖于myx.validate.js
                var validateType = $(this).attr('validate');
                if (mValidate[validateType]) {
                    if (!mValidate[validateType]($(this).val())) {
                        isCommit = false;
                        $('<span style="background-image:url(/Images/error.png);width:16px;height:16px;display:inline-block;margin-left:5px;" title="' + validationTip[validateType] + '"></span>').insertAfter($(this));
                    }
                    else {
                        $(this).next().hide();
                        var newtxt = $(this).val();
                        if (newtxt != oldtxt) {
                            dataInfo[$(this).attr('key')] = newtxt;
                        }
                    }
                }
                var newtxt = $(this).val();
                if (newtxt != oldtxt) {
                    dataInfo[$(this).attr('key')] = newtxt;
                }
            });

            $('.edit-check').on('click', function () {
                var ischecked = $(this).is(':checked') ? 1 : 0;
                dataInfo[$(this).attr('key')] = ischecked;
                $(this).attr('content', ischecked);
            });

            $(this).find('td').eq(tdLength - 1).html('<input type="button" id="edit-sure" class="btn btn-small btn-primary" value="确定" /><input type="button" id="edit-cancel" class="btn btn-small btn-primary" value="取消" />');

            $('#edit-cancel').on('click', function () {
                var count = tds.length - 1;
                for (var i = 0; i < count; i++) {
                    tds.eq(i).html(beforeEditInfo[i]);
                }
                resetEditButton();
            });

            function resetEditButton() {
                tds.eq(tds.length - 1).html('<a href="#"><i class="icon-pencil"></i></a><a href="#ModalDel" role="button" data-toggle="modal"><i class="icon-remove"></i></a>');
            }

            $('#edit-sure').on('click', function () {
                if (isCommit) {
                    $.ajax({
                        type: 'POST',
                        url: '/' + thisModel + '/Update',
                        contentType: 'application/json',
                        dataType: 'html',
                        //IE6、IE7默认不支持JSON.stringify，可以引入json2.js解决此问题
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
            });
        }
        else if (e.target.className == 'icon-remove') {
            deleteId.push($(this).find('td').eq(0).html());
        }
    });

    $('#js-btn-modal-delete').on('click', function () {
        $.ajax({
            type: 'POST',
            url: '/' + thisModel + '/Delete',
            contentType: 'application/json',
            dataType: 'html',
            data: JSON.stringify(deleteId),
            success: function () {
                location.reload();
            }
        });
    });

    var isSelectAll = false;
    $('#js-check-all').on('click', function () {
        isSelectAll = !isSelectAll;
        $('#js-table').find('.js-check-cell').attr('checked', isSelectAll);
    });

    $('#js-btn-toolbar-delete').on('click', function (e) {
        var trs = $('.table tbody').find('tr');
        var deltd, ids = [];
        for (var i = 0; i < trs.length; i++) {
            deltd = trs.eq(i);
            if (deltd.find('.js-check-cell').is(':checked')) {
                ids.push(deltd.find('td').eq(0).html());
            }
        }
        if (ids.length != 0) {
            $.ajax({
                type: 'POST',
                url: '/' + thisModel + '/Delete',
                contentType: 'application/json',
                dataType: 'html',
                data: JSON.stringify(ids),
                success: function () {
                    location.reload();
                }
            });
        }
        else {
            alert('请选中需要删除的行');
        }
    });
}

$('#js-btn-toolbar-refresh').on('click', function () {
    location.reload();
});

$('#js-btn-toolbar-add').on('click', function () {
    $('#js-add-div').show();
    $('body').css('height', parseInt($(document).height()) + 100 + 'px');
    $('body').animate({ scrollTop: $('#js-grid').height() }, 'fast', function () {
        resize();
    });
});

$('#js-btn-form-add').on('click', function () {
    var form = $(this).closest("form");
    if (form.find('.form-init').length > 0) {
        alert('表单信息不完整，请检查后重新提交！')
    }
    else if (form.find('.form-error').length > 0) {
        alert('表单信息输入有误，请检查后重新提交！')
    }
    else {
        $.post('/' + thisModel + '/Add', form.serialize())
         .done(function () {
             alert('添加成功！');
             location.reload();
         });
    }
});

$('#js-btn-form-cancel').on('click', function () {
    $('#js-add-div').hide();
    $('body').css('height', $(window).height());
    $('body').animate({ scrollTop: -$('#js-add-div').height() }, 'fast', function () {
        resize();
    });
});

$('#js-module-name').on('blur', function () {
    $('#js-module-code').val(makePy($(this).val()));
});