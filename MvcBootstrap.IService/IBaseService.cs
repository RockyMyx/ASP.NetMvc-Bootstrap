using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.IService
{
    public interface IBaseService<T, U>
        where T : EntityObject
        where U : IBaseDao<T>
    {
        int GetCount();
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
