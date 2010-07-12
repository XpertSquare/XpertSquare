using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;
using XpertSquare.Data.NH.Repository;

namespace XpertSquare.Web.Helpers
{
    public static class UserHelper
    {
        public static XsUser GetUserByUsername(String ntUsername)
        {
            XsUser user = null;
            if (!String.IsNullOrEmpty(ntUsername.Trim()))
            {
                IXsUserRepository userRepository = new UserRepository();
                user = userRepository.GetByUsername(ntUsername);

                if (null == user)
                {
                    user = new XsUser();
                    user.Username = ntUsername;
                    user.DisplayName = ntUsername;
                    user = userRepository.Save(user);                    
                }
            }

            return user;            
        }

        public static String GetDisplayNameForGUI(XsUser user)
        { 
            String displayNameForGUI = "<Anonymous>";
            if (null != user)
            {
                displayNameForGUI = user.DisplayName;
                if (String.IsNullOrEmpty(displayNameForGUI))
                {
                    if (!String.IsNullOrEmpty(user.FirstName))
                    {
                        displayNameForGUI = user.FirstName;
                    }
                    else if (!String.IsNullOrEmpty(user.LastName))
                    {
                        displayNameForGUI = user.LastName;
                    }
                    displayNameForGUI = user.Username;
                }
            }
            return displayNameForGUI;
        }

        public static String GetUserStatsForGUI(XsUser user)
        {
            String userStatsForGUI = String.Empty;

            if (null != user)
            {
                userStatsForGUI = String.Concat(user.QuestionCount.ToString(), " x Q | ", user.AnswerCount.ToString(), " x A"); 
            }

            return userStatsForGUI;
        }

    }
}
