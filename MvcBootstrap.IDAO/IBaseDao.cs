using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace MvcBootstrap.IDAO
{
    public interface IBaseDao<T> where T : EntityObject
    {
        T GetEntity(Func<T, bool> whereExp);
        IList<T> GetAll();
        IEnumerable<T> GetEntities(Func<T, bool> whereExp);
        int GetEntitiesCount(Func<T, bool> whereExp);
        IEnumerable<T> GetPagingInfo(Func<T, int> orderby, int pageIndex, int pageSize);
        IEnumerable<T> GetPagingInfo(int pageIndex, int pageSize);
        IEnumerable<T> GetPagingInfo(int pageSize);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(List<int> idList);
    }
}
