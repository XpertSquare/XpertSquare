using System;
using System.Collections.Generic;
using System.Linq;

using XpertSquare.Core.Model;

namespace XpertSquare.Core.Repository
{
     public interface IQuestionRepository : IRepository<XpertSquare.Core.Model.XsQuestion,long>
    {
         IQueryable<XsQuestion> GetUserQuestions(long userID);
    }
}
