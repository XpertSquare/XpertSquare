using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace XpertSquare.Web.Helpers
{
    public static class MenuHelper
    {
        private static String ASK_A_QUESTION = "Ask a question";
        private static String CREATE_AN_ARTICLE = "Create an article";

        public static string Menu(this HtmlHelper helper)
        {
            StringBuilder sbMenu = new StringBuilder();

            StringBuilder sbMenuLeft = new StringBuilder();
            StringBuilder sbMenuRight = new StringBuilder();

            // Create opening unordered list tag
            sbMenu.Append("<ul>");

            sbMenuLeft.Append("<div class='navigation'><ul>");
            sbMenuRight.Append("<div class='navigation' style='float: right;'><ul>");

            // Render each top level node
            var topLevelNodes = SiteMap.RootNode.ChildNodes;

            
            foreach (SiteMapNode node in topLevelNodes)
            {
                if (node.Title.Equals(ASK_A_QUESTION) || node.Title.Equals(CREATE_AN_ARTICLE))
                {
                    sbMenuRight.AppendLine("<li>");
                    if (SiteMap.CurrentNode == node)
                    {
                        sbMenuRight.AppendFormat("<a class='selected' href='{0}'>{1}</a>", node.Url, helper.Encode(node.Title));
                    }
                    else
                    {
                        sbMenuRight.AppendFormat("<a href='{0}'>{1}</a>", node.Url, helper.Encode(node.Title));
                    }
                    sbMenuRight.AppendLine("</li>");
                }
                else
                {
                    sbMenuLeft.AppendLine("<li>");
                    if (SiteMap.CurrentNode == node)
                    {
                        sbMenuLeft.AppendFormat("<a class='selected' href='{0}'>{1}</a>", node.Url, helper.Encode(node.Title));
                    }
                    else
                    {
                        sbMenuLeft.AppendFormat("<a href='{0}'>{1}</a>", node.Url, helper.Encode(node.Title));
                    }
                    sbMenuLeft.AppendLine("</li>");

                }
            }

            // Close unordered list tag
            sbMenuLeft.Append("</ul>");
            sbMenuLeft.Append("</div>");
            sbMenuRight.Append("</ul>");
            sbMenuRight.Append("</div>");
            return String.Concat(sbMenuLeft.ToString(), sbMenuRight.ToString());
        }


    }

}
