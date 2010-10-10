using System;
using System.Linq;

namespace ChessCalendar
{
    class ChessDotComGame
    {
        public string Comments { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public string Title { get; set; }

        public string Rating
        {
            get
            {
                return Description.Split("<br/>".ToArray(), StringSplitOptions.None)[0];
            }
        }       
    }
}
