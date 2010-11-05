﻿using System;

namespace ChessCalendar.Interfaces
{
    /// <summary>
    /// A subset of the RSSItem
    /// </summary>
    public interface IChessItem
    {
        string Author { set; get; }
        string Comments { set; get; }
        string Description { set; get; }
        string Link { set; get; }
        string PubDate { set; get; }
        string Title { set; get; }

        string Message { set; get; }
        string GameID { get;}
        string PGN { get; set; }
        bool NewMove { get; set; }

        string Rating {get; }
        string TimeLeftRaw {get; }
        DateTime TimeLeft {get; }
        string Move {get; }
    }
}
