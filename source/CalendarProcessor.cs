using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessCalendar
{
    /// <summary>
    /// Gets 
    /// </summary>
    public class CalendarProcessor: Feed
    {
        public OutputClass Output { get; set; }
        public EntryList ToDo { get; set; }

        public Uri Calendar { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<string> UsersToProcess { get; set; } //TODO: should this be an EntryList?

        public bool Post { get; set; }
        public bool Save { get; set; }

        public bool NewMessage { get; set; }
        public bool DebugMode { get; set; }
        public bool Beep_On_New_Move { get; set; }
        public bool GetPGNs { get; set; }
        public bool LogGames { get; set; }
        public bool ResetWait { get; set; }
        public string UserLogged { get; set; }

        /// <summary>
        /// Creates a new instance of login and Calendar information for the inherited feed.
        /// </summary>
        /// <param name="uriToWatch"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="logToCalendar"></param>
        public CalendarProcessor(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            this.Uri = uriToWatch;
            this.UserName = userName;
            this.Password = password;
            this.Calendar = logToCalendar;
        }

        public CalendarProcessor(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool load)
        {
            this.Uri = uriToWatch;
            this.UserName = userName;
            this.Password = password;
            this.Calendar = logToCalendar;

            if (load){this.Load();}
        }

        public void Refresh()
        {
            this.Load();
            this.AddOrUpdate_Games(this.ToDo, this);
        }

        public void Pull_Feed_Info()
        {
            //feedToSave.AddRange(RssDocument.Load(uriToWatch).Channel.Items);
        }

        internal void Go(List<string> usersToWatch)
        {
            var x = this.Post_NewMoves(usersToWatch); //Outputs to NewMove Queue, builds ToDo
            
            this.Save_To_Calendar(x, new Feed(), "", "", new Uri("")); //denoted by URI   //Processes all items in ToDo
        }

        internal void Go()
        {
            this.Refresh();

            //get all the opponents in *this*, and save them all to the calendar
            foreach (var x in this.GetOpponents().Select(feedOpponent => this.Post_NewMoves(feedOpponent, true)))
            {
                this.Save_To_Calendar(x, new Feed(), "", "", new Uri("")); //denoted by URI   //Processes all items in ToDo
            }
        }
        
        /// <summary>
        /// Saves new items to the Calendar
        /// </summary>
        internal void Save_To_Calendar(EntryList calendarManager )
        {

            foreach (ChessRSSItem chessRssItem in this)
            {
                //save
            }

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Filters the RSS given to the users wanted
        /// </summary>
        /// <param name="usersToWatch"></param>
        /// <returns></returns>
        internal EntryList Post_NewMoves(List<string> usersToWatch)
        {


            //returns a list of Entries specific to that user.
            throw new System.NotImplementedException();
        }

        internal EntryList Post_NewMoves(string userToWatch, bool output)
        {


            //returns a list of Entries specific to that user.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Posts moves in feed to this.Output
        /// </summary>
        internal EntryList Post_NewMoves()
        {
            foreach (ChessRSSItem chessRssItem in this)
            {
                //post

                var toDo = new EntryList();
                //toDo.DebugMode = this.DebugMode;
                //toDo.Beep_On_New_Move = this.Beep_On_New_Move;

                //_calendarToPost = logToCalendar;

                ////load up Ignore list with items already in calendar

                ////get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
                ////TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..

                toDo.IgnoreList = GoogleCalendar.GetAlreadyLoggedChessGames(this.UserName, this.Password, this.Calendar, DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)), DateTime.Now, "auto-logger");

                //    feedToSave.AddRange(RssDocument.Load(uriToWatch).Channel.Items);

                //this.AddOrUpdate_Games(toDo, feedToSave);

                //    //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                //    //On remove, delete the all day reminder.

                //    this.ClearList = false;

                //this.ProcessNewRSSItems(feedToSave); //If there are *new* entries, then the gridview will clear and refresh

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

        private void AddOrUpdate_Games(EntryList toDo, ICollection<ChessRSSItem> gamelist)
        {
            if (gamelist != null)
            {
                this.NewMessage = true;

                if (gamelist.Count > 0)
                {
                    this.LogGames = true;

                    //TODO: this needs to be in a field in the log form
                    //this.Output(string.Empty, Environment.NewLine + "Found " + gamelist.Count.ToString() + " Updated Games: " + DateTime.Now.ToLongTimeString());

                    //this.Output(string.Empty, Environment.NewLine, OutputMode.Form);
                    foreach (ChessRSSItem game in gamelist)
                    {
                        toDo.ProcessRSSItem(game);
                    }
                }
                else
                {
                    //this.Output(string.Empty, "No new or updated games found: " + DateTime.Now.ToLongTimeString());
                    this.LogGames = false;
                }
            }
        }

        private void ProcessNewRSSItems(IEnumerable<ChessRSSItem> newRssItems)
        {
            foreach (var item in newRssItems)
            {
                this.Output.NewMoves.Enqueue(item); //?
            }
        }
        private void Save_To_Calendar(EntryList x, Feed feedToSave, string userName, string password, Uri uriToWatch)
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
