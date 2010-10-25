using System;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class ChessDotComGame: IComparable, IChessItem
    {
        public string Author { get; set; }
        public string Comments { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public bool StillPosted { get; set; }

        public string Rating
        {
            get
            {
                return ""; //"Description.Split("<br/>".ToArray(), StringSplitOptions.None)[0];
            }
        }
        public int CompareTo(ChessDotComGame other)
        {
            return other.PubDate.CompareTo(this.PubDate);
        }
        public int CompareTo(object obj)
        {
            return this.CompareTo((ChessDotComGame) obj);
        }
    }
}
