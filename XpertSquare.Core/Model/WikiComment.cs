using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    public class WikiComment : XsBusinessObject
    {
        private long _id;
        private String _content = null;
        private bool _approved = false;

        public virtual long ID
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public virtual String Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public bool Approved
        {
            get { return _approved; }
            set { _approved = value; }
        }

        public virtual XsUser Author { get; set; }

        public virtual XsCore Parent { get; set; }

        public virtual WikiComment ParentComment { get; set; }
    }
}
