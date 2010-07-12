using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using XpertSquare.Core;
using XpertSquare.Core.Model;
using XpertSquare.Core.Search;
using XpertSquare.Web.Core;
using XpertSquare.Web.Helpers;
using XpertSquare.Core.Repository;
using XpertSquare.Data.Mocks;
using XpertSquare.Data.NH;
using XpertSquare.Data.NH.Repository;
using XpertSquare.Web.Models;

using log4net;



namespace XpertSquare.Controllers
{
    public class QuestionsController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionsController));

        private IQuestionRepository questionRepository = null;
        private IXsUserRepository userRepository = null;
        private ITagRepository tagRepository = null;
        private IAnswerRepository answerRepository = null;
        private IVoteRepository voteRepository = null;

        private const String ENTITY_TYPE_ANSWER = "Answer";
        private const String ENTITY_TYPE_QUESTION = "Question";
        private const String VOTE_TYPE_UP = "Up";
        private const String VOTE_TYPE_DOWN = "Down";
        private const String VOTE_ACTION_RECALL = "recall-vote";

        private const Int16 VOTE_UP_VALUE = 1;
        private const Int16 VOTE_DOWN_VALUE = -1;


         public QuestionsController()
        {

            questionRepository = new QuestionRepository();
            userRepository = new UserRepository();
            tagRepository = new TagRepository();
            answerRepository = new AnswerRepository();
            voteRepository = new VoteRepository();
        }

         public QuestionsController(IRepositoryFactory repositoryFactory )
        {
            questionRepository = repositoryFactory.GetQuestionRepository();
            userRepository = repositoryFactory.GetUserRepository();
            tagRepository = repositoryFactory.GetTagRepository();
            answerRepository = repositoryFactory.GetAnswerRepository();
            voteRepository = repositoryFactory.GetVoteRepository();
        }


        //
        // GET: /Questions/

         public ActionResult Index(int? pg)
         {

             Int32 currentPage = pg ?? 1;
             IQueryable<XsQuestion> questions =
                 from question in questionRepository.GetAll()
                 orderby question.UpdateDT descending
                 select question;

             IPagination pageQuestions = questions.AsPagination(currentPage, Settings.QUESTIONS_PAGINATION_SIZE);
             Int32 currentPageSize = Settings.QUESTIONS_PAGINATION_SIZE;

             if (pg > pageQuestions.TotalPages) pg = 1;

             if (currentPage == pageQuestions.TotalPages)
             {
                 currentPageSize = pageQuestions.TotalItems - (pageQuestions.TotalPages - 1) * Settings.QUESTIONS_PAGINATION_SIZE;
             }
             else
             {
                 if (0 == pageQuestions.TotalPages)
                 {
                     currentPageSize = 0;
                 }
             }

             TagsIndexViewModel viewModel = new TagsIndexViewModel() { CurrentPageSize = currentPageSize, Tags = pageQuestions };

             IList<XsTag> popularTags = tagRepository.GetTagsByQuestionTotal().Take(Settings.POPULAR_TAGS_LIMIT).ToList();

             QuestionsIndexViewModel questionsViewModel = new QuestionsIndexViewModel()
             {
                 Questions = pageQuestions,
                 PopularTags = popularTags,
                 CurrentPageSize = currentPageSize
             };

             return View(questionsViewModel);
         }

        //
        // GET: /Questions/Details/5/What-is-what

        public ActionResult Details(long id, String seoName)
        {
            XsQuestion questionToView = null;
            QuestionViewModel questionViewModel = null;
            try
            {
                questionToView = questionRepository.GetById(id);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Question GetById({0}) error: ", id) + ex.Message);
            }

            if (null == questionToView)
            {
                ViewData["EntityID"] = id;
                return View("NotFound");
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewData["IsAuthenticated"] = true;
            }

            IList<XsQuestion> relatedQuestions = new List<XsQuestion>();

            ISearchEngineService searchService = new SearchEngineService(questionRepository);
            relatedQuestions = searchService.SearchRelatedQuestions(questionToView);

            questionViewModel = new QuestionViewModel() { Question = questionToView, RelatedQuestions = relatedQuestions };

            return View(questionViewModel);
        }

        //POST: Questions/Answer

        [AcceptVerbs(HttpVerbs.Post), ValidateAntiForgeryToken()]
        [ValidateInput(false)]
        public ActionResult Answer(long ID, String AnswerContent)
        {
            XsQuestion questionToUpdate = null;

            try
            {
                questionToUpdate = questionRepository.GetById(ID);               
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Question GetById({0}) error: ", ID) + ex.Message);
            }

            if (null == questionToUpdate)
            {
                ViewData["EntityID"] = ID;
                return View("NotFound");
            }
            else
            {
                try
                {
                    XsAnswer answer = new XsAnswer();
                    answer.CreationDT = DateTime.UtcNow;
                    answer.UpdateDT = DateTime.UtcNow;
                    answer.Content = AnswerContent.Replace("<script", "[script").Replace("</script>", "[/script]");
                    answer.ContentHtml = new Markdown().Transform(answer.Content);

                    XsUser author = userRepository.GetByUsername(User.Identity.Name);

                    if (null == author)
                    {
                        author = new XsUser();
                        author.Username = User.Identity.Name;
                        author.AnswerCount = 1;
                    }
                    author.AnswerCount++;
                    answer.Author = author;

                    questionToUpdate.AddAnswer(answer);
                    questionRepository.Save(questionToUpdate);
                }
                catch (Exception ex)
                {
                    log.Error("There was an error when saving the question in the DB " + ex.Message + "Inner Exception " + ex.InnerException);
                    ViewData["ErrorMessage"] = "There was an error when saving the question in the DB!";
                    return View("Error");
                }
            }


            return RedirectToAction("Details", new { id = ID, seoName = questionToUpdate.SlugTitle });
        }

        //
        // GET: /Questions/Create

        public ActionResult Create()
        {
            XsQuestion question = new XsQuestion();

            return View(question);
        } 

        //
        // POST: /Questions/Create

        [AcceptVerbs(HttpVerbs.Post), ValidateAntiForgeryToken()]
        [ValidateInput(false)]
        public ActionResult Create(XsQuestion questionToCreate, String button, String QuestionTags)
        {
            questionToCreate.CreationDT = DateTime.UtcNow;
            questionToCreate.UpdateDT = DateTime.UtcNow;            

            if (button.Equals("SaveDraft"))
            {
                questionToCreate.Status = XsStatus.Draft;
            }
            else
            {
                questionToCreate.Status = XsStatus.Published;
                questionToCreate.PublishedDT = DateTime.UtcNow;
            }

            

            String tags = RemoveExtraSpaces(QuestionTags);

            String[] qTags = tags.Split(' ');

            foreach (String tag in qTags)
            {
                if (!String.IsNullOrEmpty(tag))
                {
                    String tagInvariantName = tag.ToLowerInvariant();

                    XsTag currentTag = tagRepository.GetTagByName(tagInvariantName);

                    if (null == currentTag)
                    {
                        currentTag = new XsTag();
                        currentTag.Name = tagInvariantName;
                    }                        
                    questionToCreate.AddTag(currentTag);
                }
            }
            questionToCreate.Content = questionToCreate.Content.Replace("<script", "[script").Replace("</script>", "[/script]");
            questionToCreate.ContentHtml = new Markdown().Transform(questionToCreate.Content);
            questionToCreate.Excerpt = Utils.CreateContentExcerpt(questionToCreate.ContentHtml);

            ModelState.AddModelErrors(questionToCreate.GetRuleViolations());

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: to add other article attributes 
                    XsUser author = userRepository.GetByUsername(User.Identity.Name);

                    if (null == author)
                    {
                        author = new XsUser();                       
                        author.Username = User.Identity.Name;
                        author.QuestionCount = 1;
                    }
                    author.QuestionCount++;
                    questionToCreate.Author = author;
                    questionRepository.Save(questionToCreate);


                    ViewData["QuestionTitle"] = questionToCreate.Title;
                    return View("Confirm");
                }
                catch (Exception ex)
                {
                    log.Error("There was an error when saving the question in the DB " + ex.Message + "Inner Exception " + ex.InnerException );
                    ViewData["ErrorMessage"] = "There was an error when saving the question in the DB!";
                    return View("Error");

                }
            }
            return View();
        }

        //
        // GET: /Questions/Edit/5/What-is-what
 
        public ActionResult Edit(long id, String seoName)
        {
            XsQuestion questionToEdit = null;

            try
            {
                questionToEdit = questionRepository.GetById(id);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Question GetById({0}) error: ", id) + ex.Message);
            }

            if (null == questionToEdit)
            {
                ViewData["Entity"] = "Question";
                ViewData["EntityID"] = id;
                return View("NotFound");
            }


            return View(questionToEdit);
        }

        //
        // POST: /Questions/Edit/5/what-is-what

        [AcceptVerbs(HttpVerbs.Post), ValidateAntiForgeryToken()]
        [ValidateInput(false)]
        public ActionResult Edit(long id, XsQuestion questionToSave, FormCollection collection)
        {            

            try
            {
                questionToSave = questionRepository.GetById(id);

                if (null != questionToSave)
                {
                    questionToSave.RemoveAllTags();

                    questionToSave.Tags = new List<XsTag>();

                    String tags = RemoveExtraSpaces(collection["QuestionTags"].ToString());

                    String[] qTags = tags.Split(' ');

                    foreach (String tag in qTags)
                    {
                        if (!String.IsNullOrEmpty(tag))
                        {
                            String tagInvariantName = tag.ToLowerInvariant();
                            XsTag currentTag = tagRepository.GetTagByName(tagInvariantName);

                            if (null == currentTag)
                            {
                                currentTag = new XsTag();
                                currentTag.Name = tagInvariantName;
                            }
                            questionToSave.AddTag(currentTag);
                        }
                    }

                    questionToSave.Title = collection["Title"].ToString();
                    questionToSave.Content = collection["Content"].ToString();                   

                    questionToSave.ContentHtml = new Markdown().Transform(questionToSave.Content);
                    questionToSave.Excerpt = Utils.CreateContentExcerpt(questionToSave.ContentHtml);
                    questionToSave.UpdateDT = DateTime.UtcNow;

                    ModelState.AddModelErrors(questionToSave.GetRuleViolations());

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            questionToSave.ToAddToSearchIndex = true;
                            questionRepository.Save(questionToSave);
                            return RedirectToAction("Details", "Questions", new { id = questionToSave.ID, seoName = questionToSave.SlugTitle });
                        }
                        catch (Exception ex)
                        {
                            log.Error("There was an error when saving the question in the DB " + ex.Message + "Inner Exception " + ex.InnerException);
                            ViewData["ErrorMessage"] = "There was an error when saving the question in the DB!";
                            return View("Error");

                        }
                    }
                    else
                    {
                        return View(questionToSave);
                    }
                }
            }
            catch
            {
                return View(questionToSave);
            }

            return View(questionToSave);
        }

        public ActionResult Tagged(String tag, int? pg)
        {
            Int32 currentPage = pg ?? 1;            
            IPagination questionsTagged = null;
            XsTag questionTag = null;

            if (!String.IsNullOrEmpty(tag))
            {
                questionTag = tagRepository.GetTagByName(tag);
            }

             if (null != questionTag)
            {
                questionsTagged = questionTag.Questions.AsPagination(currentPage,Settings.QUESTIONS_PAGINATION_SIZE);
            }

            Int32 currentPageSize = Settings.TAGS_PAGINATION_SIZE;
            if (currentPage == questionsTagged.TotalPages)
            {
                currentPageSize = questionsTagged.TotalItems - (questionsTagged.TotalPages - 1) * Settings.TAGS_PAGINATION_SIZE;
            }
            else
            {
                if (0 == questionsTagged.TotalPages)
                {
                    currentPageSize = 0;
                }
            }

            TaggedViewModel viewModel = new TaggedViewModel()
                {
                    CurrentPageSize = currentPageSize,
                    Questions = questionsTagged,
                    TagName = tag ?? String.Empty

                };
            return View(viewModel);
        }

        #region Voting


        [AcceptVerbs(HttpVerbs.Post), ValidateAntiForgeryToken]
        public ActionResult Vote(String EntityType, long EntityID, String VoteType, String Username, String VoteAction)
        {
            Int16 voteValue = 0;

            XsUser user = userRepository.GetByUsername(Username);
            if (null == user)
            {
                log.Error("User " + Username + " does not exist in the DB");
                throw new Exception("You need to be registered to vote!");
            }

            if (EntityType.Equals(ENTITY_TYPE_QUESTION))
            {
                if (VoteType.Equals(VOTE_TYPE_UP))
                {
                    voteValue = RegisterQuestionVoteUp(EntityID, Username, VoteAction);
                }
                else
                {
                    voteValue = RegisterQuestionVoteDown(EntityID, Username, VoteAction);
                }
            }
            else
            {
                if (VoteType.Equals(VOTE_TYPE_UP))
                {
                    voteValue = RegisterAnswerVoteUp(EntityID, Username, VoteAction);
                }
                else
                {
                    voteValue = RegisterAnswerVoteDown(EntityID, Username, VoteAction);
                }
            }

            return Content(voteValue.ToString());
        }

        private short RegisterAnswerVoteDown(long answerID, String username, String voteAction)
        {
            XsAnswer answer = answerRepository.GetById(answerID);

            XsVote duplicateVote = null;

            if (null == answer)
            {
                String errText = "Answer ID " + answerID.ToString() + " does not exist in the DB";
                log.Error(errText);
                throw new Exception(errText);
            }

            if (voteAction.Equals(VOTE_ACTION_RECALL))
            {
                foreach (XsVote vote in answer.Votes)
                {
                    if ((vote.Answer.ID == answerID)
                        && (vote.User.Username.Equals(username)) && (VOTE_DOWN_VALUE == vote.Value))
                    {
                        duplicateVote = vote;
                        answer.Votes.Remove(duplicateVote);
                        voteRepository.Delete(duplicateVote);
                        break;
                    }
                }
            }
            else
            {

                XsVote vote = new XsVote();
                vote.Type = VoteType.UpDown;
                vote.User = userRepository.GetByUsername(username);
                vote.Value = VOTE_DOWN_VALUE;

                answer.AddVote(vote);
                answerRepository.Save(answer);
            }           

            return answer.TotalVoteValue;
        }

        private Int16 RegisterAnswerVoteUp(long answerID, String username, String voteAction)
        {
            XsAnswer answer = answerRepository.GetById(answerID);
            XsVote duplicateVote = null;

            if (null == answer)
            {
                String errText = "Answer ID " + answerID.ToString() + " does not exist in the DB";
                log.Error(errText);
                throw new Exception(errText);
            }

            if (voteAction.Equals(VOTE_ACTION_RECALL))
            {
                foreach (XsVote vote in answer.Votes)
                {
                    if ((vote.Answer.ID == answerID)
                        && (vote.User.Username.Equals(username)) && (VOTE_UP_VALUE == vote.Value))
                    {                        
                        duplicateVote = vote;
                        answer.Votes.Remove(duplicateVote);
                        voteRepository.Delete(duplicateVote);
                        break;
                    }
                }
            }
            else
            {

                XsVote vote = new XsVote();
                vote.Type = VoteType.UpDown;
                vote.User = userRepository.GetByUsername(username);
                vote.Value = VOTE_UP_VALUE;

                answer.AddVote(vote);
                answerRepository.Save(answer);
            }

            return answer.TotalVoteValue;
        }

        private Int16 RegisterQuestionVoteDown(long questionID, String username, String voteAction)
        {
            XsQuestion question = questionRepository.GetById(questionID);
            XsVote duplicateVote = null;

            if (null == question)
            {
                String errText = "Question ID " + questionID.ToString() + " does not exist in the DB";
                log.Error(errText);
                throw new Exception(errText);
            }

            if (voteAction.Equals(VOTE_ACTION_RECALL))
            {
                foreach (XsVote vote in question.Votes)
                {
                    if ((vote.Question.ID == questionID)
                        && (vote.User.Username.Equals(username)) && (VOTE_DOWN_VALUE == vote.Value))
                    {
                        duplicateVote = vote;
                        question.Votes.Remove(duplicateVote);
                        voteRepository.Delete(duplicateVote);
                        break;
                    }
                }
            }
            else
            {
                XsVote vote = new XsVote();
                vote.Type = VoteType.UpDown;
                vote.User = userRepository.GetByUsername(username);
                vote.Value = VOTE_DOWN_VALUE;

                question.AddVote(vote);
                questionRepository.Save(question);
            }

            return question.TotalVoteValue;
        }

        private Int16 RegisterQuestionVoteUp(long questionID, String username, String voteAction)
        {
            XsQuestion question = questionRepository.GetById(questionID);
            XsVote duplicateVote = null;

            if (null == question)
            {
                String errText = "Question ID " + questionID.ToString() + " does not exist in the DB";
                log.Error(errText);
                throw new Exception(errText);
            }

            if (voteAction.Equals(VOTE_ACTION_RECALL))
            {
                foreach (XsVote vote in question.Votes)
                {
                    if ((vote.Question.ID == questionID)
                        && (vote.User.Username.Equals(username)) && (VOTE_UP_VALUE == vote.Value))
                    {
                        duplicateVote = vote;
                        question.Votes.Remove(duplicateVote);
                        voteRepository.Delete(duplicateVote);
                        break;
                    }
                }
            }
            else
            {
                XsVote vote = new XsVote();
                vote.Type = VoteType.UpDown;
                vote.User = userRepository.GetByUsername(username);
                vote.Value = VOTE_UP_VALUE;

                question.AddVote(vote);
                questionRepository.Save(question);
            }

            return question.TotalVoteValue;
        }


        #endregion


        private static string RemoveExtraSpaces(string text)
        {
            if (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
                return RemoveExtraSpaces(text);
            }

            return text;
        }
    }
}
