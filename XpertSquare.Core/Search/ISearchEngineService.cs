using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;

namespace XpertSquare.Core.Search
{
    public interface ISearchEngineService
    {
        IList<XsQuestion> SearchQuestions(String searchQuery);
        IList<XsQuestion> SearchQuestions(String searchQuery, Int16 numberOfResults);
        IList<XsQuestion> SearchRelatedQuestions(XsQuestion question);
        IList<XsQuestion> SearchRelatedQuestions(XsQuestion question, Int16 numberOfResults);
        IList<IndexingError> AddQuestionToIndex(XsQuestion question);
        IList<IndexingError> AddQuestionsToIndex(IList<XsQuestion> questions);
        void BuildIndex();
    }
}
