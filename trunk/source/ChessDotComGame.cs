using System;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class ChessDotComGame: ChessCalendarRSSItem, IComparable
    {
        private int CompareTo(IChessItem other)
        {
            return other.PubDate.CompareTo(this.PubDate);
        }
        public int CompareTo(object obj)
        {
            return this.CompareTo((ChessDotComGame) obj);
        }
    }
}
