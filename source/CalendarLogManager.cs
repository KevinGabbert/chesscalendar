using System;
using System.Collections.Generic;
using System.Linq;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class CalendarLogManager : List<ChessDotComGame>
    {
        public new void Add(ChessDotComGame process)
        {
            base.Add(process);  
        }
        public void Add(RssItem rssItem)
        {
            var game = new ChessDotComGame();
            game.Title = rssItem.Title;
            game.Link = rssItem.Link;
            game.PubDate = rssItem.PubDate;
            game.Comments = rssItem.Comments;
            game.Guid = rssItem.Guid;

            base.Add(game);
            Console.WriteLine("Added " + game.Title + " " + game.PubDate);
        }

        public void AddOrDeletePassedDupe(RssItem rssItem)
        {
            try
            {
                bool skipAdd = false;
                for (int i = this.Count() - 1; i > -1; i--)
                {
                    if (this[i].PubDate == rssItem.PubDate)
                    {
                        //this.Remove(this[i]);
                        skipAdd = true;
                        break;
                    }
                }

                if (!skipAdd)
                {
                    this.Add(rssItem);
                }
            }
            catch (InvalidOperationException ix)
            {
                
            }
        }
    }
}
