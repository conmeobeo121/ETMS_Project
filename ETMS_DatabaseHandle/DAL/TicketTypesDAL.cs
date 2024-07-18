using ETMS_DatabaseHandle.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class TicketTypesDAL
    {
        private EventManagerDB _db;

        public TicketTypesDAL()
        {
            _db = new EventManagerDB();
        }

        internal TicketTypesDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllTicketTypes()
        {
            string query = "SELECT * FROM [TicketTypes];";
            return _db.GetData(query);
        }

        public DataSet GetAllTicketTypesWithEventName()
        {
            string query = @"
                SELECT tt.*, e.EventName FROM [TicketTypes] tt
                JOIN [Events] e ON tt.EventID = e.EventID;
            ";
            return _db.GetData(query);
        }

        public DataSet GetTicketTypesGreaterThanTimelineWithEventID(int eventID, DateTime timeline)
        {
            string query = @"
                SELECT * FROM [TicketTypes] 
                WHERE [TypeID] = @1 AND @2 < [EndSell]
            ";
            return _db.GetData(query,
                new Param[]
                {
                    new Param() { Name = "@1", Value = eventID },
                    new Param() { Name = "@2", Value = timeline },
                });
        }

        public DataSet GetTicketTypeByID(int id)
        {
            string query = "SELECT * FROM [TicketTypes] WHERE [TypeID] = @1;";
            return _db.GetData(query, new Param[]
                {
                    new Param() { Name = "@1", Value = id },
                });
        }

        public DataSet GetTicketTypeByEventID(int id)
        {
            string query = @"
                SELECT * FROM [TicketTypes]
                WHERE EventID = @1;
            ";
            return _db.GetData(query, new Param[]
                {
                    new Param() { Name = "@1", Value = id },
                });
        }

        public DataSet GetTicketTypesWithEventNameByIDs(List<int> listTypeID)
        {
            if (listTypeID.Count == 0)
            {
                listTypeID.Add(-1000000);
            }
            string listIDStr = string.Join(",", listTypeID);
            string query = $@"
                SELECT tt.*, e.EventName FROM [TicketTypes] tt
                JOIN [Events] e ON tt.EventID = e.EventID
                WHERE tt.TypeID IN ({listIDStr});
            ";
            return _db.GetData(query);
        }

        public void InsertNewTicketType(TicketType ticketType)
        {
            string query = @"
                INSERT INTO [TicketTypes] ([TypeName], [Price], [EventID], [HasSeat], [StartSell], [EndSell]) 
                VALUES (@1, @2, @3, @4, @5, @6);
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = ticketType.TypeName},
                new Param() { Name = "@2", Value = ticketType.Price },
                new Param() { Name = "@3", Value = ticketType.EventID },
                new Param() { Name = "@4", Value = ticketType.HasSeat },
                new Param() { Name = "@5", Value = ticketType.StartSell },
                new Param() { Name = "@6", Value = ticketType.EndSell },
            });
        }

        public void UpdateTicketType(TicketType ticketType)
        {
            string query = @"
                UPDATE [TicketTypes]
                SET [TypeName] = @1, [Price] = @2, [EventID] = @3, [HasSeat] = @4, [StartSell] = @5, [EndSell] = @6
                WHERE [TypeID] = @7;
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = ticketType.TypeName},
                new Param() { Name = "@2", Value = ticketType.Price },
                new Param() { Name = "@3", Value = ticketType.EventID },
                new Param() { Name = "@4", Value = ticketType.HasSeat },
                new Param() { Name = "@5", Value = ticketType.StartSell },
                new Param() { Name = "@6", Value = ticketType.EndSell },
                new Param() { Name = "@7", Value = ticketType.TypeID }
            });
        }

        public void DeleteTicketType(int id)
        {
            string query = @"
                DELETE FROM [TicketTypes]
                WHERE [TypeID] = @1;
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = id },
            });
        }
    }
}
