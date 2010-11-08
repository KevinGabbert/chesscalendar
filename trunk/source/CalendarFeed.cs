using System;
using System.Collections.Generic;
using ChessCalendar.Interfaces;
using RssToolkit.Rss;

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
            var postedMoves = this.Post_NewMoves(); //Outputs to NewMove Queue, builds ToDo
            this.Save_To_Calendar(postedMoves, new Feed(), "", "", new Uri("") ); //denoted by URI   //Processes all items in ToDo
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
        internal List<bool> Post_NewMoves()
        {
            foreach (ChessRSSItem chessRssItem in this)
            {
                //post

                //this.ToDo = new CalendarManager(this);
                //this.ToDo.DebugMode = this.DebugMode;
                //this.ToDo.Beep_On_New_Move = this.Beep_On_New_Move;

                //_calendarToPost = logToCalendar;

                ////load up Ignore list with items already in calendar

                ////get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
                ////TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..

                //this.ToDo.IgnoreList = GoogleCalendar.GetAlreadyLoggedChessGames(userName, password, _calendarToPost, DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)), DateTime.Now, "auto-logger");

                //    feedToSave.AddRange(RssDocument.Load(uriToWatch).Channel.Items);

                //    this.AddOrUpdate_Games(this.ToDo, feedToSave);

                //    //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                //    //On remove, delete the all day reminder.

                //    this.ClearList = false;

                //    this.ProcessNewRSSItems(feedToSave); //If there are *new* entries, then the gridview will clear and refresh

                //    //TODO: if NO new entries, it won't clear & refresh, meaning cruft won't 

                //    if (this.LogGames)
                //    {
                //        //TODO: this needs to be an asterisk or something on the log form.
                //        foreach (IChessItem current in this.ToDo)
                //        {
                //            this.Output(current); //TODO this causes gridview to clear and refresh
            }

            throw new System.NotImplementedException();
        }

        private void Save_To_Calendar(List<bool> x, Feed feedToSave, string userName, string password, Uri uriToWatch)
        {
            //try
            //{
            //    if (this.LogGames)
            //    {
            //        //TODO: this needs to be an asterisk or something on the log form.
            //        foreach (IChessItem current in this.ToDo)
            //        {
            //            this.Save_Game_Info(current, userName, password);
            //        }
            //    }

            //    this.ClearList = true;

            //    this.ToDo.Clear();
            //}
            //catch (Exception ex)
            //{
            //    //if 504 Invalid gateway error. Chess.com is down
            //    this.Output(string.Empty, "error. " + ex.Message);

            //    GoogleCalendar.CreateEntry(userName, password, "Error", "Error", "Chess.com error", ex.Message +
            //                                                                                        this.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            //}

            //this.Wait(this.WaitSeconds);
        }
    }
}
