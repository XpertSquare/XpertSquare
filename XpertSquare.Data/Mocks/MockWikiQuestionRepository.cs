using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;

namespace XpertSquare.Data.Mocks
{
    /// <summary>
    /// Mock data for questions
    /// </summary>
    public class MockXsQuestionRepository :  IQuestionRepository
    {
        private static  XsQuestion mockQuestion = null;

        private static IList<XsQuestion> questions = null;

        private const String ENTITY_TYPE = "Question";

        public MockXsQuestionRepository()
        {
            if (null == mockQuestion)
            {
                mockQuestion = new XsQuestion();
                XsUser mockUser = new MockXsUserRepository().GetById(100);

                mockQuestion.ID = IdGenerator.GetNextID(ENTITY_TYPE);
                mockQuestion.CreationDT = DateTime.UtcNow;
                mockQuestion.UpdateDT = DateTime.UtcNow;
                mockQuestion.LastUpdator = "Marius";
                mockQuestion.Title = "What Was Stack Overflow Built With?";
                mockQuestion.Content = "What was Stack Overflow built with? Some even wondered if Stack Overflow was built in Ruby on Rails. I consider that a compliment! This question has been covered in some detail in our podcasts, of course, but I know not everyone has time to listen to a bunch of audio footage to find the answer to their question. So, in that spirit, here’s the technology “stack” of Stack Overflow, the stuff Jarrod, Geoff, and I used to build it:";

                mockQuestion.Author = mockUser;
                mockQuestion.Status = XsStatus.Published;
                mockQuestion.PublishedDT = DateTime.UtcNow;
                XsTag tag1 = new XsTag();
                tag1.Name = ".NET";
                mockQuestion.AddTag(tag1);

                XsTag tag2 = new XsTag();
                tag2.Name = "MVC";
                mockQuestion.AddTag(tag2);
            }

            if (null == questions)
            {

                questions = new List<XsQuestion>();

                for (int i = 0; i <= 100; i++)
                {
                    XsQuestion question = new XsQuestion();

                    int nextID = IdGenerator.GetNextID(ENTITY_TYPE);
                    question.ID = nextID;

                    question.LastUpdator = "Marius " + String.Format("{000}", nextID);
                    question.Title = "What Was Stack Overflow Built With? " + String.Format("{000}", nextID);
                    question.Content = "What was Stack Overflow built with? Some even wondered if Stack Overflow was built in Ruby on Rails. I consider that a compliment! This question has been covered in some detail in our podcasts, of course, but I know not everyone has time to listen to a bunch of audio footage to find the answer to their question. So, in that spirit, here’s the technology “stack” of Stack Overflow, the stuff Jarrod, Geoff, and I used to build it:";

                    question.Author = new MockXsUserRepository().GetById(nextID);
                    question.Status = XsStatus.Published;
                    question.PublishedDT = DateTime.UtcNow;

                    //tags
                    XsTag tag1 = new XsTag();
                    tag1.Name = ".Net " + String.Format("{000}", nextID);
                    question.AddTag(tag1);

                    XsTag tag2 = new XsTag();
                    tag2.Name = "MVC  " + String.Format("{000}", nextID);
                    question.AddTag(tag2);

                    //votes
                    int numberOfVotes = new Random().Next(5, 20);
                    for (int voteIndex = 0; voteIndex < numberOfVotes; voteIndex++)
                    {
                        XsVote vote = new XsVote();
                        vote.Type = VoteType.UpDown;
                        vote.User = new MockXsUserRepository().GetById(nextID);
                        vote.Value = Convert.ToInt16( voteIndex % 2);
                        question.AddVote(vote);
                    }

                    int numberOfAnswers = new Random().Next(10);
                    for (int answerIndex = 0; answerIndex < numberOfAnswers; answerIndex++)
                    {
                        XsAnswer answer = new XsAnswer();
                        answer.Content = "What do you really want to do? If you want to learn Clojure||ruby||C do that. If you just want to get it done do whatever is fastest for you to do. And at the very least when you say Clojure and library you are also saying Java and library, there are lots and some are very good(I don't know what they are though). And the same was said for ruby and python above. So what do you want to do?";
                        answer.Author = question.Author = new MockXsUserRepository().GetById(nextID);
                        answer.ID = IdGenerator.GetNextID("Answer");

                        question.AddAnswer(answer);
                    }

                    questions.Add(question);
                }
            }
        }

        #region IRepository<XsQuestion,long> Members

        public XsQuestion GetById(long id)
        {
            XsQuestion question = null;

            question = (from a in questions.AsQueryable()
                       where (a.ID == id)
                       select a).First();

            if (null == question)
            {
                question = mockQuestion;
            }            
            return question;
        }

        public IQueryable<XsQuestion> GetAll()
        {
            return questions.AsQueryable();
        }

        public IQueryable<XsQuestion> GetByExample(XsQuestion exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        public XsQuestion GetUniqueByExample(XsQuestion exampleInstance, params string[] propertiesToExclude)
        {
            throw new NotImplementedException();
        }

        public XsQuestion Save(XsQuestion entity)
        {
            if (0 == entity.ID)
            {
                entity.ID = IdGenerator.GetNextID(ENTITY_TYPE); ;
            }
            questions.Add(entity);
            return entity;
        }

        public XsQuestion SaveOrUpdate(XsQuestion entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(XsQuestion entity)
        {
            throw new NotImplementedException();
        }

        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public IQueryable<XsQuestion> GetUserQuestions(long userID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
