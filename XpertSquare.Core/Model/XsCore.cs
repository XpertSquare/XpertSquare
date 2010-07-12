using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using XpertSquare.Core;

namespace XpertSquare.Core.Model
{
    public abstract class XsCore: XsBusinessObject
    {
        private long _id;
        private String _title = String.Empty;
        private String _slugtTitle = String.Empty;
        private String _content = String.Empty;
        private String _contentHtml = String.Empty;
        private String _excerpt = String.Empty;
        private XsStatus _status = XsStatus.Draft;
        private DateTime _publishedDT;
        private Int32 _ranking = 0;

        public virtual long ID
        {
            get { return _id; }
            set {_id = value; }
        }

        public virtual String Title
        {
            get { return _title; }
            set
            {
                if (null != value)
                {
                    _title = value;
                    _slugtTitle = Utils.GetSlug(_title);
                }
                else
                {
                    _title = String.Empty;
                }
            }
        }

        public virtual String SlugTitle
        {
            get { return _slugtTitle; }
            protected set
            {
                if (null != value)
                {
                    _slugtTitle = value;
                }
                else
                {
                    _slugtTitle = String.Empty;
                }
            }
        }


        public virtual String Content
        {
            get { return _content; }
            set
            {
                if (null != value)
                {
                    _content = value;
                }
                else
                {
                    _content = String.Empty;
                }
            }
        }

        public virtual String ContentHtml
        {
            get { return _contentHtml; }
            set
            {
                if (null != value)
                {
                    _contentHtml = value;
                }
                else
                {
                    _contentHtml = String.Empty;
                }
            }
        }

       

        public virtual String Excerpt
        {
            get { return _excerpt; }
            set
            {
                if (null != value)
                {
                    _excerpt = value;
                }
                else
                {
                    _excerpt = String.Empty;
                }
            }

        }


        public virtual XsStatus Status 
        { 
            get { return _status; }
            set { _status = value; } 
        }

        public virtual DateTime PublishedDT
        {
            get { return _publishedDT; }
            set { _publishedDT = value; }
        }

        public virtual Int32 Ranking
        {
            get { return _ranking; }
            set { _ranking = value; }
        }

        public virtual XsUser Author { get; set; }


        public virtual IList<XsTag> Tags { get; set; }
        public virtual IList<WikiComment> Comments { get; set; }
        public virtual IList<XsVote> Votes { get; set; }

        public virtual String AllTags
        {
            get
            {
                String allTags = String.Empty;

                if ((null != Tags) && (Tags.Count > 0))
                {
                    foreach (XsTag tag in Tags)
                    {
                        if (String.IsNullOrEmpty(allTags))
                        {
                            allTags = tag.Name;
                        }
                        else
                        {
                            allTags = String.Concat(allTags, " ", tag.Name);
                        }
                    }
                }

                return allTags;
            }
        }

        public XsCore()
        {
            Tags = new List<XsTag>();
            Comments = new List<WikiComment>();
            Votes = new List<XsVote>();
        }
               
        
        /// <summary>
        /// Adds a comment to the collection of comments.
        /// </summary>
        /// <param name="comment"></param>
        public virtual void AddComment(WikiComment comment)
        {
            if (!Comments.Contains(comment))
            {
                comment.Parent = this;
                Comments.Add(comment);
            }
        }
    }
}
