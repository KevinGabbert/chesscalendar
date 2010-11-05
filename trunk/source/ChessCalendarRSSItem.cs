using System;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class ChessCalendarRSSItem: RssToolkit.Rss.RssItem, IChessItem
    {
        public bool NewMove { get; set; }
        public string Message { get; set; }
        public string PGN { get; set; }
        public string GameID
        {
            get
            {
                return ParseUtility.GetGameID(this.Link);
            }
        }

        public string RatingRaw
        {
            get
            {
                return ParseUtility.GetRating(this.Description);
            }
        }
        public string TimeLeftRaw
        {
            get
            {
                return ParseUtility.GetTimeLeftRaw(this.Description);
            }
        }
        public System.DateTime TimeLeft
        {
            get
            {
                return ParseUtility.GetTimeLeft(this.Description);
            }
        }
        public string MoveRaw
        {
            get
            {
                return ParseUtility.GetMove(this.Description);
            }
        }


        public int Rating
        {
            get { throw new System.NotImplementedException(); }
        }

        public int Move
        {
            get { throw new System.NotImplementedException(); }
        }

        public string GameTitle
        {
            get
            {
                return ParseUtility.GetGameTitle(this.Title);
            }
        }

        public string Opponent
        {
            get
            {
                return ParseUtility.GetOpponent(this.Title);
            }
        }
    }
}
