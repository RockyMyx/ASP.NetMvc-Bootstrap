using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<Module>, IModuleDao
    {
        public override IEnumerable<Module> GetPagingInfo(int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Take(pageSize).ToList();
            }
        }

        public override IEnumerable<Module> GetPagingInfo(int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public override IEnumerable<Module> GetEntities(Expression<Func<Module, bool>> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            }
        }

        public IQueryable<Module> GetSortedModules()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable();
            }
        }

        public int GetModuleParentId(int moduleId)
        {
            using (DBEntity db = new DBEntity())
            {
                return Convert.ToInt32(db.Module.Where(m => m.ID == moduleId)
                                                .Select(m => m.ParentId)
                                                .Single());
            }
        }
    }
}
