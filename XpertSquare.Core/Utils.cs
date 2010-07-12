using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web;

namespace XpertSquare.Core
{
    public static class Utils
    {
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Slug are always lowercase.")]
        public static string GetSlug(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            text = text.ToLowerInvariant();
            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("¿", string.Empty);
            text = text.Replace("!", string.Empty);
            text = text.Replace("¡", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace("<", string.Empty);
            text = text.Replace(">", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }

        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }

        private static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                char c = normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates an excerpt of ~150 chars from a content
        /// </summary>
        /// <param name="_content"></param>
        public static String CreateContentExcerpt(String htmlContent)
        {
            const int EXCERPT_SIZE = 150;
            String sExcerpt = String.Empty;

            if (null != htmlContent)
            {
                StringBuilder sbExcerpt = new StringBuilder();

                sExcerpt = htmlContent.Replace(Environment.NewLine, " ");
                sExcerpt = RemoveHtmlTags(sExcerpt);

                int sPosition = 0;
                bool bExcerptLengthReached = false;
                while ((sPosition < sExcerpt.Length) && !bExcerptLengthReached)
                {
                    sbExcerpt = sbExcerpt.Append(sExcerpt[sPosition]);
                    if ((sPosition >= EXCERPT_SIZE) && (Char.IsSeparator(sExcerpt[sPosition])))
                    {
                        bExcerptLengthReached = true;
                    }
                    sPosition++;
                }

                if (htmlContent.Length > EXCERPT_SIZE)
                {
                    sbExcerpt = sbExcerpt.Append("...");
                }
                sExcerpt = sbExcerpt.ToString();
            }

            return sExcerpt;
        }


        /// <summary>
        /// Removes HTML tags from a string using regex        
        /// </summary>
        /// <param name="_content"></param>
        public static String RemoveHtmlTags(String htmlContent)
        {
            return Regex.Replace(htmlContent, @"<(.|\n)*?>", String.Empty);
        }


    }

 }

