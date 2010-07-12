using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using XpertSquare.Core.Model;
using XpertSquare.Web.Core;
using XpertSquare.Web.Helpers;
using XpertSquare.Data.Mocks;
using XpertSquare.Core.Repository;
using log4net;

namespace XpertSquare.Web.Controllers
{
    public class ArticlesController : XsController
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ArticlesController));

        private IArticleRepository articleRepository = null;
        private IXsUserRepository userRepository = null;


        public ArticlesController()
        {

            articleRepository = new MockWikiArticleRepository();
            userRepository = new MockXsUserRepository();
        }

        public ArticlesController(IArticleRepository wikiArticleRepository)
        {
            articleRepository = wikiArticleRepository;
        }

        //
        // GET: /Articles/

        public ActionResult Index()
        {
            IEnumerable<WikiArticle> articles
                = from article in articleRepository.GetAll()
                  orderby article.UpdateDT descending
                  select article;

            return View(articles);
        }

        //
        // GET: /Articles/Details/5

        public ActionResult Details(long id,String seoName)
        {
            WikiArticle articleToView = null;

            try
            {
                articleToView = articleRepository.GetById(id);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Article GetById({0}) error: ", id) + ex.Message);
            }

            if (null == articleToView)
            {
                ViewData["Entity"] = "Article";
                ViewData["EntityID"] = id;
                return View("NotFound");
            }


            return View(articleToView);
        }

        //
        // GET: /Articles/Create

        public ActionResult Create()
        {
            WikiArticle article = new WikiArticle();

            return View(article);
        } 

        //
        // POST: /Articles/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "ID")] WikiArticle articleToCreate, String button)
        {
            if (button.Equals("SaveDraft"))
            {
                articleToCreate.Status = XsStatus.Draft;
            }
            else
            {
                articleToCreate.Status = XsStatus.Published;
                articleToCreate.PublishedDT = DateTime.UtcNow;
            }

            ModelState.AddModelErrors(articleToCreate.GetRuleViolations());

            if (ModelState.IsValid)
            {

                try
                {
                    //TODO: to add other article attributes 
                    articleToCreate.Author = userRepository.GetByUsername(User.Identity.Name);
                    
                    articleRepository.Save(articleToCreate);

                    ViewData["ArticleTitle"] = articleToCreate.Title;
                    return View("Confirm");
                }
                catch (Exception ex)
                {
                    log.Error("There was an error when saving the article in the DB " + ex.Message);
                    ViewData["ErrorMessage"] = "There was an error when saving the article in the DB!";
                    return View("Error");
                    
                }
            }
            return View();
        }

       
        //
        // GET: /Articles/Edit/5
 
        public ActionResult Edit(long id, String seoName)
        {
            WikiArticle articleToEdit = null;

            try
            {
                articleToEdit = articleRepository.GetById(id);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Article GetById({0}) error: ",id) + ex.Message);
            }

            if (null == articleToEdit)
            {
                ViewData["Entity"] = "Article";
                ViewData["EntityID"] = id;
                return View("NotFound");
            }


            return View(articleToEdit);
        }

        //
        // POST: /Articles/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(long id, FormCollection collection)
        {
            WikiArticle articleToSave = null;

            try
            {
                articleToSave = articleRepository.GetById(id);
                UpdateModel(articleToSave);
                articleToSave.UpdateDT = DateTime.UtcNow;

                articleRepository.Save(articleToSave);
            
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
