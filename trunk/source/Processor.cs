﻿using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using ChessCalendar.Enums;
using ChessCalendar.Interfaces;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class ProcessorManager_Deprecated: OutputClass
    {
        public const string CHESS_DOT_COM_PGN_PATH = "http://www.chess.com/echess/download_pgn.html?id=";

        private bool _stop = false;

        #region Properties

            public Uri _calendarToPost;
            public System.Windows.Forms.ContextMenu ContextMenu { get; set; }

            public List<CalendarProcessor> CCFeeds { get; set; }

            //TODO: Deprecate this
            public List<Feed> Feeds { get; set; }

            public EntryList ToDo { get; set; }

            public string LogVersion { get; set; }
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

            public bool NewMessage { get; set; }
            public bool DebugMode { get; set; }
            public bool Beep_On_New_Move { get; set; }
            public bool GetPGNs { get; set; }
            public bool LogGames { get; set; }
            public bool ResetWait { get; set; }
            public string UserLogged { get; set; }
           
            public bool ClearList { get; set; }//needs a better name.
 
        #endregion

        /// <summary>
        /// This object is intended to be consumed by one subscriber per instantiation.
        /// </summary>
        public ProcessorManager_Deprecated()
        {
            this.Feeds = new List<Feed>();
            this.Messages = new Queue<string>();

            this.WaitSeconds = 1000;
        }

        #region Under Construction

        /// <summary>
        /// //user needs to call Process_All_Feeds() to start
        /// </summary>
        /// <param name="uriToWatch"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="logToCalendar"></param>
        /// <param name="post"></param>
        /// <param name="save"></param>
        public void Add_Feed(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool post, bool save)
        {
            var newFeed = new CalendarProcessor(uriToWatch, userName, password, logToCalendar);
            newFeed.Post = post;
            newFeed.Save = save;

            this.CCFeeds.Add(newFeed);
        }

        public void Process_Feed(Uri uriToWatch, string userName, string password, Uri logToCalendar, bool post, bool save)
        {
            var newFeed = new CalendarProcessor(uriToWatch, userName, password, logToCalendar);
            newFeed.Post = post;
            newFeed.Save = save;

            newFeed.Go();
        }
  
        /// <summary>
        /// Intended to take the place of Save_Single_Feed_To_Calendar
        /// The purpose here is to parse all the feeds into this object structure, outputting to base.NewMoves so that the Subscriber can Dequeue them,
        /// then saving to Calendar
        /// </summary>
        public void Process_ALL_Feeds(bool postAll, bool saveAll)
        {
            foreach (CalendarProcessor feed in this.Feeds)
            {
                feed.Post = postAll;
                feed.Save = saveAll;
                //feed.Go();
            }
        }

        public void Process_Feed(string userName)
        {
            foreach (CalendarProcessor feed in this.Feeds)
            {
                 feed.Go(feed.GetOpponents());
            }
        }

        #endregion

        /// <summary>
        /// This function will eventually be made irrelevant, and may be deleted.
        /// </summary>
        /// <param name="uriToWatch"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="logToCalendar"></param>
        public void Save_Single_Feed_To_Calendar(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            this.ToDo = new EntryList();
            this.ToDo.DebugMode = this.DebugMode;
            this.ToDo.Beep_On_New_Move = this.Beep_On_New_Move;

            _calendarToPost = logToCalendar;

            //load up Ignore list with items already in calendar

            //get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
            //TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..

            this.ToDo.IgnoreList = GoogleCalendar.GetAlreadyLoggedChessGames(userName, password, _calendarToPost, DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)), DateTime.Now, "auto-logger");

            while (true)
            {
                if (!this._stop)
                {
                    var newFeed = new Feed();
                    this.Feeds.Add(newFeed);

                    this.Save_To_Calendar(newFeed, userName, password, uriToWatch);
                }
                else
                {
                    Application.DoEvents();
                }
            }
        }

        private void Save_To_Calendar(Feed feedToSave, string userName, string password, Uri uriToWatch)
        {
            try
            {
                feedToSave.AddRange(RssDocument.Load(uriToWatch).Channel.Items);

                this.AddOrUpdate_Games(this.ToDo, feedToSave);

                //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                //On remove, delete the all day reminder.

                this.ClearList = false;

                this.ProcessNewRSSItems(feedToSave); //If there are *new* entries, then the gridview will clear and refresh

                //TODO: if NO new entries, it won't clear & refresh, meaning cruft won't 

                if (this.LogGames)
                {
                    //TODO: this needs to be an asterisk or something on the log form.
                    foreach (IChessItem current in this.ToDo)
                    {
                        this.Output(current); //TODO this causes gridview to clear and refresh
                        this.Save_Game_Info(current, userName, password);
                    }
                }

                this.ClearList = true;

                this.ToDo.Clear();
            }
            catch (Exception ex)
            {
                //if 504 Invalid gateway error. Chess.com is down
                this.Output(string.Empty, "error. " + ex.Message);

                GoogleCalendar.CreateEntry(userName, password, "Error", "Error", "Chess.com error", ex.Message +
                                                                                                    this.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            }

            this.Wait(this.WaitSeconds);
        }

        private void ProcessNewRSSItems(IEnumerable<ChessRSSItem> newRssItems)
        {
            foreach (var item in newRssItems)
            {
                this.Output(item);
            }
        }

        private  void Wait(int waitSeconds)
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

                if(this.ResetWait)
                {
                    this.ResetWait = false;
                    break;
                }

                if(this._stop)
                {
                    this.WaitProgress = 0;
                    this.NextCheck = new DateTime();
                    break;
                }
            }

            this.NewMessage = false;
        }

        private void Save_Game_Info(IChessItem gameToLog, string userName, string password)
        {
            if (this.DebugMode)
            {
                this.Output(string.Empty, "Logging " + gameToLog.Title + " to Calendar: " + _calendarToPost.OriginalString);
            }

            if (this.GetPGNs)
            {
                ProcessorManager_Deprecated.Download_PGN(gameToLog);
            }

            GoogleCalendar.CreateEntry(userName, password, DateTime.Parse(gameToLog.PubDate).ToLongDateString(),
                                                            gameToLog.Link, 
                                                            gameToLog.Title + " " + DateTime.Parse(gameToLog.PubDate).ToShortTimeString(), 
                                                            "|" + gameToLog.Link + 
                                                            Environment.NewLine +
                                                            "|" + gameToLog.PubDate +
                                                            Environment.NewLine +
                                                            "|" + gameToLog.Description +
                                                            Environment.NewLine +
                                                            "|" + gameToLog.PGN +
                                                            Environment.NewLine +
                                                            "|" + this.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            if (this.DebugMode)
            {
                this.Output(string.Empty, gameToLog.Title + " activity logged " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
            }

            this.ToDo.Ignore(gameToLog); //we won't need to log this one again, 
        }

        private static void Download_PGN(IChessItem gameToLog) //TODO: GameType.ChessDotComGame
        {
            //this.Output(string.Empty, "Getting PGN for game: " + gameToLog.Title + " (" + gameToLog.GameID + ")");
            WebClient client = new WebClient();
            Stream strm = client.OpenRead(CHESS_DOT_COM_PGN_PATH + gameToLog.GameID);
            if (strm != null)
            {
                var sr = new StreamReader(strm);
                gameToLog.PGN = sr.ReadToEnd();
                strm.Close();
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

        internal void Stop()
        {
            this._stop = true;
        }
        internal void Go()
        {
            this._stop = false;
        }
    }
}