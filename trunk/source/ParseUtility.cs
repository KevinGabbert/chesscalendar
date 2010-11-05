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
            throw new System.NotImplementedException(); //"Description.Split("<br/>".ToArray(), StringSplitOptions.None)[0];
        }

        internal static System.DateTime GetTimeLeft(string description)
        {
            throw new System.NotImplementedException();
        }

        internal static string GetTimeLeftRaw(string description)
        {
            throw new System.NotImplementedException();
        }

        internal static string GetMove(string description)
        {
            throw new System.NotImplementedException();
        }
    }
}
