$('.js-module-name').on('blur', function () {
    $('.js-module-code').val(makePy($(this).val()));
});

$('#EditParentID').on('change', function () {
    if ($(this).get(0).selectedIndex != 0) {
        $('#EController').show();
        $('#EController').find('.form-tip').toggleClass('form-init');
    }
    else {
        $('#EController').hide();
        $('#EController').find('.form-tip').toggleClass('form-init');
    }
});

function FillEditInfo(id) {
    $('#EditModuleID').val(id);
    $.getJSON('/Module/Get/' + id)
     .done(function (data) {
         $('#EditParentID').find('option[value=' + data.ParentId + ']').prop('selected', 'true');
         if (data.ParentId != null) {
             $('#EController').show();
         }
         $('#EditName').val(data.Name);
         $('#EditCode').val(data.Code);
         $('#EditController').val(data.Controller);
         if (data.IsEnable) {
             $('#EditVisible').prop('checked', 'true');
         }
         else {
             $('#EditHide').prop('checked', 'true');
         }
         if (data.Operations != null) {
             var operations = data.Operations.split(',');
             for (var i = 1; i <= operations.length; i++) {
                 $('#op' + i).prop('checked', 'true');
             }
         }
         else {
             var opLength = $('#EditOp').find('input[id^="op"]').length;
             for (var i = 1; i <= opLength; i++) {
                 $('#op' + i).prop('checked', '');
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

//实现common.js中的方法
function showDetail(data) {
    $('#detailController').html(data.Controller);
    $('#detailName').html(data.Name);
    $('#detailCode').html(data.Code);
    $('#detailIsEnable').html(data.IsEnable == 0 ? '是' : '否');
    $('#detailCode').html(data.Code);
}