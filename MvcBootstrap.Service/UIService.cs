using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.Service;

public static class UIService
{
    /// <summary>
    /// 生成用户功能菜单
    /// </summary>
    public static MvcHtmlString CreateMenu(this HtmlHelper helper)
    {
        //HttpSessionState session = new HttpSessionState();
        //session.Get("RoleId");

        //ToTest
        int roleID = 1; 
        string parentMenu = "<a href=\"#{0}\" class=\"nav-header\" data-toggle=\"collapse\"><i class=\"ico-menu ico-{1}\"></i>{2}</a>";
        string childMenu = "<ul id=\"{0}\" class=\"nav nav-list collapse in pl20\">{1}</ul>";
        string childContent = "<li><a target=\"content\" href=\"/{0}\"><i class=\"ico-menu ico-{1}\"></i>{2}</a></li>";
        UserService userService = new UserService();
        //获取可以浏览的菜单
        IEnumerable<UserBrowseViewModel> modules = userService.GetUserBrowse(roleID);
        //获取父菜单
        IList<UserBrowseViewModel> parentModules = new List<UserBrowseViewModel>();
        modules.Enumerate(m => m.ParentId == 0, m => parentModules.Add(m));
        //获取子菜单
        IEnumerable<T_Module> childModules = null;
        StringBuilder menuBuilder = new StringBuilder();
        StringBuilder childBuilder = new StringBuilder();
        ModuleService moduleService = new ModuleService();
        foreach (var parent in parentModules)
        {
            menuBuilder.AppendFormat(parentMenu, parent.Code + "-menu", parent.Code, parent.Name);
            childModules = moduleService.GetChildModules(parent.ID);
            foreach (var child in childModules)
            {
                childBuilder.AppendFormat(childContent, child.Url, child.Code, child.Name);
            }

            menuBuilder.AppendFormat(childMenu, parent.Code + "-menu", childBuilder.ToString());
            childBuilder.Clear();
        }

        return MvcHtmlString.Create(menuBuilder.ToString());
    }

    /// <summary>
    /// 生成权限操作选项
    /// </summary>
    public static MvcHtmlString CreateOperations(this HtmlHelper helper)
    {
        string opLabel = "<label class=\"checkbox inline ml10\"><input type=\"checkbox\" id=\"op{0}\" name=\"op{0}\" />{1}</label>";
        OperationService service = new OperationService();
        IDictionary<int, string> operations = service.GetOperations();
        StringBuilder opBuilder = new StringBuilder();
        foreach (KeyValuePair<int, string> item in operations)
        {
            opBuilder.AppendFormat(opLabel, item.Key, item.Value);
        }

        return MvcHtmlString.Create(opBuilder.ToString());
    }

    /// <summary>
    /// 权限分配时，生成每个资源对应的权限
    /// </summary>
    public static MvcHtmlString DistributeOptions(this HtmlHelper helper, int moduleId)
    {
        //string label = "<form class=\"js-form-permission\" name=\"setPermission\"><input type=\"checkbox\" class=\"js-checkall-permission\" style=\"margin-top:-2px\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=\"全选\" /><label class=\"inline mr40 pl20\">{0}</label>";
        string label = "<form class=\"js-form-permission\" name=\"setPermission\"><input type=\"checkbox\" class=\"js-checkall-permission\" style=\"margin-top:-2px\" title=\"全选\" /><label class=\"inline mr40 pl20\">{0}</label>";
        string checkbox = "<input type=\"checkbox\" name=\"{0}-{1}\" style=\"margin:-2px 8px 0 8px\" />{2}";

        StringBuilder opBuilder = new StringBuilder();
        ModuleService moduleService = new ModuleService();
        IEnumerable<T_Module> modules = moduleService.GetEntities(m => m.ParentId == moduleId);

        string[] operations = null;
        int actionId = 0;
        T_Operation operation = null;
        OperationService operationService = new OperationService();
        foreach (T_Module module in modules)
        {
            if (!string.IsNullOrWhiteSpace(module.Operations))
            {
                opBuilder.AppendFormat(label, module.Name);
                operations = module.Operations.Split(',');
                foreach (string op in operations)
                {
                    actionId = Convert.ToInt32(op);
                    operation = operationService.GetEntity(o => o.ID == actionId);
                    opBuilder.AppendFormat(checkbox, module.ID, operation.ID, operation.Name);
                }

                opBuilder.Append("</form><p></p>");
            }
        }

        return MvcHtmlString.Create(opBuilder.ToString());
    }
}