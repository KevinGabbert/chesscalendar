using System;
using System.Collections.Generic;
using System.Linq;
using ChessCalendar.Interfaces;
using Google.GData.Client;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class GameList : List<IChessItem>
    {
        public bool DebugMode { get; set; }

        public void Remove_Item_With_Guid(string link)
        {
            this.Remove_Item_With_Guid(this, link);
        }
        public void Remove_Item_With_Guid(IList<IChessItem> listToRemoveFrom, string link)
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

        public void AddGame(RssItem rssItem)
        {
            var game = new ChessDotComGame();
            game.Title = rssItem.Title;
            game.Link = rssItem.Link;
            game.PubDate = rssItem.PubDate;
            game.Comments = rssItem.Comments;

            base.Add(game);
        }
        public void AddGame(ChessDotComGame game)
        {
            base.Add(game);
        }

        public void AddGame(AtomEntry atomEntry)
        {
            var game = new ChessDotComGame();

            //game.Title = atomEntry.Title;
            //game.Link = atomEntry.Link;
            //game.PubDate = atomEntry.PubDate;
            //game.Comments = atomEntry.Comments;
            //game.Guid = atomEntry.Guid;

            if (!this.Contains(game))
            {
                base.Add(game);
            }
        }

        public void AddRange(IEnumerable<AtomEntry> atomEntries)
        {
            foreach (var atomEntry in atomEntries)
            {
                this.AddGame(atomEntry);
            }
        }

        protected bool Contains(IChessItem chessItem)
        {
            bool pubMatch = this.Where(thisGame => thisGame.PubDate == chessItem.PubDate).Any();
            bool guidMatch = this.Where(thisGame => thisGame.Link == chessItem.Link).Any();

            return pubMatch && guidMatch;
        }
    }
}
