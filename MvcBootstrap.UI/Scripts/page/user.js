function FillEditInfo(id) {
    $('#EditUserID').val(id);
    $.getJSON('/User/Get/' + id)
     .done(function (data) {
         $('#EditName').val(data.Name);
         $('#EditPassword').val(data.Password);
         $('#EditRemark').val(data.Remark);
         $('#EditRoleId').find('option[value=' + data.RoleId + ']').prop('selected', 'true');
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
    },
    callback: {
        onCheck: onCheck
    }
};

function onCheck(e, treeId, treeNode) {
    var treeObj = $.fn.zTree.getZTreeObj("aisTree"),
                    nodes = treeObj.getCheckedNodes(true),
                    v = "";
    for (var i = 0; i < nodes.length; i++) {
        v += nodes[i].name + ",";
        alert(nodes[i].id); //获取选中节点的值
        alert(nodes[i].pId); //获取选中节点的值
    }
}

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
        $.getJSON('/User/GetResourceTreeNodes')
         .done(function (data) {
             $.fn.zTree.init($("#aisTree"), setting, data);
             $('.js-loading').hide();
         });
    }
});
