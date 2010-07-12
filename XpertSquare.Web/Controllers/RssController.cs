using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;

using XpertSquare.Web.Core;
using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;
using XpertSquare.Data.NH.Repository;

using log4net;


namespace XpertSquare.Controllers
{
    public class RssController : XsController
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(RssController));

        private IQuestionRepository questionRepository = null;

        public RssController()
        {
            questionRepository = new QuestionRepository();
        }

        public RssController(IRepositoryFactory repositoryFactory)
        {
            questionRepository = repositoryFactory.GetQuestionRepository();
        }

        //
        // GET: /Rss/
        public ActionResult Feed()
        {
            SyndicationFeed feed =
        new SyndicationFeed(Settings.SITE_TITLE,
                            Settings.SITE_TITLE,
                            new Uri("http://localhost"),
                            Settings.SITE_TITLE,
                            DateTime.Now);

            List<SyndicationItem> items = new List<SyndicationItem>();

            IEnumerable<XsQuestion> rssQuestions =
                from q in questionRepository.GetAll()
                orderby q.CreationDT descending
                select q;

            foreach (XsQuestion q in rssQuestions)
            {
                SyndicationItem item = new SyndicationItem();
                item.Title = new TextSyndicationContent(q.Title);
                item.Authors.Add(new SyndicationPerson() { Name = q.Author.DisplayName ?? "<Anonymous>" });
                item.Copyright = new TextSyndicationContent(Settings.SITE_TITLE);
                item.Id = q.ID.ToString();
                item.LastUpdatedTime = new DateTimeOffset(q.UpdateDT);
                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri( 
                    String.Concat("http://"
                    , Request.Url.Host
//                    , HttpRuntime.AppDomainAppVirtualPath
                    , Url.Action("Details", "Questions", new { id = q.ID, seoName = q.SlugTitle }))
                    )
                    , "text/html" ));


                items.Add(item);

            }

            feed.Items = items;


            return new RssActionResult() { Feed = feed };
        }

    }
}
