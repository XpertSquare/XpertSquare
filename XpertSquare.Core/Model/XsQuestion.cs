using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    public class XsQuestion : XsCore
    {

        private bool _toAddToSearchIndex = false;
        public virtual IList<XsQuestionHistory> History { get; set; }
        public virtual IList<XsAnswer> Answers { get; set; }
        public virtual XsAnswer AcceptedAnswer { get; set; }
        
        #region Constructors
        public XsQuestion()
        {
            History = new List<XsQuestionHistory>();
            Answers = new List<XsAnswer>();
        }
        #endregion

        #region Public Methods

        public virtual void AddToHistory(XsQuestionHistory questionHistory)
        {
            questionHistory.ID = Guid.NewGuid();
            History.Add(questionHistory);
        }

        public virtual void AddAnswer(XsAnswer answer)
        {
            if (!Answers.Contains(answer))
            {
                answer.Parent = this;
                Answers.Add(answer);
            }
        }

        public virtual void AddVote(XsVote vote)
        {
            if (!Votes.Contains(vote))
            {
                vote.Question = this;
                Votes.Add(vote);
            }
        }

        /// <summary>
        /// Adds a tag to the collection of tags. The same tag will not be added twice (it will be silently ignored)
        /// </summary>
        /// <param name="tag"></param>
        public virtual void AddTag(XsTag tag)
        {
            if (!Tags.Contains(tag))
            {
                tag.AddQuestion(this);
                Tags.Add(tag);
            }
        }

        public virtual Int16 TotalVoteValue
        {
            get
            {
                Int16 voteValue = 0;
                foreach (XsVote vote in Votes)
                {
                    voteValue += vote.Value;
                }
                return voteValue;
            }
        }
        
        public virtual bool IsValid()
        {
            return (GetRuleViolations().Count() == 0);
        }

        public virtual IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("The question title is required", "Title");

            if ((null != Title) && (Title.Length > 150))
                yield return new RuleViolation("The question title length is longer than 150 characters", "Title");

            if (String.IsNullOrEmpty(Content))
                yield return new RuleViolation("The question content is required", "Content");

            if ((null != Content) && (Content.Length > 2000))
                yield return new RuleViolation("The question content lenght is longer than 2000 characters", "Content");


            if ((null != ContentHtml) && (ContentHtml.Length > 2000))
                yield return new RuleViolation("The question content lenght is longer than 2000 characters", "Content");

            if (0 == Tags.Count)
            {
                yield return new RuleViolation("The question needs to have at least one tag", "Tags");
            }

            if (Tags.Count>5)
            {
                yield return new RuleViolation("The question cannot have more than 5 tags", "Tags");
            }
            foreach (var tag in Tags)
            {
                if (tag.Name.Length>30)
                {
                    yield return new RuleViolation("Each tag cannot be longer than 30 characters", "Tags");
                }
            }

            yield break;
        }

        public virtual void RemoveAllTags()
        {
            Int32 tagsCount = Tags.Count;
            for (Int32 tagIndex = 0; tagIndex < tagsCount; tagIndex++)
            {
                XsTag tag = Tags.ElementAt(0);
                tag.RemoveQuestion(this);
                Tags.Remove(tag);
            }
        }

        public virtual bool ToAddToSearchIndex
        {
            get { return _toAddToSearchIndex; }
            set { _toAddToSearchIndex = value; }
        }

        #endregion

    }
}
