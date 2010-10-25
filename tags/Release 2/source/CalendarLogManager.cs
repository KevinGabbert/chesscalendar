﻿using System;
using System.Linq;
using ChessCalendar.Enums;
using RssToolkit.Rss;

namespace ChessCalendar
{
    public class CalendarLogManager : GameList
    {
        #region Properties
            public GameList IgnoreList { get; set; }
            public bool Beep_On_New_Move { get; set; }
        #endregion

        public CalendarLogManager(Log logToManage)
        {
            this.Log = logToManage;
            this.IgnoreList = new GameList(logToManage);
        }

        public new void Add(ChessDotComGame game)
        {
            base.Add(game);  
        }
        public void Ignore(RssItem rssItem)
        {
            this.Add(this.IgnoreList, rssItem);
        }
        public void Ignore(ChessDotComGame game)
        {
            this.Add(this.IgnoreList, game);
        }
        public void Add(GameList list, RssItem rssItem)
        {
            try
            {
                //Lets see if we can find a match
                if (list.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any())
                {
                    list.Remove_Item_With_Guid(rssItem.Link);
                }
                else
                {
                    list.AddGame(rssItem);
                }
            }
            catch (Exception ex)
            {
                this.Log.Output(string.Empty, ex.Message, OutputMode.Form);
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void Add(GameList list, ChessDotComGame game)
        {
            try
            {
                //Lets see if we can find a match
                if (list.Where(thisGame => thisGame.PubDate == game.PubDate).Any())
                {
                    list.Remove_Item_With_Guid(game.Link);
                }
                else
                {
                    list.AddGame(game);
                }
            }
            catch (Exception ex)
            {
                this.Log.Output(string.Empty, ex.Message, OutputMode.Form);
                throw;
            }
        }

        public void ProcessRSSItem(ChessCalendarRSSItem rssItem)
        {
            this.IgnoreIfWeHaveIt(rssItem);

            if ((!this.Contains(rssItem)) && (!this.IgnoreListHasIt(rssItem)))
            {
                //Its not in here, and we are not ignoring it..
                if(this.Beep_On_New_Move){Console.Beep();}

                this.Log.Output(string.Empty, "** <NEW> Your Move!: " + rssItem.Title + " *** " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
                this.Remove_Any_Older_Versions_Of(rssItem); //Do any necessary cleaning out of previous published items
                this.Add(this, rssItem);
            }
            else
            {
                this.Log.Output(string.Empty, "** Your Move! " + rssItem.Title + " ** " + DateTime.Now.ToShortTimeString(), OutputMode.Form);
                this.Remove_Item_With_Guid(rssItem.Link);
            }
        }
        private bool IgnoreListHasIt(RssItem rssItem)
        {
            bool ignorePubMatch = this.IgnoreList.Where(thisGame => thisGame.PubDate == rssItem.PubDate).Any();
            bool ignoreGuidMatch = this.IgnoreList.Where(thisGame => thisGame.Link == rssItem.Link).Any();

            return ignorePubMatch && ignoreGuidMatch;
        }
        private void IgnoreIfWeHaveIt(ChessCalendarRSSItem rssItem)
        {
            if (!this.IgnoreListHasIt(rssItem))
            {
                if (this.Contains(rssItem))
                {
                    this.Ignore(rssItem);
                }
            }
        }
        private void Remove_Any_Older_Versions_Of(RssItem rssItem)
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