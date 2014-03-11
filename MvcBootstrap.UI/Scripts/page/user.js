function FillEditInfo(id) {
    $('#EditUserID').val(id);
    $.getJSON('/User/Get/' + id)
     .done(function (data) {
         $('#EditName').val(data.Name);
         $('#EditPassword').val(data.Password);
         $('#EditRemark').val(data.Remark);
         $('#EditRoleId').find('option[value=' + data.RoleId + ']').prop('selected', 'true');
         var ops = $('#js-edit-form').find('input[name^="Can"]');
         for (var i = 0; i < ops.length; i++) {
             ops[i].checked = data[ops[i].name];
         }
         $(".js-loading").hide();
     });
};

var setting = {
    check: {
        enable: true
    },
    data: {
        simpleData: {
            enable: true
        }
    }
};

$('#js-btn-toolbar-node').on('click', function () {
    var rowCheck = $('input:checked.js-check-cell');
    if (rowCheck.length == 0) {
        alert('请选择需要分配节点的用户！');
        return false;
    }
    else if (rowCheck.length > 1) {
        alert('一次只能分配一个用户的节点！');
        return false;
    }
    else {
        $('#ModalNode').modal('show');
        $('.js-loading').show();
        $.getJSON('/User/GetResourceTreeNodes/' + jPage.getCheckId())
         .done(function (data) {
             $.fn.zTree.init($("#aisTree"), setting, data);
             $('.js-loading').hide();
         });
    }
 });

 $('#js-btn-modal-node').on('click', function (e) {
     var checkedNodes = '';
     var treeObj = $.fn.zTree.getZTreeObj("aisTree"),
                    nodes = treeObj.getCheckedNodes(true);
     for (var i = 0; i < nodes.length; i++) {
         checkedNodes += nodes[i].id + ',';
     }
     checkedNodes = checkedNodes.substring(0, checkedNodes.length - 1);
     $.post(jPage.getUrl('DistributeUserNodes'), { 'idString': checkedNodes }, function (result) {
         if (result) {
             alert('资源分配成功！');
         }
     })
 });