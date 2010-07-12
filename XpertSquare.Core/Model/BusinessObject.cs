using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    public class XsBusinessObject
    {
        private DateTime _creationDT;
        private DateTime _updateDT;
        private String _lastUpdator;

        public virtual DateTime CreationDT
        {
            get { return _creationDT; }
            set { _creationDT = value; }
        }
        public virtual DateTime UpdateDT
        {
            get { return _updateDT; }
            set { _updateDT = value; }
        }
        public virtual String LastUpdator {
            get { return _lastUpdator; }
            set
            {
                if (null != value)
                    _lastUpdator = value;
                else _lastUpdator = String.Empty;
            }
        }

        public XsBusinessObject()
        {
            _creationDT = DateTime.UtcNow;
            _updateDT = DateTime.UtcNow;
        }
    }
}
