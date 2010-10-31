using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using ChessCalendar.Enums;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class Log: OutputClass
    {
        public const string CHESS_DOT_COM_PGN_PATH = "http://www.chess.com/echess/download_pgn.html?id=";
        public const string GOOGLE_CALENDAR_DEFAULT = "http://www.google.com/calendar/feeds/default/private/full";
        public const string DETECTED = " detected ";

        #region Properties

            public Uri _calendarToPost = new Uri(GOOGLE_CALENDAR_DEFAULT);
            public System.Windows.Forms.ContextMenu ContextMenu { get; set; }
            public CalendarLogManager ToDo { get; set; }

            public string LogVersion { get; set; }
            public int WaitSeconds { get; set; }
            public int WaitProgress { get; set; }
            public DateTime NextCheck { get; set; }

            public bool NewMessage { get; set; }
            public bool DebugMode { get; set; }
            public bool Beep_On_New_Move { get; set; }
            public bool GetPGNs { get; set; }
            public bool LogGames { get; set; }

        #endregion

        public Log()
        {
            this.Messages = new Queue<string>();
            this.WaitSeconds = 10;
        }

        public void Log_All_Games(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            this.ToDo = new CalendarLogManager(this);
            this.ToDo.DebugMode = this.DebugMode;
            this.ToDo.Beep_On_New_Move = this.Beep_On_New_Move;

            _calendarToPost = logToCalendar;

            //load up Ignore list with items already in calendar

            //get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
            //TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..

            this.Output(string.Empty, "Querying Calendar for older entries");
            this.ToDo.IgnoreList = GoogleCalendar.GetExistingChessGames(userName, password, _calendarToPost, DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)), DateTime.Now, "auto-logger");

            while (true)
            {
                ChessCalendarRSSItems newRssItems = new ChessCalendarRSSItems();
                    
                try
                {
                    newRssItems.AddRange(RssDocument.Load(uriToWatch).Channel.Items);

                    this.AddOrUpdate_Games(this.ToDo, newRssItems);

                    //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                    //On remove, delete the all day reminder.

                    this.ProcessNewRSSItems(newRssItems);

                    if (this.LogGames)
                    {
                        this.NewMessage = true;

                        //This should ONLY show up on the form.
                        this.Output(string.Empty, "Logging " + this.ToDo.Count + " Notifications to Calendar..", OutputMode.Form);
                        foreach (ChessDotComGame current in this.ToDo)
                        {
                            this.Output(current);
                            this.Log_Game(current, userName, password);
                        }
                    }

                    this.ToDo.Clear();
                    this.Output(string.Empty, "----------" + Environment.NewLine, OutputMode.Form);
                }
                catch (Exception ex)
                {
                    //if 504 Invalid gateway error. Chess.com is down
                    this.Output(string.Empty, "error. " + ex.Message);

                    GoogleCalendar.CreateEntry(userName, password, "Error", "Error", "Chess.com error", ex.Message +
                                                                this.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
                }

                this.Output(string.Empty, "Sleeping for " + this.WaitSeconds + " seconds.");
                Wait(this.WaitSeconds);
            }
        }

        private void ProcessNewRSSItems(IEnumerable<ChessCalendarRSSItem> newRssItems)
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
            }

            this.NewMessage = false;
        }

        private void Log_Game(ChessDotComGame gameToLog, string userName, string password)
        {
            if (this.DebugMode)
            {
                this.Output(string.Empty, "Logging " + gameToLog.Title + " to Calendar: " + _calendarToPost.OriginalString);
            }

            if (this.GetPGNs)
            {
                this.Download_PGN(gameToLog);
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

        private void Download_PGN(ChessDotComGame gameToLog)
        {
            this.Output(string.Empty, "Getting PGN for game: " + gameToLog.Title + " (" + gameToLog.GameID + ")");
            WebClient client = new WebClient();
            Stream strm = client.OpenRead(CHESS_DOT_COM_PGN_PATH + gameToLog.GameID);
            if (strm != null)
            {
                var sr = new StreamReader(strm);
                gameToLog.PGN = sr.ReadToEnd();
                strm.Close();
            }
        }

        private void AddOrUpdate_Games(CalendarLogManager toDo, ICollection<ChessCalendarRSSItem> gamelist)
        {
            if (gamelist != null)
            {
                if (gamelist.Count > 0)
                {
                    this.LogGames = true;
                    this.Output(string.Empty, Environment.NewLine + "Found " + gamelist.Count.ToString() + " Updated Games: " + DateTime.Now.ToLongTimeString());
                    
                    this.Output(string.Empty, Environment.NewLine, OutputMode.Form);
                    foreach (ChessCalendarRSSItem game in gamelist)
                    {
                        toDo.ProcessRSSItem(game);
                    }
                }
                else
                {
                    this.Output(string.Empty, "No new or updated games found: " + DateTime.Now.ToLongTimeString());
                    this.LogGames = false;
                }
            }
        }
    }
}