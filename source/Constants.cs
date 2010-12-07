namespace ChessCalendar
{
    public static class Constants
    {
        public const string CHESS_DOT_COM = "http://www.chess.com";
        public const string CHESS_DOT_COM_RSS_ECHESS = CHESS_DOT_COM + "/rss/echess/";

        public const string CHESS_DOT_COM_PGN_PATH = CHESS_DOT_COM + "/echess/download_pgn.html?id=";
        public const string CHESS_DOT_COM_GAME_LINK = CHESS_DOT_COM + "/echess/game.html?id=";

        public const string GOOGLE_DOT_COM = "http://www.google.com";
        public const string CALENDAR_FEEDS = GOOGLE_DOT_COM + "/calendar/feeds/";
        public const string DEFAULT_FEED = CALENDAR_FEEDS + "default" + PRIVATE_FULL;
        public const string OWN_CALENDARS = CALENDAR_FEEDS + "default/owncalendars/full";

        public const string PRIVATE_FULL = "/private/full";

        public const string GRID = "TabPage_Grid";
        public const string RECORD_IN_CALENDAR = "TabPageName_chkLogToCalendar";
        public const string MESSAGES = "TabPage_txtMessages";
        public const string PTAB = "PTab_";

        public const string NEW = "ADD +";
        public const string AUTO_LOGGER = "auto-logger";
    }
}
