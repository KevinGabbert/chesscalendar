using System;
using System.Collections.Generic;
using System.Linq;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class GameList : List<ChessDotComGame>
    {
        public bool DebugMode { get; set; }

        public void Remove_Item_With_Guid(string link)
        {
            this.Remove_Item_With_Guid(this, link);
        }
        public void Remove_Item_With_Guid(IList<ChessDotComGame> listToRemoveFrom, string link)
        {
            for (int i = listToRemoveFrom.Count() - 1; i > -1; i--)
            {
                var currentGame = listToRemoveFrom[i];

                if (currentGame.Link == link)
                {
                    if(this.DebugMode)
                    {
                        Console.WriteLine("Removing Game: " + currentGame.Title); ;
                    }

                    listToRemoveFrom.Remove(currentGame);
                    break;
                }
            }
        }

        public void AddNewGame(RssItem rssItem)
        {
            var game = new ChessDotComGame();
            game.Title = rssItem.Title;
            game.Link = rssItem.Link;
            game.PubDate = rssItem.PubDate;
            game.Comments = rssItem.Comments;
            game.Guid = rssItem.Guid;

            base.Add(game);
        }
        public void AddGame(ChessDotComGame game)
        {
            game.Title = game.Title;
            game.Link = game.Link;
            game.PubDate = game.PubDate;
            game.Comments = game.Comments;
            game.Guid = game.Guid;

            base.Add(game);
        }
    }
}
