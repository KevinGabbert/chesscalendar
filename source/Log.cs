using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RssToolkit.Rss;

namespace ChessCalendar
{
    class Log
    {
        public const string DETECTED = " detected ";
        public static Uri _calendarToPost = new Uri("http://www.google.com/calendar/feeds/default/private/full"); //default string. probably not even needed, but its helpful to know the format.
        public static bool _logGames = false;

        public static string LogVersion { get; set; }
        public static bool DebugMode { get; set; }

        public static void Log_All_Games(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            var toDo = new CalendarLogManager();
            toDo.DebugMode = Log.DebugMode;

            _calendarToPost = logToCalendar;

            while (true)
            {
                List<RssItem> newRssItems = new List<RssItem>();

                try
                {
                    newRssItems.AddRange(RssDocument.Load(uriToWatch).Channel.Items);
                    Log.AddOrUpdate_Games(toDo, newRssItems);

                    //TODO On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                    //On remove, delete the all day reminder.

                    foreach (ChessDotComGame current in toDo.Where(current => Log._logGames))
                    {
                        Log.Log_Game(current, userName, password);
                    }

                    //Console.WriteLine("Games Logged to Calendar.");
                }
                catch (Exception ex)
                {
                    //if 504 Invalid gateway error. Chess.com is down
                    Console.WriteLine("error. " + ex.Message);

                    GoogleCalendar.CreateEntry(userName, password, "Chess.com error", ex.Message +
                                                                Log.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
                }

                Console.WriteLine("Sleeping for 5 minutes");
                Thread.Sleep(new TimeSpan(0, 0, 5, 0));
            }
        }

        private static void Log_Game(ChessDotComGame gameToLog, string userName, string password)
        {
            if (Log.DebugMode)
            {
                Console.WriteLine("Logging " + gameToLog.Title + " to Calendar: " + _calendarToPost.OriginalString);
            }

            GoogleCalendar.CreateEntry(userName, password, gameToLog.Title, gameToLog.Link + 
                                                                            Environment.NewLine + 
                                                                            gameToLog.Description + 
                                                                            Environment.NewLine + 
                                                                            Log.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            if (Log.DebugMode)
            {
                Console.WriteLine(gameToLog.Title + " activity logged " + DateTime.Now.ToShortTimeString());
            }
        }    
        private static void AddOrUpdate_Games(CalendarLogManager toDo, ICollection<RssItem> gamelist)
        {
            if (gamelist != null)
            {
                if (gamelist.Count > 0)
                {
                    Log._logGames = true;
                    Console.WriteLine("Found " + gamelist.Count.ToString() + " Games: " + DateTime.Now.ToLongTimeString());
                    foreach (RssItem game in gamelist)
                    {
                        toDo.ProcessRSSItem(game);
                    }
                }
                else
                {
                    Console.WriteLine("No new or updated games found: " + DateTime.Now.ToLongTimeString());
                    Log._logGames = false;
                }
            }
        }
    }
}
