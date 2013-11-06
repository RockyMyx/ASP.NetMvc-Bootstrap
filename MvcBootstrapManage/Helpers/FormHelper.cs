using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBootstrapManage.Models;
using System.Web.Mvc;
using System.Text;

public class FormHelper
{
    public static Module GetModuleInfo(FormCollection formInfo)
    {
        Module module = new Module();
        module.Name = formInfo["Name"].ToString();
        module.Code = formInfo["Code"].ToString();
        module.Controller = formInfo["Controller"].ToString();
        module.IsEnable = formInfo["IsEnable"] == null ? true : string.Compare(formInfo["IsEnable"], "1") == 0;
        int parentId = Convert.ToInt32(formInfo["ParentId"]);
        if (parentId != 0)
        {
            module.ParentId = parentId;
        }

        List<string> operations = formInfo.AllKeys.Where(k => k.Contains("op")).ToList();
        if (operations.Count > 0)
        {
            StringBuilder strOperation = new StringBuilder();
            foreach (string operation in operations)
            {
                strOperation.Append(operation.Replace("op", "") + ",");
            }
            module.Operations = strOperation.Remove(strOperation.Length - 1, 1).ToString();
        }

        return module;
    }

    public static Role GetRoleInfo(FormCollection formInfo)
    {
        Role role = new Role();
        role.Name = formInfo["Name"].ToString();
        role.Remark = formInfo["Remark"].ToString();
        role.IsEnable = formInfo["IsEnable"] == null ? true : string.Compare(formInfo["IsEnable"], "1") == 0;
        return role;
    }
}