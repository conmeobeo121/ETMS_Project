using ETMS_DatabaseHandle.DAL;
using System;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class EventTypesBLL
    {
        private EventTypesDAL _dal;

        public EventTypesBLL()
        {
            _dal = new EventTypesDAL();
        }

        internal EventTypesBLL(string connectionString)
        {
            _dal = new EventTypesDAL(connectionString);
        }

        public DataSet GetAllEventTypes()
        {
            return _dal.GetAllEventTypes();
        }

        public DataSet FilterByName(string searchString)
        {
            return _dal.FilterByName(searchString);
        }

        public void InsertNewEventType(string eventName)
        {
            try
            {
                _dal.InsertNewEventType(new DTO.EventType()
                {
                    TypeName = eventName.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateEventType(string eventName, int id)
        {
            try
            {
                _dal.UpdateEventType(new DTO.EventType()
                {
                    TypeId = id,
                    TypeName = eventName.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteEventType(int id)
        {
            try
            {
                _dal.DeleteEventType(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }
}
