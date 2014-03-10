$('.js-module-name').on('blur', function () {
    $('.js-module-code').val(makePy($(this).val()));
});

$('#EditParentID').on('change', function () {
    if ($(this).get(0).selectedIndex != 0) {
        $('#EUrl').show();
        $('#EUrl').find('.form-tip').toggleClass('form-init');
    }
    else {
        $('#EUrl').hide();
        $('#EUrl').find('.form-tip').toggleClass('form-init');
    }
});

//实现common.js中的方法（编辑）
function FillEditInfo(id) {
    $('#EditModuleID').val(id);
    $.getJSON('/Module/Get/' + id)
     .done(function (data) {
         $('#EditParentID').find('option[value=' + data.ParentId + ']').prop('selected', 'true');
         if (data.ParentId != null) {
             $('#EUrl').show();
         }
         $('#EditName').val(data.Name);
         $('#EditCode').val(data.Code);
         $('#EditUrl').val(data.Url).attr('readonly', 'readonly');
         if (data.IsEnable) {
             $('#EditVisible').prop('checked', 'true');
         }
         else {
             $('#EditHide').prop('checked', 'true');
         }
         //清空选择状态
         var ops = $('#EditOp').find('input[id^="op"]');
         for (var i = 0; i < ops.length; i++) {
             ops[i].checked = false;
         }
         //设置选择状态
         if (data.Operations != null) {
             var operations = data.Operations.split(',');
             for (var i = 0; i <= operations.length; i++) {
                 $('#op' + operations[i]).prop('checked', 'true');
             }
         }
         $(".js-loading").hide();
     });
};

$('#js-btn-modal-search').on('click', function () {
    $.post(jPage.getUrl('AdvanceSearch'), $('#js-search-form').serialize())
     .done(function (result) {
         jPage.showSearch(result);
     });
});

//实现common.js中的方法（查看详情）
function showDetail(data) {
    $('#detailUrl').html(data.Url);
    $('#detailName').html(data.Name);
    $('#detailCode').html(data.Code);
    $('#detailIsEnable').html(data.IsEnable == 0 ? '是' : '否');
}