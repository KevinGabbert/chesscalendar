using System;
using System.Linq;
using ChessCalendar.Enums;
using ChessCalendar.Interfaces;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class CalendarLogManager : GameList
    {
        #region Properties
            public GameList IgnoreList { private get; set; }
            public bool Beep_On_New_Move { private get; set; }
        #endregion

        public CalendarLogManager(Log logToManage)
        {
            this.Log = logToManage;
            this.IgnoreList = new GameList(logToManage);
        }
        public void Ignore(IChessItem game)
        {
            //if(!IgnoreListHasIt(game))
            //{
                this.Add(this.IgnoreList, game);
            //}
        }
        private void Add(GameList list, IChessItem game)
        {
            try
            {
                //Lets see if we can find a match
                if (list.Where(thisGame => thisGame.PubDate == game.PubDate).Any())
                {
                    list.AddGame(game);
                    //list.Remove_Item_With_Guid(game.Link);
                }
                else
                {
                    list.Remove_Item_With_Guid(game.Link);
                    //list.AddGame(game);
                }
            }
            catch (Exception ex)
            {
                this.Log.Output(string.Empty, ex.Message, OutputMode.Form);
                throw;
            }
        }

        public void ProcessRSSItem(IChessItem rssItem)
        {
            this.IgnoreIfWeHaveIt(rssItem);

            if ((!this.Contains(rssItem)) && (!this.IgnoreListHasIt(rssItem)))
            {
                //Its not in here, and we are not ignoring it..
                if(this.Beep_On_New_Move){Console.Beep();}

                //This needs to color a field green on the Log form.
                //this.Log.Output(string.Empty, "** <NEW> Your Move!: " + rssItem.Title + " *** " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
                rssItem.NewMove = true;
                this.Remove_Any_Older_Versions_Of(rssItem); //Do any necessary cleaning out of previous published items
                this.Add(this, rssItem);
            }
            else
            {
                rssItem.NewMove = false;
                //this.Log.Output(string.Empty, "** Your Move! " + rssItem.Title + " ** " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
                this.Remove_Item_With_Guid(rssItem.Link);
            }
        }
        private bool IgnoreListHasIt(IChessItem rssItem)
        {
            bool ignorePubMatch = this.IgnoreList.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any();
            bool ignoreGuidMatch = this.IgnoreList.Where(thisGame => thisGame.Link == rssItem.Link).Any();

            return ignorePubMatch && ignoreGuidMatch;
        }
        private void IgnoreIfWeHaveIt(IChessItem rssItem)
        {
            if (!this.IgnoreListHasIt(rssItem))
            {
                if (this.Contains(rssItem))
                {
                    this.Ignore(rssItem);
                }
            }
        }
        private void Remove_Any_Older_Versions_Of(IChessItem rssItem)
        {
           //Well, technically, this should be remove any OTHER versions of, but the point is to get rid of
           //previously stored versions, which are older.
           bool pubMatch = this.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any();
           bool guidMatch = this.Where(thisGame => thisGame.Link == rssItem.Link).Any();

           if ((!guidMatch) && (pubMatch))
           {
               //Then we have a new one. So lets get rid of old cruft in preparation for adding.
               this.Remove_Item_With_Guid(rssItem.Link);  
           }

           bool ignorePubMatch = this.IgnoreList.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any();
           bool ignoreGuidMatch = this.IgnoreList.Where(thisGame => thisGame.Link == rssItem.Link).Any();

           if ((ignoreGuidMatch) && (!ignorePubMatch))
           {
               //Then we have a new one. So lets get rid of old cruft in preparation for adding.
               this.IgnoreList.Remove_Item_With_Guid(rssItem.Link);
           }
        }
    }
}
