using System;
using System.Linq;
using ChessCalendar.Interfaces;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace ChessCalendar
{
    //TODO: this class needs to be instantiated
    public class GoogleCalendar:IError
    {
        //#region Properties

        //public OutputClass Output { get; set; }
        //#endregion

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public string OperationProgress { get; set; }

        public  Uri _calendarToPost = new Uri(Constants.DEFAULT_FEED);
        private  readonly Google.GData.Calendar.CalendarService _service = new CalendarService("ChessMoveLogService");

        public GoogleCalendar()
        {
      
            

        }

        //public GoogleCalendar(OutputClass output)
        //{

        //}

        #region Events


        #endregion

        public  CalendarFeed RetrieveCalendars(string userName, string password)
        {
            // Create a CalenderService and authenticate
            _service.setUserCredentials(userName, password);

            var query = new CalendarQuery();
            query.Uri = new Uri(Constants.OWN_CALENDARS);
            

            return _service.Query(query);
        }
        public void CreateEntry(CalendarInfo calendarInfo, string title, string description, DateTime start, DateTime end, out string error)
        {
            _calendarToPost = calendarInfo.Uri;
            error = string.Empty;

            //TODO: If username is null then report an error another way.  EventLog?

            try
            {
                var entry = new EventEntry();
                entry.Title.Text = title;
                //entry.Authors.Add(new AtomPerson(AtomPersonType.Author, Constants.AUTO_LOGGER));

                entry.Content.Content = description;
                entry.Locations.Add(new Where(string.Empty,string.Empty, Constants.AUTO_LOGGER));
                entry.Times.Add(new When(start, end)); //entry.Times.Add(new When()); //how to add an all day?

                if (!string.IsNullOrEmpty(calendarInfo.UserName))
                {
                    _service.setUserCredentials(calendarInfo.UserName, calendarInfo.Password);
                }

                (new GDataGAuthRequestFactory("", "")).CreateRequest(GDataRequestType.Insert, _calendarToPost);

                if (_calendarToPost != null)
                {
                    _service.Insert(_calendarToPost, entry);
                }
                else
                {
                    error = "No Calendar to Insert into (CreateEntry Erroneously called";
                }

                //this.Log.Output(string.Empty, "Event Successfully Added", OutputMode.Form);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                this.Error = error;
            }
        }

        public  GameList GetAlreadyLoggedChessGames(CalendarInfo calendarInfo, DateTime startDate, DateTime endDate, string query, out string error)
        {
            EventQuery chessGamesQuery = new EventQuery(calendarInfo.Uri.ToString());
            
            chessGamesQuery.NumberToRetrieve = 100;
            chessGamesQuery.Query = query;
            chessGamesQuery.StartDate = startDate;
            chessGamesQuery.EndDate = endDate;

            //chessGamesQuery.ExtraParameters = "distinct";
            
            //chessGamesQuery.Author = Constants.AUTO_LOGGER; //entry.Authors.Add(new AtomPerson(AtomPersonType.Author, Constants.AUTO_LOGGER));

            GameList queriedGames = new GameList();
            error = string.Empty;

            try
            {
                _service.setUserCredentials(calendarInfo.UserName, calendarInfo.Password);

                chessGamesQuery.StartIndex = 0;
                EventFeed myResultsFeed = _service.Query(chessGamesQuery);

                queriedGames.AddRange(myResultsFeed.Entries);
                
                //Is this part even needed? Because NextChunk never has any entries.
                //while (myResultsFeed.NextChunk != null)
                //{
                //    chessGamesQuery.StartIndex += chessGamesQuery.NumberToRetrieve;
                //    chessGamesQuery.Query = myResultsFeed.NextChunk;
                //    myResultsFeed = _service.Query(chessGamesQuery);

                //    queriedGames.AddRange(myResultsFeed.Entries);
                //}
            }
            catch (Exception ex)
            {
                queriedGames = new GameList();
                error = ex.Message;
            }
            finally
            {
                this.Error = error;
            }

            return queriedGames;
        }

    }
}
