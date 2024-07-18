using ETMS_DatabaseHandle.DAL;
using ETMS_DatabaseHandle.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class TicketTypesBLL
    {
        private TicketTypesDAL _dal;

        public TicketTypesBLL()
        {
            _dal = new TicketTypesDAL();
        }

        public TicketTypesBLL(string connectionString)
        {
            _dal = new TicketTypesDAL(connectionString);
        }

        public DataSet GetAllTicketTypes()
        {

            return _dal.GetAllTicketTypes();

        }

        public DataSet GetAllTicketTypesWithEventName()
        {
            return _dal.GetAllTicketTypesWithEventName();
        }

        public DataSet GetTicketTypeByID(int id)
        {
            return _dal.GetTicketTypeByID(id);
        }

        public DataSet GetTicketTypeByEventID(int id)
        {
            return _dal.GetTicketTypeByEventID(id);
        }

        public DataSet GetTicketTypesGreaterThanTimelineWithEventID(int eventID, DateTime timeline)
        {
            return _dal.GetTicketTypesGreaterThanTimelineWithEventID(eventID, timeline);
        }

        public DataSet GetTicketTypesWithEventNameByIDs(List<int> listTypeID)
        {
            return _dal.GetTicketTypesWithEventNameByIDs(listTypeID);
        }

        public void InsertNewTicketType(string typeName, int price, int eventID, bool hasSeat, DateTime startSell, DateTime endSell)
        {
            try
            {
                _dal.InsertNewTicketType(new TicketType()
                {
                    TypeName = typeName,
                    Price = price,
                    EventID = eventID,
                    HasSeat = hasSeat,
                    StartSell = startSell,
                    EndSell = endSell
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateTicketType(string typeName, int price, int eventID, bool hasSeat, DateTime startSell, DateTime endSell, int typeID)
        {
            try
            {
                _dal.UpdateTicketType(new TicketType()
                {
                    TypeID = typeID,
                    TypeName = typeName,
                    Price = price,
                    EventID = eventID,
                    HasSeat = hasSeat,
                    StartSell = startSell,
                    EndSell = endSell
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteTicketType(int id)
        {
            try
            {
                _dal.DeleteTicketType(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }

}
