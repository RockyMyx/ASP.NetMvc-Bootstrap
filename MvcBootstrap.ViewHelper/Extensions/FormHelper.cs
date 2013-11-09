using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using MvcBootstrap.EFModel;

public class FormHelper
{
    public static Module GetModuleInfo(FormCollection formInfo)
    {
        Module module = new Module();
        module.Name = formInfo["Name"].ObjToStr();
        module.Code = formInfo["Code"].ObjToStr();
        module.Controller = formInfo["Controller"].ObjToStr();
        module.IsEnable = formInfo["IsEnable"] == null ? true : string.Compare(formInfo["IsEnable"], "1") == 0;
        module.ParentId = formInfo["ParentId"].ObjToInt();

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
        role.Name = formInfo["Name"].ObjToStr();
        role.Remark = formInfo["Remark"].ObjToStr();
        role.IsEnable = formInfo["IsEnable"] == null ? true : string.Compare(formInfo["IsEnable"], "1") == 0;
        return role;
    }
}