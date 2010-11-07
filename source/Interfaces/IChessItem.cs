using System;

namespace ChessCalendar.Interfaces
{
    /// <summary>
    /// Common Chess props intended for use by all classes and controls
    /// </summary>
    public interface IChessItem: IRSS_Item
    {
        string Author { set; get; }
        string Comments { set; get; }

        string GetPubDate { get; }

        string Message { set; get; }

        string GameTitle { get; }
        string GameLink { get; }

        string Opponent {get; }

        string RatingRaw {get; }
        int Rating { get; }

        string TimeLeftRaw {get; }
        DateTime TimeLeft {get; }

        string MoveRaw {get; }
        int Move { get; }
    }
}
