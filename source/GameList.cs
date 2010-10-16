using System;
using System.Collections.Generic;
using System.Linq;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class GameList : List<ChessDotComGame>
    {
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
                    Console.WriteLine("Removing Game: " + currentGame.Title);
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
    }
}
