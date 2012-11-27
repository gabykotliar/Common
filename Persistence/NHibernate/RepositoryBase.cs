using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Persistence;
using NHibernate;
using NHibernate.Criterion;

namespace Common.Persistence.NHibernate
{
    public abstract class RepositoryBase<T> : Repository<T>
        where T : class
    {
        protected RepositoryBase(ISession session)
        {
            Session = session;
        }

        protected ISession Session { get; set; }

        #region 'Get' Methods. Retrieve a unique entity from the DB
        
        public T GetById(object id)
        {
            return GetById(id, false);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return Session.QueryOver<T>().Where(predicate).List().FirstOrDefault();
        }

        #endregion

        #region 'Find' Methods. Retrieve a set of entities from the DB

        public IEnumerable<T> Find()
        {
            return Session.CreateCriteria<T>().List<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {            
            return Session.QueryOver<T>().Where(predicate).Future();
        }

        #endregion
                
        public T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);

            return entity;
        }

        public void Delete(object id)
        {
            Session.Delete(GetById(id));
            Session.Flush();
        }

        #region NHibernate specific
        
        public T Save(T entity)
        {
            Session.Save(entity);

            return entity;
        }

        public IList<T> FindByCriteria(params ICriterion[] criterion)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));

            foreach (ICriterion criterium in criterion)
            {
                criteria.Add(criterium);
            }

            return criteria.List<T>() as List<T>;
        }

        public IList<T> FindByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            Example example = Example.Create(exampleInstance);

            foreach (var propertyToExclude in propertiesToExclude)
            {
                example.ExcludeProperty(propertyToExclude);
            }

            criteria.Add(example);

            return criteria.List<T>() as List<T>;
        }

        public T GetByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            var foundList = FindByExample(exampleInstance, propertiesToExclude);

            if (foundList.Count > 1) throw new NonUniqueResultException(foundList.Count);

            return foundList.Count > 0
                    ? foundList[0]
                    : default(T);
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier, assuming that the instance exists.
        /// </summary>
        /// <param name="id">A valid identifier of an existing persistent instance of the class.</param>
        /// <remarks>
        ///   You should not use this method to determine if an instance exists (use a query or NHibernate.ISession.Get(System.Type,System.Object) 
        ///   instead). Use this only to retrieve an instance that you assume exists, where non-existence would be an actual error.
        /// </remarks>
        /// <example>
        /// Load will never return null. It will always return an entity or throw an exception.
        /// 
        /// Why is this useful? Well, if you know that the value exist in the database, 
        /// and you don’t want to pay the extra select to have that, but you want to get that value so we can add that 
        /// reference to an object, you can use Load to do so:
        /// <code>
        /// s.Save(
        ///      new Order
        ///      {
        ///           Amount = amount,
        ///           customer = s.Load&lt;Customer&gt;(1)
        ///      }
        /// );
        /// </code>
        /// </example>
        /// <returns>The persistent instance or proxy.</returns>
        public T LoadProxyById(object id)
        {
            return Session.Load<T>(id);
        }

        public T GetById(object id, bool shouldLock)
        {
            var lockMode = shouldLock ? LockMode.Upgrade : LockMode.None;

            return Session.Get<T>(id, lockMode);
        }

        #endregion
    }
}
