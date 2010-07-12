using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    /// <summary>
    /// Represents the tag for the <see cref="WikiPage"/> entity
    /// </summary>    
    [Serializable()]
    public class XsTag
    {
        private String _name = String.Empty;
        private long _id = 0;

        //this is an workaround for NHibernate.Linq bug
        private Int32 _questionsCount = 0;

        public virtual long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual String Name
        {
            get { return _name; }
            set {
                if (null != value)
                {
                    _name = value.ToLowerInvariant();                    
                }
                else
                {
                    _name = String.Empty;
                }
            }
        }

        public virtual Int32 QuestionCount
        {
            get { return _questionsCount; }
            protected set { _questionsCount = value; }
        }

        public virtual IList<XsQuestion> Questions { get; set; }
        public virtual IList<WikiArticle> Articles { get; set; }

        public XsTag()
        {
            Questions = new List<XsQuestion>();
            Articles = new List<WikiArticle>();
        }       

        public virtual void AddQuestion(XsQuestion question)
        {
            Questions.Add(question);
            _questionsCount++;
        }

        public virtual void RemoveQuestion(XsQuestion question)
        {
            Questions.Remove(question);
            _questionsCount--;
        }

        public virtual void AddArticle(WikiArticle article)
        {
            Articles.Add(article);
        }

        #region to move in a base class

        private int? cachedHashcode;

        /// <summary>
        /// To help ensure hashcode uniqueness, a carefully selected random number multiplier 
        /// is used within the calculation.  Goodrich and Tamassia's Data Structures and
        /// Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
        /// of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
        /// for more information.
        /// </summary>
        private const int HASH_MULTIPLIER = 31;

        public override bool Equals(object obj)
        {
            XsTag compareTo = obj as XsTag;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (compareTo == null)
                return false;

            return _name.Equals(compareTo.Name);

        }

        public override int GetHashCode()
        {
            if (cachedHashcode.HasValue)
                return cachedHashcode.Value;

            unchecked
            {
                // It's possible for two objects to return the same hash code based on 
                // identically valued properties, even if they're of two different types, 
                // so we include the object's type in the hash calculation
                int hashCode = GetType().GetHashCode();
                cachedHashcode = (hashCode * HASH_MULTIPLIER) ^ Name.GetHashCode();
            }
            return cachedHashcode.Value;
        }


        #endregion

    }
}
