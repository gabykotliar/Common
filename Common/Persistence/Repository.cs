using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Persistence
{
    public interface Repository<T>
    {
        T GetById(object id);

        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Find();
        
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);        

        T SaveOrUpdate(T entity);

        void Delete(object id);
    }
}