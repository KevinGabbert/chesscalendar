using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChessCalendar.Enums;
using ChessCalendar.Interfaces;
using RssToolkit.Rss;

namespace ChessCalendar
{
    /// <summary>
    /// Gets 
    /// </summary>
    public class CalendarProcessor: ChessFeed
    {
        #region Properties

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
            public bool ClearList { get; set; } //needs a better name.
            public string UserLogged { get; set; }


            public int WaitSeconds { get; set; }

            private int _waitProgress;
            public int WaitProgress
            {
                get
                {
                    return _waitProgress < 0 ? 0 : _waitProgress;
                }
                set
                {
                    _waitProgress = value;
                }
            }
            public DateTime NextCheck { get; set; }

        #endregion

        private bool _stop = false;

        /// <summary>
        /// Creates a new instance of login and Calendar information for the inherited feed.
        /// </summary>
        /// <param name="uriToWatch"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="logToCalendar"></param>
        public CalendarProcessor(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool refresh)
        {
            this.FeedUri = uriToWatch;
            this.UserName = userName;
            this.Password = password;
            this.Calendar = logToCalendar;

            this.RefreshRSS();
        }

        //public CalendarProcessor(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool load)
        //{
        //    this.Uri = uriToWatch;
        //    this.UserName = userName;
        //    this.Password = password;
        //    this.Calendar = logToCalendar;

        //    if (load){this.Load();}
        //}

        public void RefreshRSS()
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
            //var x = this.Post_NewMoves(usersToWatch, true); //Outputs to NewMove Queue, builds ToDo
            
           // this.Store(x, new ChessFeed(), "", "", new Uri("")); //denoted by URI   //Processes all items in ToDo
        }

        internal void Go()
        {
            this.RefreshRSS();

            //get all the opponents in *this*, and save them all to the calendar
            foreach (var x in this.GetOpponents().Select(feedOpponent => this.Post_NewMoves(feedOpponent, true)))
            {
                this.Store(x, new ChessFeed(), "", "", new Uri("")); //denoted by URI   //Processes all items in ToDo
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

        internal EntryList Post_NewMoves(string chessUserToWatch, bool output)
        {
            foreach (ChessRSSItem chessRssItem in this)
            {
                
            }

            //returns a list of Entries specific to that user.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Posts moves in feed to this.Output
        /// returns a list of Entries specific to that user.
        /// </summary>
        internal EntryList Post_NewMoves()
        {
            try
            {
                foreach (ChessRSSItem chessRssItem in this)
                {
                    this.ToDo = new EntryList();
                    this.ToDo.DebugMode = this.DebugMode;
                    this.ToDo.Beep_On_New_Move = this.Beep_On_New_Move;

                    //get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
                    //TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..
                    this.ToDo.IgnoreList = GoogleCalendar.GetAlreadyLoggedChessGames(this.UserName, this.Password, this.Calendar, DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)), DateTime.Now, "auto-logger");

                    this.RefreshRSS(); 

                    this.AddOrUpdate_Games(this.ToDo, this);

                    //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                    //On remove, delete the all day reminder.

                    //    this.ClearList = false;

                    this.PostAllNewRSSItems(this); //If there are *new* entries, then the gridview will clear and refresh

                    //    //TODO: if NO new entries, it won't clear & refresh, meaning cruft won't 

                    if (this.LogGames)
                    {
                        //TODO: this needs to be an asterisk or something on the log form.
                        foreach (IChessItem current in this.ToDo)
                        {
                            this.Output.Post(current); //TODO this causes gridview to clear and refresh
                            //Not needed here this.Store(this.ToDo, this, this.UserName, this.Password, this.FeedUri);
                        }
                    }

                    this.ClearList = true;

                    this.ToDo.Clear();
                }
            }
            catch (Exception ex)
            {
                //if 504 Invalid gateway error. Chess.com is down
                this.Output.Post(string.Empty, "error. " + ex.Message, OutputMode.Form);

                GoogleCalendar.CreateEntry(this.UserName, 
                                           this.Password, 
                                           "Error", 
                                           "Error", 
                                           "Chess.com error", 
                                           ex.Message + "--", DateTime.Now, DateTime.Now, this.Calendar);
            }

            this.Wait(this.WaitSeconds);

            return new EntryList();
        }

        private void Wait(int waitSeconds)
        {
            DateTime start = DateTime.Now;
            TimeSpan waitTime = new TimeSpan(0, 0, 0, waitSeconds);

            DateTime finish = start + waitTime;
            this.NextCheck = finish;

            DateTime current = DateTime.Now;
            while (current < finish)
            {
                Application.DoEvents();

                var difference = (finish.Subtract(DateTime.Now));
                this.WaitProgress = Convert.ToInt32(100 - ((difference.TotalSeconds / waitTime.TotalSeconds) * 100));
                current = DateTime.Now;

                if (this.ResetWait)
                {
                    this.ResetWait = false;
                    break;
                }

                if (this._stop)
                {
                    this.WaitProgress = 0;
                    this.NextCheck = new DateTime();
                    break;
                }
            }

            this.NewMessage = false;
        }

        private void PostAllNewRSSItems(IEnumerable<ChessRSSItem> newRssItems)
        {
            foreach (var item in newRssItems)
            {
                this.Output.Post(item);
            }
        }

        private void PostGamesWithOpponent(IEnumerable<ChessRSSItem> newRssItems, string opponent)
        {
            foreach (var item in newRssItems.Where(item => item.Opponent == opponent))
            {
                this.Output.Post(item);
            }
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
        private void Store(EntryList x, ChessFeed feedToSave, string userName, string password, Uri uriToWatch)
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

        internal void Stop()
        {
            this._stop = true;
        }
        internal void Start()
        {
            this._stop = false;
        }
    }
}


        ///// <summary>
        ///// //user needs to call Process_All_Feeds() to start
        ///// </summary>
        ///// <param name="uriToWatch"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        ///// <param name="logToCalendar"></param>
        ///// <param name="post"></param>
        ///// <param name="save"></param>
        //public void Add_Feed(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool post, bool save)
        //{
        //    var newFeed = new CalendarProcessor(uriToWatch, userName, password, logToCalendar);
        //    newFeed.Post = post;
        //    newFeed.Save = save;

        //    this.CCFeeds.Add(newFeed);
        //}

        //public void Process_Feed(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool post, bool save)
        //{
        //    var newFeed = new CalendarProcessor(uriToWatch, userName, password, logToCalendar);
        //    newFeed.Post = post;
        //    newFeed.Save = save;

        //    newFeed.Go();
        //}
  
        ///// <summary>
        ///// Intended to take the place of Save_Single_Feed_To_Calendar
        ///// The purpose here is to parse all the feeds into this object structure, outputting to base.NewMoves so that the Subscriber can Dequeue them,
        ///// then saving to Calendar
        ///// </summary>
        //public void Process_ALL_Feeds(bool postAll, bool saveAll)
        //{
        //    foreach (CalendarProcessor feed in this.Feeds)
        //    {
        //        feed.Post = postAll;
        //        feed.Save = saveAll;
        //        //feed.Go();
        //    }
        //}

        //public void Process_Feed(string userName)
        //{
        //    foreach (CalendarProcessor feed in this.Feeds)
        //    {
        //         feed.Go(feed.GetOpponents());
        //    }
        //}

        //#endregion