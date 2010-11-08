using System.Collections.Generic;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class MessageQueue: Queue<IChessItem>
    {
        private bool _updated;
        public bool Updated
        {
            get
            {
                var retVal = _updated;
                _updated = false;

                return retVal;
            } 
            set
            {
                _updated = value;
            }
        }
    }
}
