using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using log4net;
using log4net.Config;

using XpertSquare.Core.Search;
using XpertSquare.Core.Repository;

using XpertSquare.Data.NH.Repository;
using System.Threading;

namespace XpertSquare
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Users routes
            routes.MapRoute(
                "UsersRoute",                                              
                "users",                          
                new { controller = "Users", action = "Index", id = "" }  
            );

            routes.MapRoute(
                "UserGridData",                                  
                "users/GridData",                          
                new { controller = "Users", action = "GridData" }  
            );


            routes.MapRoute(
                "UserDetailsRoute",                                              
                "users/{id}/{seoName}",                        
                new { controller = "Users", action = "Details", id = "", seoName = "" } 
            );

            routes.MapRoute(
               "UsersEditRoute",                                          
               "users/edit/{id}/{seoName}",                          
               new { controller = "Users", action = "Edit", id = "", seoName = "" } 
           );

            #endregion

            #region Questions Route

            routes.MapRoute(
                "QuestionsRoute",                                              
                "questions",                           
                new { controller = "Questions", action = "Index", id = "" }  
            );
           

            routes.MapRoute(
               "QuestionTaggedRoute",                                           
               "questions/tagged/{tag}",                                        
               new { controller = "Questions", action = "Tagged", tag = "" }    
            );

            routes.MapRoute(
                "QuestionCreateRoute",                              
                "questions/create",                                
                new { controller = "Questions", action = "Create" }   
            );

            routes.MapRoute(
                "QuestionVoteRoute",                                  
                "questions/vote",                                     
                new { controller = "Questions", action = "Vote" }     
            );

            routes.MapRoute(
             "QuestionAnswerRoute",                                   
             "questions/answer/{id}",                                 
             new { controller = "Questions", action = "Answer", id = "" }
         );


            routes.MapRoute(
                "QuestionDetailsRoute",                                           
                "questions/{id}/{seoName}",                          
                new { controller = "Questions", action = "Details", id = "", seoName = "" }  
            );

            routes.MapRoute(
               "QuestionEditRoute",                                         
               "questions/edit/{id}/{seoName}",                         
               new { controller = "Questions", action = "Edit", id = "", seoName = "" }  
           );


            #endregion

            #region Home routes
            routes.MapRoute(
                "HomeAboutRoute",                                        
                "about",                                             
                new { controller = "Home", action = "About" }                
            );

            #endregion

            #region Rss route
            routes.MapRoute(
                "Rss",                                                  
                "rss/{action}",                                        
                new { controller = "Rss", action = "Feed" }   
            );
            #endregion

            #region Search route
            routes.MapRoute(
                "SearchQuery",                                                 
                "search/{action}",                                         
                new { controller = "Search", action = "Search" }  
            );           
            #endregion

            routes.MapRoute(
                "CatchAll",                                             
                "{controller}/{action}/{id}",                           
                new { controller = "Questions", action = "Index", id = "" }  
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();

            IQuestionRepository questionRepository = new QuestionRepository();
            ISearchEngineService searchEngine = new SearchEngineService(questionRepository);

            //ThreadStart ts = new ThreadStart(searchEngine.BuildIndex);

            Thread thread = new Thread(searchEngine.BuildIndex);
            thread.Start();

        }
    }
}