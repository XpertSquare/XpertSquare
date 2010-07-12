using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XpertSquare.Web.Helpers
{
    public static class ItemStartedLabelHelper
    {
        public static string ItemStartedLabel(this HtmlHelper helper, DateTime dtStarted)
        {
            String sLabel = String.Empty;
            Int16 days = 0;
            Int16 hours = 0;
            Int16 minutes = 0;  
            Int16 seconds = 0;  

            if (null != dtStarted)
            {
                days = Convert.ToInt16((DateTime.UtcNow - dtStarted).TotalDays);
                if (0 == days)
                {
                    hours = Convert.ToInt16((DateTime.UtcNow - dtStarted).TotalHours);
                    if (0 == hours)
                    {
                        minutes = Convert.ToInt16((DateTime.UtcNow - dtStarted).TotalMinutes);
                        if (0 == minutes)
                        {
                            seconds = Convert.ToInt16((DateTime.UtcNow - dtStarted).TotalSeconds);
                            sLabel = String.Format("{0}s ago", seconds);
                        }
                        else
                        {
                            sLabel = minutes > 1 ? String.Format("{0} mins ago", minutes) : String.Format("{0} min ago", minutes);
                        }
                    }
                    else
                    {
                        sLabel = hours > 1 ? String.Format("{0} hours ago", hours) : String.Format("{0} hour ago", hours);
                    }
                }
                else
                {
                    sLabel = days > 1 ? String.Format("{0} days ago", days) : String.Format("{0} day ago", days);
                }
               
            }
            else
            {
               sLabel = "1s ago";
            }

            return sLabel;
        }        
    }
}
