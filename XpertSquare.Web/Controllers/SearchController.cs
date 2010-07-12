using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using log4net;

using XpertSquare.Core;
using XpertSquare.Core.Model;
using XpertSquare.Web.Core;
using XpertSquare.Core.Repository;
using XpertSquare.Data.NH;
using XpertSquare.Data.NH.Repository;
using XpertSquare.Core.Search;

using XpertSquare.Web.Models;


namespace XpertSquare.Web.Controllers
{
    public class SearchController : XsController
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SearchController));
        private IQuestionRepository questionRepository = null;

        //
        // GET: /Search/

        public SearchController(IRepositoryFactory repositoryFactory)
        {
            questionRepository = repositoryFactory.GetQuestionRepository();
        }

        public SearchController()
        {
            questionRepository = new QuestionRepository();
        }

        
        [ValidateInput(false)]        
        public ActionResult Search(String q, int? pg)
        {

            Int32 currentPage = pg ?? 1;

            SearchResultViewModel searchResult = new SearchResultViewModel() 
            { 
                SearchQuery = q, 
                SearchSuggestion=String.Empty 
            };
            IList<XsQuestion> searchQuestions = new List<XsQuestion>();

            if (!String.IsNullOrEmpty(q))
            {
                ISearchEngineService searchService = new SearchEngineService(questionRepository);
                searchQuestions = searchService.SearchQuestions(q);
            }

            IPagination pageQuestions = searchQuestions.AsPagination(currentPage,Settings.QUESTIONS_PAGINATION_SIZE);                

            Int32 currentPageSize = Settings.TAGS_PAGINATION_SIZE;

            if (currentPage == pageQuestions.TotalPages)
            {
                currentPageSize = pageQuestions.TotalItems - (pageQuestions.TotalPages - 1) * Settings.TAGS_PAGINATION_SIZE;
            }
            else
            {
                if (0 == pageQuestions.TotalPages)
                {
                    currentPageSize = 0;
                }
            }

            searchResult.Questions = pageQuestions;
            if (0 == pageQuestions.TotalItems)
            {
                String searchSuggestion = new XpertSquare.Core.Search.GoogleSearchSuggestor().GetSuggestion(q);
                searchResult.SearchSuggestion = searchSuggestion;
            }

            return View(searchResult);
        }
    }
}
