namespace ChessCalendar.Interfaces
{
    /// <summary>
    /// A subset of the RSSItem
    /// </summary>
    public interface IRSS_Item
    {
        string Description { set; get; }
        string Link { set; get; }
        string PubDate { set; get; }
        string GameID { get; }
        string PGN { get; set; }
        bool NewMove { get; set; }
        string Title { set; get; }
    }
}
