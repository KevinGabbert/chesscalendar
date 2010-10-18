using System.Collections.Generic;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class ChessCalendarRSSItems : List<ChessCalendarRSSItem>
    {
        public void AddRange(IEnumerable<RssItem> rssItems)
        {
            foreach (var rssItem in rssItems)
            {
                ChessCalendarRSSItem newItem = new ChessCalendarRSSItem();
                newItem.Author = rssItem.Author;
                newItem.Comments = rssItem.Comments;
                newItem.Description = rssItem.Description;
                newItem.Link = rssItem.Link;
                newItem.PubDate = rssItem.PubDate;
                newItem.Title = rssItem.Title;

                this.Add(newItem);
            }
        }
    }
}
