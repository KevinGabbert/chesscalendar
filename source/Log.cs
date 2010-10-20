using System;
using System.Collections.Generic;
using System.Threading;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class Log
    {
        #region Properties

            public const string DETECTED = " detected ";
            public static Uri _calendarToPost = new Uri("http://www.google.com/calendar/feeds/default/private/full"); //default string. probably not even needed, but its helpful to know the format.
            public static bool LogGames { get; set; }
            public static string LogVersion { get; set; }
            public static bool DebugMode { get; set; }
            public static bool Beep_On_New_Move { get; set; }
            public static CalendarLogManager ToDo { get; set; }
            public static System.Windows.Forms.NotifyIcon NotifyIcon { get; set; }
            public static System.Windows.Forms.ContextMenu ContextMenu { get; set; }
            public static OutputMode OutputMode { get; set; }

        #endregion

        public static void Log_All_Games(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            Log.ToDo = new CalendarLogManager();
            Log.ToDo.DebugMode = Log.DebugMode;
            Log.ToDo.Beep_On_New_Move = Log.Beep_On_New_Move;

            _calendarToPost = logToCalendar;

            //load up Ignore list with items already in calendar

            //get all "auto-logger" entries created in the last 15 days (the max time you can have a game)
            //TODO: well, you can actually also go on vacation, which would make it longer but this first version doesn't accomodate for that..

            Log.Output(string.Empty, "Querying Calendar for older entries");
            Log.ToDo.IgnoreList = GoogleCalendar.GetExistingChessGames(userName, password, _calendarToPost, DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)), DateTime.Now, "auto-logger");

            while (true)
            {
                ChessCalendarRSSItems newRssItems = new ChessCalendarRSSItems();

                try
                {
                    newRssItems.AddRange(RssDocument.Load(uriToWatch).Channel.Items);

                    Log.AddOrUpdate_Games(Log.ToDo, newRssItems);

                    //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                    //On remove, delete the all day reminder.

                    if (Log.LogGames)
                    {
                        //This should ONLY show up on the form.
                        Log.Output(string.Empty, "Logging " + Log.ToDo.Count + " Notifications to Calendar..", OutputMode.Form);
                        foreach (ChessDotComGame current in Log.ToDo)
                        {
                            Log.Log_Game(current, userName, password);
                        }
                    }

                    Log.ToDo.Clear();
                    //Log.Output(string.Empty, "----------" + Environment.NewLine, OutputMode.Form);
                }
                catch (Exception ex)
                {
                    //if 504 Invalid gateway error. Chess.com is down
                    Log.Output(string.Empty, "error. " + ex.Message);

                    GoogleCalendar.CreateEntry(userName, password, "Error", "Error", "Chess.com error", ex.Message +
                                                                Log.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
                }

                //Log.Output(string.Empty, "Sleeping for 5 minutes", OutputMode.Form);
                Thread.Sleep(new TimeSpan(0, 0, 5, 0));
            }
        }
        private static void Log_Game(ChessDotComGame gameToLog, string userName, string password)
        {
            if (Log.DebugMode)
            {
                Log.Output(string.Empty,"Logging " + gameToLog.Title + " to Calendar: " + _calendarToPost.OriginalString, OutputMode.Form);
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
                                                            "|" + Log.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            if (Log.DebugMode)
            {
                Log.Output(string.Empty, gameToLog.Title + " activity logged " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
            }

            Log.ToDo.Ignore(gameToLog); //we won't need to log this one again, 
        }    
        private static void AddOrUpdate_Games(CalendarLogManager toDo, ICollection<ChessCalendarRSSItem> gamelist)
        {
            if (gamelist != null)
            {
                if (gamelist.Count > 0)
                {
                    Log.LogGames = true;
                    Log.Output(string.Empty, Environment.NewLine, OutputMode.Form);
                    Log.Output(string.Empty, "Found " + gamelist.Count.ToString() + " Updated Games: " + DateTime.Now.ToLongTimeString());
                    
                    //if form mode
                    Log.Output(string.Empty, Environment.NewLine, OutputMode.Form);
                    foreach (ChessCalendarRSSItem game in gamelist)
                    {
                        toDo.ProcessRSSItem(game);
                    }
                }
                else
                {
                    Log.Output(string.Empty, "No new or updated games found: " + DateTime.Now.ToLongTimeString());
                    Log.LogGames = false;
                }
            }
        }

        public static void Output(string title, string outputMessage)
        {
            switch (OutputMode)
            {
                case OutputMode.Balloon:
                    Log.NotifyIcon.BalloonTipText = outputMessage;
                    Log.NotifyIcon.ShowBalloonTip(2000);

                    //Now Write message to form as well.
                    break;

                case OutputMode.Form:
                    //Write message to form.
                    break;

                default:
                    break;
            }
        }
        public static void Output(string title, string outputMessage, OutputMode outputMode)
        {
            if(outputMode == OutputMode.Form)
            {
                //Write message to form.
            }
        }
    }
}
