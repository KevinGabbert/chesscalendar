using System;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class ChessDotComGame: ChessCalendarRSSItem, IComparable, IChessItem
    {
        public bool StillPosted { get; set; }

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
