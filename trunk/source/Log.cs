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

        public static string LogVersion { get; set; }

        public static void Log_All_Games(Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            var toDo = new CalendarLogManager();
            _calendarToPost = logToCalendar;

            while (true)
            {
                List<RssItem> newRssItems = RssDocument.Load(uriToWatch).Channel.Items;

                toDo.RemoveRepetitiveItems(newRssItems);
                Log.AddOrUpdate_Games(toDo, newRssItems);

                //On add, and it doesn't already exist, create a reminder. (also create an all day reminder)
                //On remove, delete the all day reminder.

                for (int i = toDo.Count() - 1; i > -1; i--) //Foreach won't work here.
                {
                    ChessDotComGame current = toDo[i];

                    Log.Log_Game(current, userName, password);
                }

                Console.WriteLine("Sleeping for 1 minute");
                Thread.Sleep(new TimeSpan(0, 0, 1, 0));
            }
        }

        private static void Log_Game(ChessDotComGame gameToLog, string userName, string password)
        {
            Console.WriteLine("Logging " + gameToLog.Title + " to Calendar: " + _calendarToPost.OriginalString);
            GoogleCalendar.CreateEntry(userName, password, gameToLog.Title, gameToLog.Link + 
                                                                            Environment.NewLine + 
                                                                            gameToLog.Description + 
                                                                            Environment.NewLine + 
                                                                            Log.LogVersion, DateTime.Now, DateTime.Now, _calendarToPost);
            //DateTime.Parse(gameToLog.PubDate)
            Console.WriteLine(gameToLog.Title + " activity logged " + DateTime.Now.ToShortTimeString());
        }    
        private static void AddOrUpdate_Games(CalendarLogManager toDo, IEnumerable<RssItem> gamelist)
        {
            if (toDo.Count > 0)
            {
                Console.WriteLine("Found " + toDo.Count.ToString() + " Games to Log: " + DateTime.Now.ToLongTimeString());
                foreach (RssItem game in gamelist)
                {
                    Log.LogDetect(game);
                    toDo.AddOrUpdate(game, true);
                }
            }
            else
            {
                Console.WriteLine("No new or updated games found: " + DateTime.Now.ToLongTimeString());
            }
        }
        private static void LogDetect(RssItem game)
        {
            Console.WriteLine(game.Title + DETECTED + DateTime.Now.ToShortTimeString());
        }

        private static void Reset(IEnumerable<ChessDotComGame> toDo)
        {
            //we no longer know if they are still running.
            foreach (ChessDotComGame process in toDo)
            {
                process.StillPosted = false;
            }
        }

    }
}
