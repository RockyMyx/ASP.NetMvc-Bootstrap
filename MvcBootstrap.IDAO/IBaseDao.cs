using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;

namespace MvcBootstrap.IDAO
{
    public interface IBaseDao<T> where T : EntityObject
    {
        T GetEntity(Expression<Func<T, bool>> whereExp);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetEntities(Expression<Func<T, bool>> whereExp);
        int GetEntitiesCount(Expression<Func<T, bool>> whereExp = null);
        IEnumerable<T> GetPagingInfo(Expression<Func<T, int>> orderby, int pageIndex, int pageSize);
        IEnumerable<T> GetPagingInfo(int pageIndex, int pageSize);
        IEnumerable<T> GetPagingInfo(int pageSize);
        IEnumerable<T> GetSearchPagingInfo(IEnumerable<T> entities, int pageIndex, int pageSize);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(List<int> idList);
    }
}
