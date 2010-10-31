using System.Collections.Generic;

namespace ChessCalendar
{
    public class PGN
    {
        string Event { set; get; }
        string Site { set; get; }
        string Date { set; get; }
        string White { set; get; }
        string Black { set; get; }
        string Result { set; get; }
        string WhiteELO { set; get; }
        string BlackELO { set; get; }
        string TimeControl { set; get; }
        string Variant { set; get; }
        string SetUp { set; get; }
        string FEN { set; get; }

        string RawMoves { set; get; }

        //Queue<string> Moves
        //GetCurrentMove
        //GetLastMove
        //GetMoveNumber(int)
    }
}
