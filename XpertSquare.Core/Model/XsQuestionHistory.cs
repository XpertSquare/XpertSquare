using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace XpertSquare.Core.Model
{
    public class XsQuestionHistory : XsBusinessObject
    {
        private Guid _id;
        private Int16 _versionId;
        private String _title = String.Empty;
        private String _editSummary = String.Empty;
        private String _content = String.Empty;
        private String _contentHtml = String.Empty;
        private String _allTags = String.Empty;


        public virtual Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual String Title
        {
            get { return _title; }
            set
            {
                if (null != value)
                {
                    _title = value;
                }
                else
                {
                    _title = String.Empty;
                }
            }
        }

        public virtual String EditSummary
        {
            get { return _editSummary; }
            set
            {
                if (null != value)
                {
                    _editSummary = value;
                }
                else
                {
                    _editSummary = String.Empty;
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

        public virtual String AllTags
        {
            get { return _allTags; }
            set
            {
                if (null != value)
                {
                    _allTags = value;
                }
                else
                {
                    _allTags = String.Empty;
                }
            }
        }
    }
}
