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

        public  Uri _calendarToPost = new Uri(Constants.DEFAULT_FEED);
        private  readonly Google.GData.Calendar.CalendarService _service = new CalendarService("ChessMoveLogService");

        //public GoogleCalendar(OutputClass output)
        //{
            
        //}

        public  CalendarFeed RetrieveCalendars(string userName, string password)
        {
            // Create a CalenderService and authenticate
            _service.setUserCredentials(userName, password);

            var query = new CalendarQuery();
            query.Uri = new Uri(Constants.OWN_CALENDARS);

            return _service.Query(query);
        }
        public void CreateEntry(string userName, string password, string title, string description, DateTime start, DateTime end, Uri calendar, out string error)
        {
            _calendarToPost = calendar;
            error = string.Empty;

            //TODO: If username is null then report an error another way.  EventLog?

            try
            {
                var entry = new EventEntry();
                entry.Title.Text = title;
                entry.Content.Content = description;
                entry.Locations.Add(new Where(string.Empty,string.Empty, Constants.AUTO_LOGGER));
                entry.Times.Add(new When(start, end)); //entry.Times.Add(new When()); //how to add an all day?

                if (!string.IsNullOrEmpty(userName))
                {
                    _service.setUserCredentials(userName, password);
                }

                (new GDataGAuthRequestFactory("", "")).CreateRequest(GDataRequestType.Insert, _calendarToPost);

                if (_calendarToPost != null)
                {
                    _service.Insert(_calendarToPost, entry);
                }
                else
                {
                    error = "No Calendar to Insert into (CreateEntry erroneously called";
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

        public  GameList GetAlreadyLoggedChessGames(string userName, string password, Uri calendar, DateTime startDate, DateTime endDate, string query, out string error)
        {
            EventQuery myQuery = new EventQuery(calendar.ToString());
            myQuery.Query = query;
            myQuery.StartDate = startDate;
            myQuery.EndDate = endDate;

            GameList queriedGames = new GameList();
            error = string.Empty;

            try
            {
                _service.setUserCredentials(userName, password);

                EventFeed myResultsFeed = _service.Query(myQuery);
                queriedGames.AddRange(myResultsFeed.Entries.Select(entry => entry));
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
