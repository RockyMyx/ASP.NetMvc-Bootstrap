using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcBootstrap.EFModel;

public class FormHelper
{
    public static Module GetModuleInfo(FormCollection formInfo)
    {
        Module module = new Module
        {
            ID = formInfo["ID"].ObjToInt(),
            Name = formInfo["Name"].ObjToStr(),
            Code = formInfo["Code"].ObjToStr(),
            Controller = formInfo["Controller"].ObjToStr(),
            IsEnable = formInfo["IsEnable"] == null || string.Compare(formInfo["IsEnable"], "1") == 0
        };
        if (formInfo["ParentId"] != "NULL")
        {
            module.ParentId = formInfo["ParentId"].ObjToInt();
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
        Role role = new Role
        {
            Name = formInfo["Name"].ObjToStr(),
            Remark = formInfo["Remark"].ObjToStr(),
            IsEnable = formInfo["IsEnable"] == null || string.Compare(formInfo["IsEnable"], "1") == 0
        };
        return role;
    }
}