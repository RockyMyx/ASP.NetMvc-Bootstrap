using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.IDAO;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.DAO
{
    public class BaseEFDao<T> : IBaseDao<T> where T : class
    {
        #region IBaseDao<T> Members

        public T GetEntity(Func<T, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Where(whereExp).SingleOrDefault();
            }
        }

        public IEnumerable<T> GetEntities(Func<T, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Where(whereExp);
            }
        }

        public int GetEntitiesCount(Func<T, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Where(whereExp).Count();
            }
        }

        public IEnumerable<T> GetPagingInfo(Func<T, int> orderby, int? pageIndex, int pageSize)
        {
            IEnumerable<T> result = null;
            using (DBEntity db = new DBEntity())
            {
                if (orderby != null)
                {
                    result = db.CreateObjectSet<T>().OrderBy(orderby);
                }
                if (pageIndex != null)
                {
                    result = db.CreateObjectSet<T>()
                }
                return db.OrderBy(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Create(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(List<int> ids)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
