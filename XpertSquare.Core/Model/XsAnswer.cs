using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    public class XsAnswer : XsCore
    {
        public XsAnswer()
        {
         
        }       

        public virtual XsQuestion Parent { get; set; }

        #region Public Methods

        public virtual void AddVote(XsVote vote)
        {
            if (!Votes.Contains(vote))
            {
                vote.Answer = this;
                Votes.Add(vote);
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


        #endregion

    }
}
