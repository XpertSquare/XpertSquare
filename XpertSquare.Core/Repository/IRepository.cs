using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Repository
{
    public interface IRepository<T, IdT>
    {
        T GetById(IdT id);
        IQueryable<T> GetAll();
        IQueryable<T> GetByExample(T exampleInstance, params string[] propertiesToExclude);
        T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude);
        T Save(T entity);
        T SaveOrUpdate(T entity);
        void Delete(T entity);
        void CommitChanges();
    }
}
