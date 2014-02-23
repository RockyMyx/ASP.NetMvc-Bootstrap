using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public abstract class BaseService<T, U>
        where T : EntityObject
        where U : IBaseDao<T>
    {
        protected U dao = default(U);
        protected abstract void SetCurrentDao();

        protected BaseService()
        {
            SetCurrentDao();
        }

        #region IBaseService<T> Members

        public T GetEntity(Expression<Func<T, bool>> whereExp)
        {
            return dao.GetEntity(whereExp);
        }

        public IEnumerable<T> GetAll()
        {
            return dao.GetAll();
        }

        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> whereExp)
        {
            return dao.GetEntities(whereExp);
        }

        public int GetEntitiesCount(Expression<Func<T, bool>> whereExp = null)
        {
            return dao.GetEntitiesCount(whereExp);
        }

        public IEnumerable<T> GetPagingInfo(Expression<Func<T, int>> orderby, int pageIndex, int pageSize)
        {
            return dao.GetPagingInfo(orderby, pageIndex, pageSize);
        }

        public IEnumerable<T> GetPagingInfo(int pageIndex, int pageSize)
        {
            return dao.GetPagingInfo(pageIndex, pageSize);
        }

        public IEnumerable<T> GetPagingInfo(int pageSize)
        {
            return dao.GetPagingInfo(pageSize);
        }

        public IEnumerable<T> GetSearchPagingInfo(IEnumerable<T> entities, int pageIndex, int pageSize)
        {
            return dao.GetSearchPagingInfo(entities, pageIndex, pageSize);
        }

        public bool Create(T entity)
        {
            return dao.Create(entity);
        }

        public bool Update(T entity)
        {
            return dao.Update(entity);
        }

        public bool Delete(T entity)
        {
            return dao.Delete(entity);
        }

        public bool Delete(List<int> idList)
        {
            return dao.Delete(idList);
        }

        #endregion
    }
}
