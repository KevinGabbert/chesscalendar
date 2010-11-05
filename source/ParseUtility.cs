using System;
using System.Linq;

namespace ChessCalendar
{
    public static class ParseUtility
    {
        public static string GetGameID(string linkString)
        {
            string retVal = "unassigned";

            if (linkString != null)
            {
                if (linkString.Contains("?id="))
                {
                    retVal = linkString.Substring(linkString.IndexOf("?id=") + 4);
                }
            }
            return retVal;
        }

        internal static string GetRating(string description)
        {
            return description.Split("</>".ToCharArray(), StringSplitOptions.None)[0];
        }

        internal static System.DateTime GetTimeLeft(string description)
        {
            throw new System.NotImplementedException();
        }

        internal static string GetTimeLeftRaw(string description)
        {
            return description.Split("</>".ToCharArray(), StringSplitOptions.None)[3];
        }

        internal static string GetMove(string description)
        {
            return description.Split("</>".ToCharArray(), StringSplitOptions.None)[6];
        }
    }
}
