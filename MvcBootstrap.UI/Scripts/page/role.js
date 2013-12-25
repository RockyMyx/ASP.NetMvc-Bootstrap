$('#myTab a:first').tab('show');
$('#js-btn-toolbar-permission').on('click', function () {
    var roleCheck = $('input:checked.js-check-cell');
    var isCheckAll = $('#js-table').find('#js-check-all').prop('checked');
    if (isCheckAll) {
        alert('请取消全选按钮选中状态后重试！');
    }
    else if (roleCheck.length == 0) {
        alert('请选中需要分配权限的角色！');
    }
    else if (roleCheck.length > 1) {
        alert('每次只能为一个角色分配权限！');
    }
    else {
        //Reset
        var checkboxes = $('.js-form-permission').find('input[type=checkbox]');
        for (var i = 0; i < checkboxes.length; i++) {
            checkboxes.eq(i).prop('checked', '');
        }
        $('#myTab a:first').tab('show');
        $('#ModalPermission').modal('show');
        $('.js-loading').show();
        //加载现有权限
        $.getJSON('/Role/GetPermission/' + jPage.getCheckId())
         .done(function (result) {
             if (result != null) {
                 var ops = [];
                 var name;
                 $.each(result, function (k, v) {
                     ops = v.split('');
                     for (var i = 0; i < ops.length; i++) {
                         name = k + '-' + ops[i];
                         $('input[name="' + name + '"]').prop('checked', 'true');
                     }
                 });
             }
             $('.js-loading').hide();
         });
    }
});
$('#js-btn-modal-permission').on('click', function () {
    $.post(jPage.getUrl('SetPermission/' + jPage.getCheckId()), $('.js-form-permission').serialize())
     .done(function () {
         alert("权限分配成功！");
     });
});
$('.js-checkall-permission').tooltip();
$('.js-checkall-permission').on('click', function () {
    var sibCheckboxes = $(this).closest('form').find('input[type=checkbox]');
    var thisCheckbox;
    if (!$(this).prop('checked')) {
        $(this).attr('data-original-title', '全选');
        for (var i = 1; i < sibCheckboxes.length; i++) {
            thisCheckbox = sibCheckboxes.eq(i);
            if (thisCheckbox.prop('checked')) {
                thisCheckbox.prop('checked', '');
            }
        }
    }
    else {
        $(this).attr('data-original-title', '反选');
        for (var i = 1; i < sibCheckboxes.length; i++) {
            thisCheckbox = sibCheckboxes.eq(i);
            if (!thisCheckbox.prop('checked')) {
                thisCheckbox.prop('checked', 'true');
            }
        }
    }
});