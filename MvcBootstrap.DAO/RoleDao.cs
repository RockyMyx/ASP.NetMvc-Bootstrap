using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;

namespace MvcBootstrap.DAO
{
    public class RoleDao : BaseEFDao<Role>, IRoleDao<Role>
    {
        public override bool Delete(List<int> ids)
        {
            using (DBEntity db = new DBEntity())
            {
                try
                {
                    Role role = null;
                    foreach (int id in ids)
                    {
                        role = db.Role.GetEntity(r => r.ID == id);
                        db.DeleteObject(role);
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (OptimisticConcurrencyException)
                {
                    return false;
                }
            }
        }
    }
}
