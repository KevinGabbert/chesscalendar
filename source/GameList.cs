﻿using System.Collections.Generic;
using System.Linq;
using ChessCalendar.Interfaces;
using Google.GData.Client;

namespace ChessCalendar
{
    public class GameList : List<IChessItem>
    {
        #region Properties

            public bool DebugMode { get; set; }

        #endregion

        public GameList()
        {

        }

        public void Remove_Item_With_Guid(string link)
        {
            this.Remove_Item_With_Guid(this, link);
        }
        private void Remove_Item_With_Guid(IList<IChessItem> listToRemoveFrom, string link)
        {
            for (int i = listToRemoveFrom.Count() - 1; i > -1; i--)
            {
                var currentGame = listToRemoveFrom[i];

                if (currentGame.Link == link)
                {
                    if(this.DebugMode)
                    {
                        //this.Processor.Output(string.Empty, "Removing Game: " + currentGame.Title, OutputMode.Form);
                    }

                    listToRemoveFrom.Remove(currentGame);
                    break;
                }
            }
        }

        public void AddGame(IChessItem game)
        {
            base.Add(game);
        }
        private void AddGame(AtomEntry atomEntry)
        {
            foreach (AtomEntry entry in atomEntry.Feed.Entries)
            {
                if (atomEntry.Content.Content.Contains("|"))
                {
                    var game = new ChessRSSItem();

                    game.Title = entry.Title.Text.ToString();
                    game.Link = atomEntry.Content.Content.Split('|')[1];
                    game.Link = game.Link.Remove(game.Link.Length - 1);

                    game.PubDate = atomEntry.Content.Content.Split('|')[2];
                    game.PubDate = game.PubDate.Remove(game.PubDate.Length - 1);

                    game.Description = atomEntry.Content.Content;

                    if (!this.Contains(game))
                    {
                        base.Add(game);
                    }
                }
            }
        }
        public void AddRange(IEnumerable<AtomEntry> atomEntries)
        {
            foreach (var atomEntry in atomEntries)
            {
                this.AddGame(atomEntry);
            }
        }

        protected new bool Contains(IChessItem chessItem)
        {
            bool pubMatch = this.Where(thisGame => thisGame.PubDate == chessItem.PubDate).Any();
            bool guidMatch = this.Where(thisGame => thisGame.Link == chessItem.Link).Any();

            return pubMatch && guidMatch;
        }
    }
}
