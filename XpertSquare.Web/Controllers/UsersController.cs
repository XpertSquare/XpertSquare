using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using XpertSquare.Core.Model;
using XpertSquare.Core;
using XpertSquare.Web.Core;
using XpertSquare.Core.Repository;
using XpertSquare.Data.Mocks;
using XpertSquare.Data.NH.Repository;
using XpertSquare.Web.Helpers;

using log4net;

namespace XpertSquare.Web.Controllers
{
    public class UsersController : XsController
    {

        private IXsUserRepository userRepository = null;
        private IQuestionRepository questionRepository = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsersController));        

        public UsersController()
        {
            userRepository = new UserRepository();
            questionRepository = new QuestionRepository();
        }

        public UsersController(IRepositoryFactory repositoryFactory)
        {
            userRepository = repositoryFactory.GetUserRepository();
            questionRepository = repositoryFactory.GetQuestionRepository();
        }

        //
        // GET: /Users/

        public ActionResult Index(int? page)
        {
            Int32 currentPage = page ?? 1;
            IQueryable<XsUser> users = userRepository.GetAll();
            IPagination pageUsers = users.AsPagination(currentPage, Settings.USERS_PAGINATION_SIZE);

            Int32 currentPageSize = Settings.USERS_PAGINATION_SIZE;

            if (currentPage == pageUsers.TotalPages)
            {
                currentPageSize = pageUsers.TotalItems - (pageUsers.TotalPages - 1) * Settings.USERS_PAGINATION_SIZE;
            }

            ViewData["CurrentPageSize"] = currentPageSize;

             ViewData["users"] = pageUsers;

            return View(ViewData["users"]);
        }


        public ActionResult Details(long id)
        {
            XsUser user = userRepository.GetById(id);
            if (null == user)
            {
                ViewData["EntityID"] = id;
                return View("NotFound");
            }
            ViewData["IsAllowedToEdit"] = false;
            if (User.Identity.IsAuthenticated && User.Identity.Name.Equals(user.Username))
            {
                ViewData["IsAllowedToEdit"] = true;
            }

            IQueryable<XsQuestion> userQuestions = questionRepository.GetUserQuestions(id);
            ViewData["UserQuestion"] = userQuestions.AsEnumerable();

            return View(user);
        }

        //
        // GET: /users/edit/5/marius-serban

        public ActionResult Edit(long id, String seoName)
        {
           XsUser userToEdit = null;

            try
            {
                userToEdit = userRepository.GetById(id);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("User GetById({0}) error: ", id) + ex.Message);
            }

            if (null == userToEdit)
            {
                ViewData["Entity"] = "User";
                ViewData["EntityID"] = id;
                return View("NotFound");
            }
            if (User.Identity.IsAuthenticated && User.Identity.Name.Equals(userToEdit.Username))
            {
                return View(userToEdit);
            }
            else
            {
                return RedirectToAction("Details", new { id = id, seoName = Utils.GetSlug(UserHelper.GetDisplayNameForGUI(userToEdit)) });
            }
        }

        //
        // POST: /users/edit/5

        [AcceptVerbs(HttpVerbs.Post), ValidateAntiForgeryToken()]
        [ValidateInput(false)]
        public ActionResult Edit(long id, FormCollection collection, String button)
        {
            if (button.Equals("Cancel"))
            {
                return RedirectToAction("Details", new { id = id, seoName = Utils.GetSlug(collection["DisplayName"]) });
            }

            else
            {
                XsUser userToSave = null;

                try
                {
                    userToSave = userRepository.GetById(id);
                    UpdateModel(userToSave);
                    userToSave.UpdateDT = DateTime.UtcNow;
                    userToSave.Description = userToSave.Description.Replace("<script", "[script").Replace("</script>", "[/script]");

                    userRepository.Save(userToSave);

                    return RedirectToAction("Details", new { id = userToSave.ID, seoName =  Utils.GetSlug(UserHelper.GetDisplayNameForGUI(userToSave)) });
                }
                catch
                {
                    return View();
                }
            }
        }

        public JsonResult GridData(string sidx, string sord, int page, int rows)
        {


            IQueryable<XsUser> users = userRepository.GetAll();


            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = users.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var gridUsers = users
              .Skip(pageIndex * pageSize)
              .Take(pageSize);



            var jsonData = new
            {
                total = totalPages, // we'll implement later 
                page = page,
                records = totalRecords, // implement later 
                rows = (from user in gridUsers
                        select new
                        {
                            i = user.ID,
                            cell = new string[] {
                                user.ID.ToString()
                                , user.FirstName
                                , user.LastName
                                , user.DisplayName}
                        }).ToArray()
            };

            var result = Json(jsonData);
            return result;
        }
    }
}