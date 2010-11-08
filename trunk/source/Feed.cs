using System.Collections.Generic;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class Feed : List<ChessRSSItem>
    {
        public void Add(RssItem rssItem)
        {
            var newItem = new ChessRSSItem();
            newItem.Author = rssItem.Author;
            newItem.Comments = rssItem.Comments;
            newItem.Description = rssItem.Description;
            newItem.Link = rssItem.Link;
            newItem.PubDate = rssItem.PubDate;
            newItem.Title = rssItem.Title;

            base.Add(newItem); 
        }
        public void AddRange(IEnumerable<RssItem> rssItems)
        {
            foreach (var rssItem in rssItems)
            {
                this.Add(rssItem);
            }
        }
    }
}
