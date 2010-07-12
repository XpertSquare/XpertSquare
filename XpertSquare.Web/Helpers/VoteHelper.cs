using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using XpertSquare.Core.Model;

namespace XpertSquare.Web.Helpers
{
    public static class VoteHelper
    {

        private const Int16 VOTE_UP_VALUE = 1;
        private const Int16 VOTE_DOWN_VALUE = -1; 

        /// <summary>
        /// Returns whether the <paramref name="ntUsername"/> has an Up Vote for the <paramref name="answer"/>
        /// </summary>
        /// <param name="ntUsername"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static bool IsUserAnswerUpVote(String ntUsername, XsAnswer answer)
        {
            bool userHasUpVote = false;
            foreach (XsVote vote in answer.Votes)
            {
                if ((vote.User.Username.Equals(ntUsername)) && (vote.Value == VOTE_UP_VALUE))
                {
                    userHasUpVote = true;
                    break;
                }
            }
            return userHasUpVote;
        }

        /// <summary>
        /// Returns whether the <paramref name="ntUsername"/> has a Down Vote for the <paramref name="answer"/>
        /// </summary>
        /// <param name="ntUsername"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static bool IsUserAnswerDownVote(String ntUsername, XsAnswer answer)
        {
            bool userHasDownVote = false;
            foreach (XsVote vote in answer.Votes)
            {
                if ((vote.User.Username.Equals(ntUsername)) && (vote.Value == VOTE_DOWN_VALUE))
                {
                    userHasDownVote = true;
                    break;
                }
            }
            return userHasDownVote;
        }


        /// <summary>
        /// Returns whether the <paramref name="ntUsername"/> has an Up Vote for the <paramref name="question"/>
        /// </summary>
        /// <param name="ntUsername"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool IsUserQuestionUpVote(String ntUsername, XsQuestion question)
        {
            bool userHasUpVote = false;
            foreach (XsVote vote in question.Votes)
            {
                if ((vote.User.Username.Equals(ntUsername)) && (vote.Value == VOTE_UP_VALUE))
                {
                    userHasUpVote = true;
                    break;
                }
            }
            return userHasUpVote;
        }

        /// <summary>
        /// Returns whether the <paramref name="ntUsername"/> has a Down Vote for the <paramref name="question"/>
        /// </summary>
        /// <param name="ntUsername"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool IsUserAnswerDownVote(String ntUsername, XsQuestion question)
        {
            bool userHasDownVote = false;
            foreach (XsVote vote in question.Votes)
            {
                if ((vote.User.Username.Equals(ntUsername)) && (vote.Value == VOTE_DOWN_VALUE))
                {
                    userHasDownVote = true;
                    break;
                }
            }
            return userHasDownVote;
        }

    }
}
