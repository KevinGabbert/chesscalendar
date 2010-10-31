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
    }
}
