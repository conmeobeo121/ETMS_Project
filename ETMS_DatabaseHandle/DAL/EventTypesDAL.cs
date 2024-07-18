using ETMS_DatabaseHandle.DTO;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class EventTypesDAL
    {
        private EventManagerDB _db;

        public EventTypesDAL()
        {
            _db = new EventManagerDB();
        }

        internal EventTypesDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }


        public DataSet GetAllEventTypes()
        {
            string query = "SELECT * FROM EventTypes";
            return _db.GetData(query);
        }

        public DataSet FilterByName(string searchString)
        {
            string query = @"
                SELECT * FROM [EventTypes]
                WHERE TypeName COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @1;
            ";
            return _db.GetData(query, new Param[]
                {
                    new Param() { Name = "@1", Value = "%" + searchString + "%" }
                });
        }

        public void InsertNewEventType(EventType @event)
        {
            string query = "INSERT INTO [EventTypes]([TypeName]) " +
                "VALUES (@1)";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=@event.TypeName }
                });
        }

        public void UpdateEventType(EventType @event)
        {
            string query = "UPDATE [EventTypes] SET [TypeName] = @1 WHERE TypeID=@2";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=@event.TypeName },
                    new Param() {Name="@2", Value=@event.TypeId }
                });
        }

        public void DeleteEventType(int idEvent)
        {
            string query = "DELETE FROM [EventTypes] WHERE [TypeID] = @1";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=idEvent }
                });
        }
    }
}
