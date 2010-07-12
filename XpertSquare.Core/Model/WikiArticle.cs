using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    /// <summary>
    /// Main class for all wikipages/questions
    /// </summary>
    public class WikiArticle : XsCore
    {
        ///=========Fields==============
        ///ID - OK
        ///Title - OK
        ///Content - OK
        ///Tags - list - OK
        ///Creator - OK
        ///Answers
        
        /// TODO        
        ///Comments list
        ///Views
        ///NumberOfVotes        
        ///Status - New / Published / Answered /  
        ///ContentFiles - list
        ///

        

        
        #region Constructors

        public WikiArticle()
        {
            CreationDT = DateTime.UtcNow;
            UpdateDT = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds a tag to the collection of tags. The same tag will not be added twice (it will be silently ignored)
        /// </summary>
        /// <param name="tag"></param>
        public virtual void AddTag(XsTag tag)
        {
            if (!Tags.Contains(tag))
            {
                //tag.AddEntity(this);
                Tags.Add(tag);
            }
        }

        #endregion

        #region Public Methods

        public virtual bool IsValid()
        {
            return (GetRuleViolations().Count() == 0);
        }

        public virtual IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("The article title is required", "Title");

            if ((null != Title) && (Title.Length > 150))
                yield return new RuleViolation("The article title length is longer than 150 characters", "Title");

            if (String.IsNullOrEmpty(Content))
                yield return new RuleViolation("The article content is required", "Content");

            yield break;
        }
       
        #endregion
    }
}
