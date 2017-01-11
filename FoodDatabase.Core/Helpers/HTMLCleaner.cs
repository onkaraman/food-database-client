using System;
using FoodDatabase.Core.Patterns;

namespace FoodDatabase.Core.Helpers
{
    public class HTMLCleaner : LazyStatic<HTMLCleaner>
    {
        public HTMLCleaner() {}

        /// <summary>
        /// Replaces html tags with their intended characters.
        /// </summary>
        public string Replace(string rawname)
        {

            if (rawname.Contains("&uuml;"))
                rawname = rawname.Replace("&uuml;", "ü");

            if (rawname.Contains("&amp;"))
                rawname = rawname.Replace("&amp;", "&");

            if (rawname.Contains("&ouml;"))
                rawname = rawname.Replace("&ouml;", "ö");

            if (rawname.Contains("&auml;"))
                rawname = rawname.Replace("&auml;", "ä");

            if (rawname.Contains("&szlig;"))
                rawname = rawname.Replace("&szlig;", "ß");

            if (rawname.Contains("&Eacute;"))
                rawname = rawname.Replace("&Eacute;", "É");

            if (rawname.Contains("&apos;"))
                rawname = rawname.Replace("&apos;", "'");

            if (rawname.Contains("&#039;"))
                rawname = rawname.Replace("&#039;", "'");

            return rawname;
        }
    }
}
