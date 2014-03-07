using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<T_Module>, IModuleDao
    {
        public override IEnumerable<T_Module> GetPagingInfo(int pageSize)
        {
            //return dbExtend.GetModuleTree().Take(pageSize).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Take(pageSize).ToList();
            }
        }

        public override IEnumerable<T_Module> GetPagingInfo(int pageIndex, int pageSize)
        {
            //return dbExtend.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public override IEnumerable<T_Module> GetEntities(Expression<Func<T_Module, bool>> whereExp)
        {
            //return dbExtend.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            }
        }

        public IEnumerable<T_Module> GetSortedModules()
        {
            //return dbExtend.GetModuleTree().AsQueryable().ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().ToList();
            }
        }
    }
}
