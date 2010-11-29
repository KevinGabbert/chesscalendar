using System;
using System.Collections.Generic;
using System.Linq;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class ChessFeed : List<ChessRSSItem>
    {
        public Uri FeedUri { get; set; }

        public ChessFeed(Uri uriToLoad)
        {
            this.FeedUri = uriToLoad;
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
            this.Clear();

            try
            {
                this.AddRange(RssDocument.Load(this.FeedUri).Channel.Items);
            }
            catch (System.Net.WebException wx)
            {
                if(wx.Message.StartsWith("The remote name could not be resolved"))
                {
                    //TODO:  When site is down we need to handle this error. currently the program will crash.
                    
                    //Add a fake rssItem. currently app cannot process fake rss items
                    //var x = new RSSItem();
                    
                    //x.GameTitle = "site down";
                    //this.Add()
                }
            }
            catch (Exception)
            {
                throw;
            }
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
