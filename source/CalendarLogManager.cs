using System;
using System.Collections.Generic;
using System.Linq;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class GameList: List<ChessDotComGame>
    {
        public void Remove_Item_With_Guid(RssGuid guid)
        {
            this.Remove_Item_With_Guid(this, guid);
        }
        public void Remove_Item_With_Guid(IList<ChessDotComGame> listToRemoveFrom, RssGuid guid)
        {
            //.Remove(select from _ignore where Guid is "x")
            for (int i = listToRemoveFrom.Count() - 1; i > -1; i--)
            {
                if (listToRemoveFrom[i].Guid == guid)
                {
                    listToRemoveFrom.Remove(listToRemoveFrom[i]);
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
            Console.WriteLine("Added " + game.Title + " " + game.PubDate);
        }
    }

    public class CalendarLogManager : GameList
    {
        #region Properties

            private GameList _ignore = new GameList();
            public GameList Ignore
            {
                get { return _ignore; }
                set { _ignore = value; }
            }

        #endregion

        public new void Add(ChessDotComGame process)
        {
            base.Add(process);  
        }
        public void Add(RssItem rssItem)
        {
            this.Add(this.Ignore, rssItem);
        }
        public void Add(GameList list, RssItem rssItem)
        {
            try
            {
                //Lets see if we can find a match
                if (list.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any())
                {
                    list.Remove_Item_With_Guid(rssItem.Guid);
                }
                else
                {
                    list.AddNewGame(rssItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void ProcessItem(RssItem rssItem)
        {
            this.Remove_If_Deprecated(rssItem);
            this.Add(this, rssItem);
        }

        private void Remove_If_Deprecated(RssItem rssItem)
        {
           bool pubMatch = this.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any();
           bool guidMatch = this.Where(thisGame => thisGame.Guid == rssItem.Guid).Any();

           //Look for newer versions of old games
           if ((guidMatch) && (!pubMatch))
           {
               //Then we have a new one. So lets get rid of it.
               this.Remove_Item_With_Guid(rssItem.Guid);
               this.Ignore.Remove_Item_With_Guid(rssItem.Guid);
           }
        }
    }
}
