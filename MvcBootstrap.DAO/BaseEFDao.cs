using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.IDAO;
using MvcBootstrap.EFModel;
using System.Data;
using System.Data.Objects;

namespace MvcBootstrap.DAO
{
    public abstract class BaseEFDao<T> : IBaseDao<T> where T : class
    {
        protected virtual string tableName
        {
            get
            {
                return this.GetType().Name.ToString().Replace("Dao", "");
            }
        }

        #region IBaseDao<T> Members

        public virtual T GetEntity(Func<T, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Where(whereExp).SingleOrDefault();
            }
        }

        public virtual IList<T> GetAll()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().ToList();
            }
        }

        public virtual IEnumerable<T> GetEntities(Func<T, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Where(whereExp);
            }
        }

        public virtual int GetEntitiesCount(Func<T, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Where(whereExp).Count();
            }
        }


        public virtual IEnumerable<T> GetPagingInfo(Func<T, int> orderby, int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>()
                         .OrderBy(orderby)
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);
            }
        }

        public virtual IEnumerable<T> GetPagingInfo(int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>()
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);
            }
        }

        public virtual IEnumerable<T> GetPagingInfo(int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.CreateObjectSet<T>().Take(pageSize);
            }
        }

        public virtual bool Create(T entity)
        {
            using (DBEntity db = new DBEntity())
            {
                db.CreateObjectSet<T>().AddObject(entity);
                return db.SaveChanges() > 0;
            }
        }

        public virtual bool Update(T entity)
        {
            using (DBEntity db = new DBEntity())
            {
                try
                {
                    var obj = db.CreateObjectSet<T>();
                    obj.Attach(entity);
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    return db.SaveChanges() > 0;
                }
                catch (OptimisticConcurrencyException)
                {
                    db.Refresh(RefreshMode.StoreWins, entity);
                    return false;
                }
            }
        }

        public virtual bool Delete(T entity)
        {
            using (DBEntity db = new DBEntity())
            {
                try
                {
                    var obj = db.CreateObjectSet<T>();
                    obj.Attach(entity);
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
                    obj.DeleteObject(entity);
                    return db.SaveChanges() > 0;
                }
                catch (OptimisticConcurrencyException)
                {
                    db.Refresh(RefreshMode.StoreWins, entity);
                    return false;
                }
            }
        }

        public virtual bool Delete(List<int> idList)
        {
            using (DBEntity db = new DBEntity())
            {
                try
                {
                    string ids = string.Join(",", idList.ToArray());
                    db.DeleteObjects(ids, tableName);
                    return true;
                }
                catch (OptimisticConcurrencyException)
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
