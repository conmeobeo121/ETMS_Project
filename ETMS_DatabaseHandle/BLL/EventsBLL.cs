using ETMS_DatabaseHandle.DAL;
using System;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class EventsBLL
    {
        private EventsDAL _dal;

        public EventsBLL()
        {
            _dal = new EventsDAL();
        }

        internal EventsBLL(string connectionString)
        {
            _dal = new EventsDAL(connectionString);
        }

        public DataSet GetAllEvents()
        {
            return _dal.GetAllEvents();
        }

        public DataSet GetAllEventsWithTypeAndVenueName()
        {
            return _dal.GetAllEventsWithTypeAndVenueName();
        }

        public DataSet GetEventByID(int id)
        {
            return _dal.GetEventByID(id);
        }

        public DataSet GetEventByIDWithTypeAndVenueName(int id)
        {
            return _dal.GetEventByIDWithTypeAndVenueName(id);
        }

        public DataSet FilterByName(string searchString)
        {
            return _dal.FilterByName(searchString);
        }

        public DataSet GetTop3OngoingEvents()
        {
            return _dal.GetTop3OngoingEvents();
        }

        public DataSet GetOngoingEvents()
        {
            return _dal.GetOngoingEvents();
        }

        public DataSet GetTop3UpcomingEvents()
        {
            return _dal.GetTop3UpcomingEvents();
        }

        public DataSet GetUpcomingEvents()
        {
            return _dal.GetUpcomingEvents();
        }

        public DataSet GetTop10RelativeEvents(int eventID)
        {
            return _dal.GetTop10RelativeEvents(eventID);
        }

        public DataSet GetAvailableEvents()
        {
            return _dal.GetAvailableEvents();
        }

        public DataSet GetAvailableEventsByType(int typeID)
        {
            return _dal.GetAvailableEventsByType(typeID);
        }

        public DataSet GetEventTypeCounts()
        {
            return _dal.GetEventTypeCounts();
        }

        public int GetTotalOrdersEvent(int eventID)
        {
            return _dal.GetTotalOrdersEvent(eventID);
        }

        public int GetRemainingSeats(int eventID)
        {
            DataSet data = GetEventByID(eventID);
            //int venueCapacity = 
            VenuesBLL vBLL = new VenuesBLL();
            int venueCapacity = vBLL.GetVenueCapacity((int)data.Tables[0].Rows[0]["VenueID"]);
            int totalOrders = GetTotalOrdersEvent(eventID);
            return venueCapacity - totalOrders;
        }

        public void InsertNewEvent(string eventName,
                                    string eventDescription,
                                    DateTime startDate,
                                    DateTime endDate,
                                    int venueID,
                                    int typeID)
        {
            try
            {
                _dal.InsertNewEvent(new DTO.Event()
                {
                    EventName = eventName,
                    EventDescription = eventDescription,
                    EventStartDate = startDate,
                    EventEndDate = endDate,
                    VenueID = venueID,
                    TypeID = typeID
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateEvent(string eventName,
                                string eventDescription,
                                DateTime startDate,
                                DateTime endDate,
                                int venueID,
                                int typeID,
                                int eventID)
        {
            try
            {
                _dal.UpdateEvent(new DTO.Event()
                {
                    EventID = eventID,
                    EventName = eventName,
                    EventDescription = eventDescription,
                    EventStartDate = startDate,
                    EventEndDate = endDate,
                    VenueID = venueID,
                    TypeID = typeID
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteEvent(int idEvent)
        {
            try
            {
                _dal.DeleteEvent(idEvent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }
}
