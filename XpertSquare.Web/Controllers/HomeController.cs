using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using XpertSquare.Core.Model;
using XpertSquare.Core;
using XpertSquare.Web.Core;
using XpertSquare.Core.Repository;
using XpertSquare.Data.Mocks;
using log4net;

namespace XpertSquare.Web.Controllers
{
    [HandleError]
    public class HomeController : XsController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

        private IArticleRepository articleRepository = null;


        public HomeController()
        {

            articleRepository = new MockWikiArticleRepository();
        }

        public HomeController(IArticleRepository wikiArticleRepository)
        {
            articleRepository = wikiArticleRepository;
        }


        public ActionResult Index()
        {

            IEnumerable<WikiArticle> articles = articleRepository.GetAll();

            return View(articles);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View("Error");
        }

        public ActionResult NotFound()
        {
            return View("NotFound");
        }
    }
}
