﻿using System;
using System.Linq;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace ChessCalendar
{
    //TODO: this class needs to be instantiated
    public  class GoogleCalendar
    {
        //#region Properties

        //public OutputClass Output { get; set; }
        //#endregion


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
        public  void CreateEntry(string userName, string password, string title, string description, DateTime start, DateTime end, Uri calendar)
        {
            _calendarToPost = calendar;

            //TODO: If username is null then report an error another way.  EventLog?

            try
            {
                var entry = new EventEntry();
                entry.Title.Text = title;
                entry.Content.Content = description;
                entry.Locations.Add(new Where("","", "auto-logger"));
                entry.Times.Add(new When(start, end)); //entry.Times.Add(new When()); //how to add an all day?

                if (!string.IsNullOrEmpty(userName))
                {
                    _service.setUserCredentials(userName, password);
                }

                (new GDataGAuthRequestFactory("", "")).CreateRequest(GDataRequestType.Insert, _calendarToPost);
                _service.Insert(_calendarToPost, entry);

                //this.Log.Output(string.Empty, "Event Successfully Added", OutputMode.Form);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  GameList GetAlreadyLoggedChessGames(string userName, string password, Uri calendar, DateTime startDate, DateTime endDate, string query, out string error)
        {
            EventQuery myQuery = new EventQuery(calendar.ToString());
            myQuery.Query = query;
            myQuery.StartDate = startDate;
            myQuery.EndDate = endDate;

            GameList queriedGames = new GameList();
            error = "none";

            try
            {
                _service.setUserCredentials(userName, password);

                EventFeed myResultsFeed = _service.Query(myQuery);
                queriedGames.AddRange(myResultsFeed.Entries.Select(entry => entry));
            }
            catch (Exception ex)
            {
                queriedGames = null;
                error = ex.Message;
            }

            return queriedGames;
        }
    }
}
