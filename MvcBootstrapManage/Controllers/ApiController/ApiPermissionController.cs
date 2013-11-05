using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MvcBootstrapManage.Models;

namespace MvcBootstrapManage.Controllers
{
    public class ApiPermissionController : ApiController
    {
        public Dictionary<int,string> GetPermissionByRole(int id)
        {
            using (DBEntity db = new DBEntity())
            {
                var permissions = db.Permission.Where(p => p.RoleID == id)
                                    .Select(p => new { p.ControllerID, p.ActionID })
                                    .AsEnumerable();
                if (permissions.Count() == 0)
                {
                    return null;
                }
                else
                {
                    Dictionary<int, string> result = new Dictionary<int, string>();
                    int controllerId;
                    foreach (var permission in permissions)
                    {
                        controllerId = Convert.ToInt32(permission.ControllerID);
                        if (!result.ContainsKey(controllerId))
                        {
                            result.Add(controllerId, permission.ActionID.ToString());
                        }
                        else
                        {
                            result[controllerId] += permission.ActionID.ToString();
                        }
                    }

                    return result;
                }
            }
        }
    }
}