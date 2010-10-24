using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ChessCalendar.Enums;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class Log: OutputClass
    {
        public Log()
        {
            this.Messages = new Queue<string>();
        }

        #region Properties

            public const string DETECTED = " detected ";
            public Uri _calendarToPost = new Uri("http://www.google.com/calendar/feeds/default/private/full"); //default string. probably not even needed, but its helpful to know the format.
            public bool LogGames { get; set; }
            public string LogVersion { get; set; }
            public bool DebugMode { get; set; }
            public bool Beep_On_New_Move { get; set; }
            public CalendarLogManager ToDo { get; set; }
            
            public System.Windows.Forms.ContextMenu ContextMenu { get; set; }
            
            public int WaitProgress { get; set; }
            public DateTime NextCheck { get; set; }
            public bool NewMessage { get; set; }

        #endregion

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
                //if(this.CheckAgain)
                //{
                    ChessCalendarRSSItems newRssItems = new ChessCalendarRSSItems();
                    
                    try
                    {
                        newRssItems.AddRange(RssDocument.Load(uriToWatch).Channel.Items);

                        this.AddOrUpdate_Games(this.ToDo, newRssItems);

                        //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                        //On remove, delete the all day reminder.

                        if (this.LogGames)
                        {
                            this.NewMessage = true;
                            //This should ONLY show up on the form.
                            this.Output(string.Empty, "Logging " + this.ToDo.Count + " Notifications to Calendar..", OutputMode.Form);
                            foreach (ChessDotComGame current in this.ToDo)
                            {
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

                    this.Output(string.Empty, "Sleeping for 5 minutes");
                    Wait(5);
                //}
            }
        }

        private  void Wait(int waitMinutes)
        {
            DateTime start = DateTime.Now;
            TimeSpan waitTime = new TimeSpan(0, 0, waitMinutes, 0);

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

            GoogleCalendar.CreateEntry(userName, password, DateTime.Parse(gameToLog.PubDate).ToLongDateString(),
                                                            gameToLog.Link, 
                                                            gameToLog.Title + " " + DateTime.Parse(gameToLog.PubDate).ToShortTimeString(), 
                                                            "|" + gameToLog.Link + 
                                                            Environment.NewLine +
                                                            "|" + gameToLog.PubDate +
                                                            Environment.NewLine +
                                                            "|" + gameToLog.Description +
                                                            Environment.NewLine +
                                                            "|" + this.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            if (this.DebugMode)
            {
                this.Output(string.Empty, gameToLog.Title + " activity logged " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
            }

            this.ToDo.Ignore(gameToLog); //we won't need to log this one again, 
        }    
        private void AddOrUpdate_Games(CalendarLogManager toDo, ICollection<ChessCalendarRSSItem> gamelist)
        {
            if (gamelist != null)
            {
                if (gamelist.Count > 0)
                {
                    this.LogGames = true;
                    this.Output(string.Empty, Environment.NewLine + "Found " + gamelist.Count.ToString() + " Updated Games: " + DateTime.Now.ToLongTimeString());
                    
                    //if form mode
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
