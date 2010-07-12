using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;
using NHibernate.Linq;
using NHibernate;
using NHibernate.Criterion;


namespace XpertSquare.Data.NH.Repository
{
    public class UserRepository : AbstractNHibernateRepository<XsUser, long>, IXsUserRepository
    {
        public XsUser GetByUsername(String username)
        {
            return (from u in Linq()
                    where u.Username == username
                    select u).FirstOrDefault();
        }
    }
}
