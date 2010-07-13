using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XpertSquare.Web.Core
{
    /// <summary>
    /// TODO: the settings values need to be retrieved from DB or connfig file
    /// </summary>
    public class Settings
    {
        public static String SITE_TITLE = "XpertSquare";
        //Users page settings
        public static Int16 USERS_PAGINATION_SIZE = 30;
        public static Int16 QUESTIONS_PAGINATION_SIZE = 2;
        public static Int16 USERS_PAGINATION_COLUMNS = 5;
        //Tags page settings
        public static Int16 TAGS_PAGINATION_SIZE = 30;
        public static Int16 TAGSS_PAGINATION_COLUMNS = 5;
        public static Int16 RSS_ITEMS = 30;
        public static Int16 AUTOCOMPLETE_ENTRIES = 10;
        public static Int16 POPULAR_TAGS_LIMIT = 20;
    }
}
