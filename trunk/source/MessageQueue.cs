using System.Collections.Generic;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class MessageQueue: Queue<IChessItem>
    {
        public bool Updated { get; set; }
    }
}
