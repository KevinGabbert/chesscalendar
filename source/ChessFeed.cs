using System;
using System.Collections.Generic;
using System.Linq;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class ChessFeed : List<ChessRSSItem>
    {
        public Uri FeedUri { get; set; }

        public ChessFeed()
        {

        }

        public ChessFeed(Uri uriToLoad)
        {
            this.Load(uriToLoad);
        }

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

        public void Load()
        {
            this.AddRange(RssDocument.Load(this.FeedUri).Channel.Items);
        }

        public void Load(Uri uriToLoad)
        {
            this.AddRange(RssDocument.Load(uriToLoad).Channel.Items);
        }

        public IEnumerable<string> GetOpponents()
        {
            return this.Select(chessRssItem => ParseUtility.GetOpponent(chessRssItem.Title)).ToList();
        }

        public List<string> GetGames()
        {
            return this.Select(chessRssItem => ParseUtility.GetGameTitle(chessRssItem.Title)).ToList();
        }
        public List<string> GetGamesAndTitle()
        {
            return this.Select(chessRssItem => ParseUtility.GetGameTitle(chessRssItem.Title) + "  ~  " + ParseUtility.GetOpponent(chessRssItem.Title)).ToList();
        }
    }
}
