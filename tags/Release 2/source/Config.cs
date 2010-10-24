using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ChessCalendar
{
    class Config
    {
        public static IEnumerable<XElement> GetWatchList(string filePath)
        {
            return from p in XElement.Load(filePath).Elements("games")
                   select p;
        }
    }
}
