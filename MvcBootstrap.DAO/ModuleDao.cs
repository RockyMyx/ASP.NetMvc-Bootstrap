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

        public IEnumerable<Module> GetSortedModules()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().ToList();
            }
        }

        public IEnumerable<Module> GetSearchModules(IEnumerable<Module> modules, int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return modules.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
