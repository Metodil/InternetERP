﻿namespace InternetERP.Common
{
    public static class Utils
    {
        public static string TruncateAtWord(this string value, int length)
        {
            if (value == null || value.Length < length || value.IndexOf(" ", length) == -1)
            {
                return value;
            }

            return value.Substring(0, value.IndexOf(" ", length)) + "...";
        }
    }
}
