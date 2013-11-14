using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;
using System.Data.Objects;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<Module>, IModuleDao<Module>
    {
        public override bool Delete(List<int> ids)
        {
            using (DBEntity db = new DBEntity())
            {
                try
                {
                    Module module = null;
                    foreach (int id in ids)
                    {
                        module = db.Module.GetEntity(m => m.ID == id);
                        db.DeleteObject(module);
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
