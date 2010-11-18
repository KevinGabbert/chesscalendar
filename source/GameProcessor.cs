using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    /// <summary>
    /// Gets 
    /// </summary>
    public class GameProcessor
    {
        #region Properties

            public OutputClass Output { get; set; }
            public EntryList ToDo { get; set; }
            public ChessFeed ChessFeed { get; set;}

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
            //public bool LogGames { get; set; }
            public bool ResetWait { get; set; }
            public bool ClearList { get; set; } //needs a better name.
            public string UserLogged { get; set; }
            public string Name { get; set; }
            public bool UseCalendar { get; set; }

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
        /// <param name="name"></param>
        /// <param name="uriToWatch"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="logToCalendar"></param>
        public GameProcessor(string name, Uri uriToWatch, string userName, string password, Uri logToCalendar, bool useCalendar)
        {
            this.ToDo = new EntryList();
            this.Output = new OutputClass();
            this.ChessFeed = new ChessFeed();

            this.Name = name;
            this.ChessFeed.FeedUri = uriToWatch;
            this.UserName = userName;
            this.Password = password;
            this.Calendar = logToCalendar;
            this.UseCalendar = useCalendar;
        }

        public void Refresh()
        {
            this.Output.NewMoves.Updated = false;

            this.ChessFeed.Load();

            if (this.ChessFeed.Count > 0)
            {
                this.AddOrUpdate_Games(this.ToDo, this.ChessFeed);
                this.Post_NewMoves(this.ToDo);
                this.Store(this.ToDo);
            }
            else
            {
                this.ClearList = true; //this is what grid keys on to erase.
                this.Output.NewMoves.Updated = true;
            }

            this.ToDo.Clear();
        }

        public void Pull_Feed_Info()
        {
            //feedToSave.AddRange(RssDocument.Load(uriToWatch).Channel.Items);
        }

        internal void Go(IEnumerable<string> usersToWatch)
        {
            while (true)
            {
                this.Refresh();

                foreach (string user in usersToWatch)
                {
                    this.ToDo = this.Post_NewMoves(user, true); //Outputs to NewMove Queue, builds ToDo
                    this.Store(this.ToDo); //Stores all items in ToDo
                }

                //this.Wait(this.WaitSeconds);
            }
        }

        internal void Go()
        {
            while (true)
            {
                this.Refresh();

                //Get all the opponents in *this*, post them, and save all their moves to the calendar
                foreach (var x in this.ChessFeed.GetOpponents().Select(feedOpponent => this.Post_NewMoves(feedOpponent, true)))
                {
                    this.Store(x);
                }
            }
        }

        /// <summary>
        /// Filters by Opponent name
        /// </summary>
        /// <param name="chessUserToWatch"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        internal EntryList Post_NewMoves(string chessUserToWatch, bool output)
        {
            //all POST functions will use this one as a base

            //1. build an EntryList that only has entries from chessUserToWatch

            //2. call PostGamesWithOpponent(chessUserToWatch);
            
            foreach (ChessRSSItem chessRssItem in this.ChessFeed)
            {
                //Call PostGamesWith Opponent
            }

            //returns a list of Entries specific to that user.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Posts moves in feed to this.Output
        /// 
        /// returns a list of Entries specific to information stored in *this*.
        /// </summary>
        internal EntryList Post_NewMoves(EntryList toDo)
        {
            MessageBox.Show("Rewrite Post_NewMoves(string chessUserToWatch, bool output) then DELETE this function");

            try
            {
                //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                //On remove, delete the all day reminder.

                //get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
                //TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..
                if (this.UseCalendar)
                {
                    toDo.IgnoreList = GoogleCalendar.GetAlreadyLoggedChessGames(this.UserName, this.Password,
                                                                                this.Calendar,
                                                                                DateTime.Now.Subtract(new TimeSpan(15, 0,
                                                                                                                   0, 0)),
                                                                                DateTime.Now, "auto-logger");
                }

                toDo.DebugMode = this.DebugMode;
                toDo.Beep_On_New_Move = this.Beep_On_New_Move;

                this.ClearList = (toDo.Count > 0);

                foreach (var item in toDo)
                {
                    this.Output.Post(item); //TODO this causes gridview to clear and refresh   
                }

                //this.Output.NewMoves.Updated = true;
            }
            catch (Exception ex)
            {
                //if 504 Invalid gateway error. Chess.com is down
                //this.Output.Post(string.Empty, "error. " + ex.Message);

                GoogleCalendar.CreateEntry(this.UserName, 
                                           this.Password, 
                                           "Chess.com error", 
                                           ex.Message + "--", DateTime.Now, DateTime.Now, this.Calendar);
            }

            return toDo;
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
                    //this.LogGames = true;

                    //TODO: this needs to be in a field in the log form
                    //this.Output(string.Empty, Environment.NewLine + "Found " + gamelist.Count.ToString() + " Updated Games: " + DateTime.Now.ToLongTimeString());

                    if (toDo != null)
                    {
                        foreach (ChessRSSItem game in gamelist)
                        {
                            toDo.ProcessRSSItem(game);
                        }
                    }
                    else
                    {
                        //error?
                    }
                }
                else
                {
                    //this.Output(string.Empty, "No new or updated games found: " + DateTime.Now.ToLongTimeString());
                    //this.LogGames = false;
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

        /// <summary>
        /// Saves new items to the Calendar
        /// </summary>
        internal void Store(EntryList toDo)
        {
            //TODO: this needs to be an asterisk or something on the log form.
            foreach (IChessItem current in toDo)
            {
                this.Save_Game_Info(current, this.UserName, this.Password);
                toDo.Ignore(current); //we won't need to log this one again, 
            }
        }

        private void Save_Game_Info(IRSS_Item gameToLog, string userName, string password)
        {
            if (this.GetPGNs)
            {
                GameProcessor.Download_PGN(gameToLog);
            }

            GoogleCalendar.CreateEntry(userName, 
                                       password, 
                                       gameToLog.Title + " " + DateTime.Parse(gameToLog.PubDate).ToShortTimeString(),
                                            "|" + gameToLog.Link +
                                            Environment.NewLine +
                                            "|" + gameToLog.PubDate +
                                            Environment.NewLine +
                                            "|" + gameToLog.Description +
                                            Environment.NewLine +
                                            "|" + gameToLog.PGN +
                                            Environment.NewLine +
                                        "|" + "this.LogVersion", 
                                        DateTime.Now, 
                                        DateTime.Now, this.Calendar);
            if (this.DebugMode)
            {
                //this.Output.Post(string.Empty, gameToLog.Title + " activity logged " + DateTime.Now.ToShortTimeString());
            }
        }

        private static void Download_PGN(IRSS_Item gameToLog) //TODO: GameType.ChessDotComGame
        {
            //this.Output(string.Empty, "Getting PGN for game: " + gameToLog.Title + " (" + gameToLog.GameID + ")");
            WebClient client = new WebClient();
            Stream strm = client.OpenRead(Constants.CHESS_DOT_COM_PGN_PATH + gameToLog.GameID);
            if (strm != null)
            {
                var sr = new StreamReader(strm);
                gameToLog.PGN = sr.ReadToEnd();
                strm.Close();
            }
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