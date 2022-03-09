using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserMgt.Business.Interfaces;
using UserMgt.Shared.DataAccess.DBContext;

namespace UserMgt.Business.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected UserMgtDBContext UserMgtDBContext { get; set; }

        public RepositoryBase(UserMgtDBContext userMgtDBContext)
        {
            UserMgtDBContext = userMgtDBContext;
        }

        //===== NOTE: abstrat methods ========================
        //protected abstract IQueryable<T> GetEntities();  
        //protected abstract T AddEntity(T entity);
        //protected abstract T UpdateEntity(T entity);


        public IQueryable<T> FindAll()
        {
            return UserMgtDBContext.Set<T>();//.AsNoTracking();
            //return UserMgtDBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return UserMgtDBContext.Set<T>().Where(expression);//.AsNoTracking();
            //return UserMgtDBContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        //public T Create(T entity)
        {
            UserMgtDBContext.Set<T>().Add(entity);
            //UserMgtDBContext.SaveChanges();
            ////T addedEntity = AddEntity(entity);
            ////return addedEntity;
        }

        public void Update(T entity)
        //public T Update(T entity)
        {
            UserMgtDBContext.Set<T>().Update(entity);
            //UserMgtDBContext.SaveChanges();
            ////T existingEntity = UpdateEntity(entity);
            ////return existingEntity;
        }

        public void Delete(T entity)
        {
            UserMgtDBContext.Set<T>().Remove(entity);
            //UserMgtDBContext.SaveChanges();
        }

        public void CreateRange(IEnumerable<T> entityList)
        {
            UserMgtDBContext.Set<T>().AddRange(entityList);
        }

        public void UpdateRange(IEnumerable<T> entityList)
        {
            UserMgtDBContext.Set<T>().UpdateRange(entityList);
        }

        public void RemoveRange(IEnumerable<T> entityList)
        {
            UserMgtDBContext.Set<T>().RemoveRange(entityList);
        }
    }
}
