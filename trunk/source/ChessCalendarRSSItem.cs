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

        public string Rating
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
        public string Move
        {
            get
            {
                return ParseUtility.GetMove(this.Description);
            }
        }
    }
}
