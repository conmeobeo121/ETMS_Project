using ETMS_DatabaseHandle.DAL;
using ETMS_DatabaseHandle.DTO;
using System;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class TicketsBLL
    {
        private TicketsDAL _dal;

        public TicketsBLL()
        {
            _dal = new TicketsDAL();
        }

        internal TicketsBLL(string connectionString)
        {
            _dal = new TicketsDAL(connectionString);
        }

        public DataSet GetAllTickets()
        {
            return _dal.GetAllTickets();
        }

        public DataSet GetTicketById(int ticketId)
        {
            return _dal.GetTicketById(ticketId);
        }

        public void InsertNewTicket(int orderId, int typeId, string ticketCode, int? seatNumber = null)
        {
            try
            {
                _dal.InsertNewTicket(new Ticket()
                {
                    OrderID = orderId,
                    TypeID = typeId,
                    TicketCode = ticketCode,
                    SeatNumber = seatNumber
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateTicket(int orderId, int typeId, string ticketCode, int seatNumber, int ticketId)
        {
            try
            {
                _dal.UpdateTicket(new Ticket()
                {
                    TicketID = ticketId,
                    OrderID = orderId,
                    TypeID = typeId,
                    TicketCode = ticketCode,
                    SeatNumber = seatNumber
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteTicket(int ticketId)
        {
            try
            {
                _dal.DeleteTicket(ticketId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }

}
