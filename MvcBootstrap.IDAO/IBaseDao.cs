using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Linq.Expressions;

namespace MvcBootstrap.IDAO
{
    public interface IBaseDao<T> where T : class
    {
        T GetEntity(Expression<Func<T, bool>> whereExp);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetEntities(Expression<Func<T, bool>> whereExp);
        int GetEntitiesCount(Expression<Func<T, bool>> whereExp);
        IEnumerable<T> GetPagingInfo(Expression<Func<T, int>> orderby, int pageIndex, int pageSize);
        IEnumerable<T> GetPagingInfo(int pageIndex, int pageSize);
        IEnumerable<T> GetPagingInfo(int pageSize);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(List<int> idList);
    }
}
