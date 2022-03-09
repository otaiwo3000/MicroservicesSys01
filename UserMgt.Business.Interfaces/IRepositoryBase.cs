using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UserMgt.Business.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        //T Create(T entity);
        void Create(T entity);
        //T Update(T entity);
        void Update(T entity);
        void Delete(T entity);

        void CreateRange(IEnumerable<T> entityList);
        void UpdateRange(IEnumerable<T> entityList);
    }
}
