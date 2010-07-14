using System;
using System.Text;


namespace XpertSquare.Web.Core
{
    public static class WebUtils
    {

        public static String UrlEncodeUnsafeChars(String urlString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var urlChar in urlString)
            {
                switch (urlChar)
                {
                    case '#': sb.Append("%23");
                        break;                   
                    default: sb.Append(urlChar);
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
