using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XpertSquare.Core.Repository;
using XpertSquare.Data.NH.Repository;


namespace XpertSquare.Data.NH
{
    /// <summary>
    /// Exposes access to NHibernate DAO classes.  Motivation for this DAO
    /// framework can be found at http://www.hibernate.org/328.html.
    /// </summary>
    public class NHibernateRepositoryFactory : IRepositoryFactory
    {

        #region IRepositoryFactory Members

        public IQuestionRepository GetQuestionRepository()
        {
            return new QuestionRepository(); 
        }

        public IAnswerRepository GetAnswerRepository()
        {
            return new AnswerRepository();
        }

        public IXsUserRepository GetUserRepository()
        {
            return new UserRepository();
        }

        public ITagRepository GetTagRepository()
        {
            return new TagRepository();
        }

        public IVoteRepository GetVoteRepository()
        {
            return new VoteRepository();
        }

        #endregion
    }
}
