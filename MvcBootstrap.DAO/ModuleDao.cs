using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<Module>, IModuleDao
    {
        public override IEnumerable<Module> GetPagingInfo(int pageSize)
        {
            //return dbExtend.GetModuleTree().Take(pageSize).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Take(pageSize).ToList();
            }
        }

        public override IEnumerable<Module> GetPagingInfo(int pageIndex, int pageSize)
        {
            //return dbExtend.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public override IEnumerable<Module> GetEntities(Expression<Func<Module, bool>> whereExp)
        {
            //return dbExtend.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            }
        }

        public IEnumerable<Module> GetSortedModules()
        {
            //return dbExtend.GetModuleTree().AsQueryable().ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().ToList();
            }
        }
    }
}
