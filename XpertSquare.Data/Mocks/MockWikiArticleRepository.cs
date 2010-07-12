using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;

namespace XpertSquare.Data.Mocks
{
    public class MockWikiArticleRepository : IArticleRepository
    {
        /// <summary>
        /// Mock data for article         
        /// </summary>

        private static  WikiArticle mockArticle = null;

        private static IList<WikiArticle> articles = null;

        private const String ENTITY_TYPE = "Article";

        public MockWikiArticleRepository()
        {
            if (null == mockArticle)
            {
                mockArticle = new WikiArticle();
                XsUser mockUser = new MockXsUserRepository().GetById(100);

                mockArticle.ID = IdGenerator.GetNextID(ENTITY_TYPE);
                mockArticle.CreationDT = DateTime.UtcNow;
                mockArticle.UpdateDT = DateTime.UtcNow;
                mockArticle.LastUpdator = "Marius";
                mockArticle.Title = "What Was Stack Overflow Built With?";
                mockArticle.Content = "What was Stack Overflow built with? Some even wondered if Stack Overflow was built in Ruby on Rails. I consider that a compliment! This question has been covered in some detail in our podcasts, of course, but I know not everyone has time to listen to a bunch of audio footage to find the answer to their question. So, in that spirit, here’s the technology “stack” of Stack Overflow, the stuff Jarrod, Geoff, and I used to build it:";

                mockArticle.Author = mockUser;
                mockArticle.Status = XsStatus.Published;
                mockArticle.PublishedDT = DateTime.UtcNow;
                XsTag tag1 = new XsTag();
                tag1.Name = ".NET";
                mockArticle.AddTag(tag1);

                XsTag tag2 = new XsTag();
                tag2.Name = "MVC";
                mockArticle.AddTag(tag2);
            }

            if (null == articles)
            {

                articles = new List<WikiArticle>();

                for (int i = 0; i <= 100; i++)
                {
                    WikiArticle article = new WikiArticle();

                    int nextID = IdGenerator.GetNextID(ENTITY_TYPE);
                    article.ID = nextID;
                    article.LastUpdator = "Marius " + String.Format("{000}", nextID);
                    article.Title = "What Was Stack Overflow Built With? " + String.Format("{000}", nextID);
                    article.Content = "What was Stack Overflow built with? Some even wondered if Stack Overflow was built in Ruby on Rails. I consider that a compliment! This question has been covered in some detail in our podcasts, of course, but I know not everyone has time to listen to a bunch of audio footage to find the answer to their question. So, in that spirit, here’s the technology “stack” of Stack Overflow, the stuff Jarrod, Geoff, and I used to build it:";

                    article.Author = new MockXsUserRepository().GetById(nextID);
                    article.Status = XsStatus.Published;
                    article.PublishedDT = DateTime.UtcNow;

                    XsTag tag1 = new XsTag();
                    tag1.Name = ".Net " + String.Format("{000}", nextID);
                    article.AddTag(tag1);

                    XsTag tag2 = new XsTag();
                    tag2.Name = "MVC  " + String.Format("{000}", nextID);
                    article.AddTag(tag2);                    
                    articles.Add(article);
                }
            }
        }

        #region IRepository<WikiArticle,long> Members

        public WikiArticle GetById(long id)
        {
            WikiArticle article = null;

            article = (from a in articles.AsQueryable()
                       where (a.ID == id)
                       select a).First();

            if (null == article)
            {
                article = mockArticle;
            }


            article.ID = id;
            return article;
        }

        public IQueryable<WikiArticle> GetAll()
        {
            return articles.AsQueryable();
        }

        public IQueryable<WikiArticle> GetByExample(WikiArticle exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        public WikiArticle GetUniqueByExample(WikiArticle exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        public WikiArticle Save(WikiArticle entity)
        {
            if (0 == entity.ID)
            {
                entity.ID = IdGenerator.GetNextID(ENTITY_TYPE); ;
            }
            articles.Add(entity);
            return entity;
        }

        public WikiArticle SaveOrUpdate(WikiArticle entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(WikiArticle entity)
        {
            throw new NotImplementedException();
        }

        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
