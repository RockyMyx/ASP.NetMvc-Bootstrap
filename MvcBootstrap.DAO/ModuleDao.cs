using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<T_Module>, IModuleDao
    {
        public override IEnumerable<T_Module> GetPagingInfo(int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Take(pageSize).ToList();
            }
        }

        public override IEnumerable<T_Module> GetPagingInfo(int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public override IEnumerable<T_Module> GetEntities(Expression<Func<T_Module, bool>> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            }
        }

        public IEnumerable<T_Module> GetSortedModules()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().ToList();
            }
        }
    }
}
