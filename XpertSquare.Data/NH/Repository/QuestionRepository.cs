﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;
using XpertSquare.Core.Search;

namespace XpertSquare.Data.NH.Repository
{
    public class QuestionRepository : AbstractNHibernateRepository<XsQuestion, long>,IQuestionRepository
    {
        public IQueryable<XsQuestion> GetUserQuestions(long userID)
        {
            return (from q in Linq()
                    where q.Author.ID == userID
                    select q);

        }
        public override XsQuestion Save(XsQuestion question)
        {
            //For new questions the ID is generated by NHibernate. The questions needs to be saved before it is added to the search index.
            XsQuestion questionSaved = base.Save(question);

            SearchEngineService svcSearch = new SearchEngineService(this);
            svcSearch.AddQuestionToIndex(questionSaved);

            return questionSaved;
        }

        public override XsQuestion SaveOrUpdate(XsQuestion question)
        {
            if (question.ToAddToSearchIndex)
            {
                SearchEngineService svcSearch = new SearchEngineService(this);
                svcSearch.AddQuestionToIndex(question);
            }
            return base.SaveOrUpdate(question);
        }
    }
}