using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using XpertSquare.Core;
using XpertSquare.Core.Model;
using XpertSquare.Web.Core;
using XpertSquare.Web.Helpers;
using XpertSquare.Core.Repository;

using log4net;

using XpertSquare.Data.Mocks;
using XpertSquare.Data.NH;
using XpertSquare.Data.NH.Repository;
using XpertSquare.Web.Models;

namespace XpertSquare.Web.Controllers
{
    public class TagsController : XsController
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(TagsController));

        private ITagRepository tagRepository = null;


         public TagsController()
        {

            tagRepository = new TagRepository();
        }

         public TagsController( IRepositoryFactory  repositoryFactory)
        {
            tagRepository = repositoryFactory.GetTagRepository();
        }


         //
         // GET: /Tags/

         public ActionResult Index(int? pg)
        {

            Int32 currentPage = pg ?? 1;
            IQueryable<XsTag> tags = tagRepository.GetTagsByQuestionTotal().AsQueryable();
            IPagination pageTags = tags.AsPagination(currentPage, Settings.TAGS_PAGINATION_SIZE);

            Int32 currentPageSize = Settings.TAGS_PAGINATION_SIZE;

            if (currentPage == pageTags.TotalPages)
            {
                currentPageSize = pageTags.TotalItems - (pageTags.TotalPages - 1) * Settings.TAGS_PAGINATION_SIZE;
            }
            else 
            {
                if (0 == pageTags.TotalPages)
                {
                    currentPageSize = 0;
                }
            }

            TagsIndexViewModel viewModel = new TagsIndexViewModel() { CurrentPageSize = currentPageSize, Tags = pageTags };

            return View(viewModel);
        }
    }
}
