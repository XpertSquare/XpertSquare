using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;

namespace XpertSquare.Data.Mocks
{
   public class MockXsUserRepository : IXsUserRepository
    {
       private XsUser mockUser = null;

       IList<XsUser> users = null; 


       public MockXsUserRepository()
       {
           //create one generic user
           mockUser = new XsUser();
           mockUser.ID = 10;
           mockUser.Username = "Marius.Serban";
           mockUser.FirstName = "Marius";
           mockUser.LastName = "Serban";

           users = new List<XsUser>();
           for (int i = 0; i < 99; i++)
           {
               XsUser user = new XsUser();
               user.ID = 101 + i;
               user.Username = "Marius.Serban" + i.ToString();
               user.FirstName = "Marius" + i.ToString();
               user.LastName = "Serban" + i.ToString();
               users.Add(user);
           }

       }


       #region IRepository<XsUser,long> Members

       public XsUser GetById(long id)
        {
            mockUser.ID = id;
            return mockUser;
        }

       public XsUser GetByUsername(String username)
       {
           mockUser.Username = username;
           return mockUser;
       }

        public IQueryable<XsUser> GetAll()
        {
            return users.AsQueryable();
        }

        public IQueryable<XsUser> GetByExample(XsUser exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        public XsUser GetUniqueByExample(XsUser exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        public XsUser Save(XsUser entity)
        {
            return entity;
        }

        public XsUser SaveOrUpdate(XsUser entity)
        {
            return entity;
        }

        public void Delete(XsUser entity)
        {
            throw new NotImplementedException();
        }

        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        #endregion
       

        #region IRepository<XsUser,long> Members

        XsUser IRepository<XsUser, long>.GetById(long id)
        {
            throw new NotImplementedException();
        }

        IQueryable<XsUser> IRepository<XsUser, long>.GetAll()
        {
            throw new NotImplementedException();
        }

        IQueryable<XsUser> IRepository<XsUser, long>.GetByExample(XsUser exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        XsUser IRepository<XsUser, long>.GetUniqueByExample(XsUser exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        XsUser IRepository<XsUser, long>.Save(XsUser entity)
        {
            throw new NotImplementedException();
        }

        XsUser IRepository<XsUser, long>.SaveOrUpdate(XsUser entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<XsUser, long>.Delete(XsUser entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<XsUser, long>.CommitChanges()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
