using System;

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

        internal static string GetOpponent(string title)
        {
            var split = title.Split("-".ToCharArray(), StringSplitOptions.None); //This could break if the player has '-' in his name

            return split[split.Length - 1]; //Opponent name will always be the last item in the title.  
        }

        internal static string GetTournamentName(string title)
        {
            var split = title.Split("-".ToCharArray(), StringSplitOptions.None); //This could break if the player or tourney has '-' in his/its name

            return split[split.Length - 2]; //Opponent name will always be the 2nd to last item in the title
        }

        internal static string GetGameTitle(string title)
        {
            var split = title.Split("-".ToCharArray(), StringSplitOptions.None); //this could break if there is a hyphen in the title

            return split[0]; //Title will always be the 2nd to last item in the title
        }

        internal static DateTime GetPubDate(string stringDate)
        {
            return DateTime.Parse(stringDate);
        }
    }
}
