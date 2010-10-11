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

        public void AddOrUpdate(RssItem rssItem, bool stillPosted)
        {
            try
            {
                int i = 0;
                foreach (ChessDotComGame entry in this.Where(entry => entry.PubDate == rssItem.PubDate))
                {
                    i++;
                    entry.StillPosted = stillPosted;
                    break;
                }

                if (i == 0)
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
            }
            catch (InvalidOperationException ix)
            {
                
            }
        }
        public void RemoveRepetitiveItems(List<RssItem> newRssItems)
        {
            bool removeNewItem = false;

            //Foreach and Linq won't work when removing from the collection you are checking
            for (int j = newRssItems.Count() - 1; j > -1; j--) 
            {
                for (int i = this.Count() - 1; i > -1; i--) 
                {
                    if (this[i].PubDate == newRssItems[j].PubDate)
                    {
                        this.Remove(this[i]);
                        removeNewItem = true;
                        break;
                    }
                }

                if (removeNewItem)
                {
                    newRssItems.Remove(newRssItems[j]);
                    removeNewItem = false;
                }
            }
        }
    }
}
