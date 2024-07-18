using ETMS_DatabaseHandle.DTO;
using System;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class TicketsDAL
    {
        private EventManagerDB _db;

        public TicketsDAL()
        {
            _db = new EventManagerDB();
        }

        internal TicketsDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllTickets()
        {
            string query = "SELECT * FROM Tickets";
            return _db.GetData(query);
        }

        public DataSet GetTicketById(int ticketId)
        {
            string query = "SELECT * FROM Tickets WHERE TicketID = @1";
            return _db.GetData(query, new Param[]
                {
                new Param() { Name = "@1", Value = ticketId }
                });
        }

        public void InsertNewTicket(Ticket ticket)
        {
            string query = "INSERT INTO Tickets (OrderID, TypeID, TicketCode, SeatNumber) VALUES (@1, @2, @3, @4);";
            _db.SetData(query, new Param[]
                {
                    new Param() { Name = "@1", Value = ticket.OrderID },
                    new Param() { Name = "@2", Value = ticket.TypeID },
                    new Param() { Name = "@3", Value = ticket.TicketCode },
                    new Param() { Name = "@4", Value = (object)ticket.SeatNumber ?? DBNull.Value }
                });
        }

        public void UpdateTicket(Ticket ticket)
        {
            string query = "UPDATE Tickets SET OrderID = @1, TypeID = @2, TicketCode = @3, SeatNumber = @4 WHERE TicketID = @5";
            _db.SetData(query, new Param[]
                {
                new Param() { Name = "@1", Value = ticket.OrderID },
                new Param() { Name = "@2", Value = ticket.TypeID },
                new Param() { Name = "@3", Value = ticket.TicketCode },
                new Param() { Name = "@4", Value = (object)ticket.SeatNumber ?? DBNull.Value },
                new Param() { Name = "@5", Value = ticket.TicketID }
                });
        }

        public void DeleteTicket(int ticketId)
        {
            string query = "DELETE FROM Tickets WHERE TicketID = @1";
            _db.SetData(query, new Param[]
                {
                new Param() { Name = "@1", Value = ticketId }
                });
        }
    }

}
