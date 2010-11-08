using System;

namespace ChessCalendar
{
    public class ChessCalendarFeed: Feed
    {
        public OutputClass Output { get; set; }
        public CalendarManager CalendarManager { get; set; }

        public Uri Uri { get; set; }
        public Uri Calendar { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool Post { get; set; }
        public bool Save { get; set; }

        /// <summary>
        /// Creates a new instance of login and Calendar information for the inherited feed.
        /// </summary>
        /// <param name="uriToWatch"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="logToCalendar"></param>
        public ChessCalendarFeed(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            this.Uri = uriToWatch;
            this.UserName = userName;
            this.Password = password;
            this.Calendar = logToCalendar;
        }

        public void Pull_Feed_Info()
        {
            //feedToSave.AddRange(RssDocument.Load(uriToWatch).Channel.Items);
        }

        public void Create_ToDo_List()
        {
            
        }


        internal void Go()
        {
            if (this.Post)
            {
                this.Post_NewMoves();
            }

            if (this.Save)
            {
                this.Save_To_Calendar(); //denoted by URI
            }
        }

        

        /// <summary>
        /// Saves new items to the Calendar
        /// </summary>
        internal void Save_To_Calendar( )
        {

            foreach (ChessRSSItem chessRssItem in this)
            {
                //save
            }

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Posts moves in feed to this.Output
        /// </summary>
        internal void Post_NewMoves()
        {
            foreach (ChessRSSItem chessRssItem in this)
            {
                //post
            }

            throw new System.NotImplementedException();
        }
    }
}
