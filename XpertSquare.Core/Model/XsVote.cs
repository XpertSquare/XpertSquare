using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    public class XsVote:XsBusinessObject
    {
        private long _id;
        private VoteType _type = VoteType.UpDown;
        private Int16 _value = 0;
        private VotedEntityType _votedEntity = VotedEntityType.Question;


        public virtual long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual XsUser User { get; set; }
        public virtual XsAnswer Answer { get; set; }
        public virtual XsQuestion Question { get; set; }

        public virtual Int16 Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual VoteType Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public XsVote()
        {
            CreationDT = DateTime.UtcNow;
            UpdateDT = DateTime.UtcNow;
        }
    }
}
