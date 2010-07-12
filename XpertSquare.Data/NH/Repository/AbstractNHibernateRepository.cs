using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;
using XpertSquare.Core.Repository;

namespace XpertSquare.Data.NH.Repository
{
    public abstract class AbstractNHibernateRepository<T, IdT> : IRepository<T, IdT>
    {
        /// <summary>
        /// Loads an instance of type TypeOfListItem from the DB based on its ID.
        /// </summary>
        public T GetById(IdT id)
        {
            return (T)NHibernateSession.Get(persitentType, id);
        }

        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        public IQueryable<T> GetAll()
        {
            return NHibernateSession.Linq<T>().AsQueryable();
        }

        public INHibernateQueryable<T> Linq()
        {
            return NHibernateSession.Linq<T>();
        }

        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        public IQueryable<T> GetByCriteria(params ICriterion[] criterion)
        {
            ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);

            foreach (ICriterion criterium in criterion)
            {
                criteria.Add(criterium);
            }

            return criteria.List<T>().AsQueryable();
        }

        public IQueryable<T> GetByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);
            Example example = Example.Create(exampleInstance);

            foreach (string propertyToExclude in propertiesToExclude)
            {
                example.ExcludeProperty(propertyToExclude);
            }

            criteria.Add(example);

            return criteria.List<T>().AsQueryable();
        }

        /// <summary>
        /// Looks for a single instance using the example provided.
        /// </summary>
        /// <exception cref="NonUniqueResultException" />
        public T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            IQueryable<T> foundList = GetByExample(exampleInstance, propertiesToExclude);

            if (foundList.Count<T>() > 1)
            {
                throw new NonUniqueResultException(foundList.Count<T>());
            }

            if (foundList.Count<T>() > 0)
            {
                return foundList.First<T>();
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </summary>
        
        public virtual T Save(T entity)
        {
            NHibernateSession.Save(entity);
            return entity;
        }

        /// <summary>
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to update any 
        /// entity, even if its ID is assigned.
        /// </summary>
        public virtual T SaveOrUpdate(T entity)
        {
            NHibernateSession.SaveOrUpdate(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            NHibernateSession.Delete(entity);
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        public void CommitChanges()
        {
            if (NHibernateSessionManager.Instance.HasOpenTransaction())
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            else
            {
                // If there's no transaction, just flush the changes
                NHibernateSessionManager.Instance.GetSession().Flush();
            }
        }

        /// <summary>
        /// Exposes the ISession used within the DAO.
        /// </summary>
        private ISession NHibernateSession
        {
            get
            {
                return NHibernateSessionManager.Instance.GetSession();
            }
        }

        private Type persitentType = typeof(T);
    }
}
