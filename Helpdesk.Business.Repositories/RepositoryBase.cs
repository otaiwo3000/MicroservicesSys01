using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Helpdesk.Business.Interfaces;
using Helpdesk.Shared.DataAccess.DBContext;


namespace Helpdesk.Business.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected HelpDeskDBContext HelpDeskDBContext { get; set; }

        public RepositoryBase(HelpDeskDBContext helpDeskDBContext)
        {
            HelpDeskDBContext = helpDeskDBContext;
        }

        //===== NOTE: abstrat methods ========================
        //protected abstract IQueryable<T> GetEntities();  
        //protected abstract T AddEntity(T entity);
        //protected abstract T UpdateEntity(T entity);


        public IQueryable<T> FindAll()
        {
            return HelpDeskDBContext.Set<T>();//.AsNoTracking();
            //return HelpDeskDBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return HelpDeskDBContext.Set<T>().Where(expression);//.AsNoTracking();
            //return HelpDeskDBContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        //public T Create(T entity)
        {
            HelpDeskDBContext.Set<T>().Add(entity);
            //HelpDeskDBContext.SaveChanges();
            ////T addedEntity = AddEntity(entity);
            ////return addedEntity;
        }

        public void Update(T entity)
        //public T Update(T entity)
        {
            HelpDeskDBContext.Set<T>().Update(entity);
            //HelpDeskDBContext.SaveChanges();
            ////T existingEntity = UpdateEntity(entity);
            ////return existingEntity;
        }

        public void Delete(T entity)
        {
            HelpDeskDBContext.Set<T>().Remove(entity);
            //HelpDeskDBContext.SaveChanges();
        }

        public void CreateRange(IEnumerable<T> entityList)
        {
            HelpDeskDBContext.Set<T>().AddRange(entityList);
        }

        public void UpdateRange(IEnumerable<T> entityList)
        {
            HelpDeskDBContext.Set<T>().UpdateRange(entityList);
        }

        public void RemoveRange(IEnumerable<T> entityList)
        {
            HelpDeskDBContext.Set<T>().RemoveRange(entityList);
        }
    }
}
