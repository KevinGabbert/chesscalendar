using System;

namespace ChessCalendar
{
    public class ChessSiteInfo
    {
        public string UserName { get; set; }
        public Uri UriToWatch { get; set; }

        //TODO so users can add commas between usernames on login form
        //public List<string> UserName { get; set; }
        //public List<Uri> UriToWatch { get; set; }

        //public ChessSiteInfo()
        //{
        //    this.UserName = new List<string>();
        //    this.UriToWatch = new List<Uri>();
        //}
    }
}
