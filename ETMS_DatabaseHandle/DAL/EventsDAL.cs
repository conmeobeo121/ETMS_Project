using ETMS_DatabaseHandle.DTO;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class EventsDAL
    {
        private EventManagerDB _db;

        public EventsDAL()
        {
            _db = new EventManagerDB();
        }

        internal EventsDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllEvents()
        {
            string query = @"SELECT * FROM [Events];";
            return _db.GetData(query);
        }

        public DataSet GetAllEventsWithTypeAndVenueName()
        {
            string query = @"
                SELECT e.EventID, e.EventName, e.EventDescription, e.EventStartDate, e.EventEndDate, v.VenueID, v.VenueName, tp.TypeID, tp.TypeName
                FROM Events e
                JOIN EventTypes tp ON e.TypeID = tp.TypeID
                JOIN Venues v ON e.VenueID = v.VenueID;                
            ";
            return _db.GetData(query);
        }

        public DataSet GetEventByID(int id)
        {
            string query = "SELECT * FROM [Events] WHERE EventID = @1;";
            return _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = id }
            });
        }


        public DataSet GetEventByIDWithTypeAndVenueName(int id)
        {
            string query = @"SELECT e.EventID, e.EventName, e.EventDescription, e.EventStartDate, e.EventEndDate, v.VenueID, v.VenueName, tp.TypeID, tp.TypeName
                            FROM Events e
                            JOIN EventTypes tp ON e.TypeID = tp.TypeID
                            JOIN Venues v ON e.VenueID = v.VenueID
                            WHERE EventID = @1;
            ";
            return _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = id }
            });
        }

        public DataSet FilterByName(string searchString)
        {
            string query = @"
                SELECT * FROM Events
                WHERE EventName COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @1;
            ";
            return _db.GetData(query, new Param[]
                {
                    new Param() { Name = "@1", Value = "%" + searchString + "%" }
                });
        }

        public DataSet GetTop3OngoingEvents()
        {
            string query = @"
               SELECT TOP 3 * FROM [Events]
                WHERE [EventStartDate] <= GETDATE() AND GETDATE() <= [EventEndDate];
            ";
            return _db.GetData(query);
        }

        public DataSet GetOngoingEvents()
        {
            string query = @"
               SELECT * FROM [Events]
                WHERE [EventStartDate] <= GETDATE() AND GETDATE() <= [EventEndDate];
            ";
            return _db.GetData(query);
        }

        public DataSet GetTop3UpcomingEvents()
        {
            string query = @"
               SELECT TOP 3 * FROM [Events]
                WHERE GETDATE() < [EventStartDate]
                ORDER BY DATEDIFF(DAY, GETDATE(), [EventStartDate]) ASC;
            ";
            return _db.GetData(query);
        }

        public DataSet GetUpcomingEvents()
        {
            string query = @"
               SELECT * FROM [Events]
                WHERE GETDATE() < [EventStartDate]
                ORDER BY DATEDIFF(DAY, GETDATE(), [EventStartDate]) ASC;
            ";
            return _db.GetData(query);
        }

        public DataSet GetTop10RelativeEvents(int eventID)
        {
            string query = @"
               SELECT TOP 10 * FROM [Events]
                WHERE [EventID] <> @1 AND GETDATE() < [EventEndDate] AND [TypeID] = (
                    SELECT [TypeID] FROM [Events]
                    WHERE [EventID] = @1
                )
                ORDER BY DATEDIFF(DAY, GETDATE(), [EventStartDate]) ASC;
            ";
            return _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = eventID }
            });
        }

        public DataSet GetAvailableEvents()
        {
            string query = @"
               SELECT * FROM [Events]
                WHERE GETDATE() <= [EventEndDate];
            ";
            return _db.GetData(query);
        }

        public DataSet GetAvailableEventsByType(int typeID)
        {
            string query = @"
                SELECT * FROM [Events]
                WHERE GETDATE() <= [EventEndDate] AND [TypeID] = @1;
            ";
            return _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = typeID }
            });
        }

        public DataSet GetEventTypeCounts()
        {
            string query = @"
                SELECT et.TypeName, COUNT(e.EventID) AS EventCount
                FROM EventTypes et
                LEFT JOIN Events e ON e.TypeID = et.TypeID
                GROUP BY et.TypeName;
            ";
            return _db.GetData(query);
        }

        public int GetTotalOrdersEvent(int eventID)
        {
            string query = @"
                SELECT COUNT(t.TicketID) as CountOrders
                FROM [Tickets] t
                JOIN [Orders] o ON t.OrderID = o.OrderID
                JOIN [TicketTypes] tt ON t.TypeID = tt.TypeID
                JOIN [Events] e ON tt.EventID = e.EventID
                WHERE e.EventID = @1 AND o.Status = 'PAID';
            ";
            DataSet data = _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = eventID }
            });
            return (int)data.Tables[0].Rows[0][0];
        }

        public void InsertNewEvent(Event @event)
        {
            string query = @"
                INSERT INTO [Events](EventName, EventDescription, EventStartDate, EventEndDate, VenueID, TypeID)
                VALUES (@1, @2, @3, @4, @5, @6);
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = @event.EventName },
                new Param() { Name = "@2", Value = @event.EventDescription },
                new Param() { Name = "@3", Value = @event.EventStartDate },
                new Param() { Name = "@4", Value = @event.EventEndDate },
                new Param() { Name = "@5", Value = @event.VenueID },
                new Param() { Name = "@6", Value = @event.TypeID },
            });
        }

        public void UpdateEvent(Event @event)
        {
            string query = @"
                UPDATE [Events]
                SET [EventName]=@1, [EventDescription]=@2, [EventStartDate]=@3, [EventEndDate]=@4, [VenueID]=@5, [TypeID]=@6
                WHERE [EventID]=@7;
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = @event.EventName },
                new Param() { Name = "@2", Value = @event.EventDescription },
                new Param() { Name = "@3", Value = @event.EventStartDate },
                new Param() { Name = "@4", Value = @event.EventEndDate },
                new Param() { Name = "@5", Value = @event.VenueID },
                new Param() { Name = "@6", Value = @event.TypeID },
                new Param() { Name = "@7", Value = @event.EventID }
            });
        }

        public void DeleteEvent(int id)
        {
            string query = @"
                DELETE FROM [Events]
                WHERE [EventID]=@1;
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = id }
            });
        }


    }
}
