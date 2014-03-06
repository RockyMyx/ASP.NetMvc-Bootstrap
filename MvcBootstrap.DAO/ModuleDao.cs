using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<module>, IModuleDao
    {
        public override IEnumerable<module> GetPagingInfo(int pageSize)
        {
            //return dbExtend.GetModuleTree().Take(pageSize).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Take(pageSize).ToList();
            }
        }

        public override IEnumerable<module> GetPagingInfo(int pageIndex, int pageSize)
        {
            //return dbExtend.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public override IEnumerable<module> GetEntities(Expression<Func<module, bool>> whereExp)
        {
            //return dbExtend.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().Where(whereExp).ToList();
            }
        }

        public IEnumerable<module> GetSortedModules()
        {
            //return dbExtend.GetModuleTree().AsQueryable().ToList();
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().AsQueryable().ToList();
            }
        }
    }
}
