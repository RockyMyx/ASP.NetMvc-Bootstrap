using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.DAO;
using System.Data.Objects.DataClasses;
using MvcBootstrap.IBLL;

namespace MvcBootstrap.BLL
{
    public class BaseService<T> : IBaseService<T> where T : EntityObject
    {
        #region IBaseService<T> Members

        public T GetEntity(Func<T, bool> whereExp)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetEntities(Func<T, bool> whereExp)
        {
            throw new NotImplementedException();
        }

        public int GetEntitiesCount(Func<T, bool> whereExp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPagingInfo(Func<T, int> orderby, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPagingInfo(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPagingInfo(int pageSize)
        {
            throw new NotImplementedException();
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

        public bool Delete(List<int> idList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
