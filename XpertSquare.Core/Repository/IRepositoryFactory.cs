using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Repository
{
    /// <summary>
    /// Provides an interface for retrieving Repository objects
    /// </summary>
    public interface IRepositoryFactory
    {
        IQuestionRepository GetQuestionRepository();
        IAnswerRepository GetAnswerRepository();
        IXsUserRepository GetUserRepository();
        ITagRepository GetTagRepository();
        IVoteRepository GetVoteRepository();
    }
}
