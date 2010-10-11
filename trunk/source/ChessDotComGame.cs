using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessCalendar
{
    public class ChessDotComGame: IComparable
    {
        public string Comments { get; set; }
        public string Description { get; set; }
        public RssToolkit.Rss.RssGuid Guid { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public string Title { get; set; }
        public bool StillPosted { get; set; }

        public string Rating
        {
            get
            {
                return Description.Split("<br/>".ToArray(), StringSplitOptions.None)[0];
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
