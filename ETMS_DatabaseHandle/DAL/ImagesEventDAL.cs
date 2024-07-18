using ETMS_DatabaseHandle.DTO;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class ImagesEventDAL
    {
        private EventManagerDB _db;

        public ImagesEventDAL()
        {
            _db = new EventManagerDB();
        }

        internal ImagesEventDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllImagesEvent()
        {
            string query = @"SELECT * FROM [ImagesEvent];";
            return _db.GetData(query);
        }

        public DataSet GetAllImagesEventWithName()
        {
            string query = @"
                SELECT img.*, e.EventName FROM [ImagesEvent] img
                JOIN [Events] e ON img.EventID = e.EventID;
            ";
            return _db.GetData(query);
        }

        public DataSet GetImagesEventByID(int id)
        {
            string query = "SELECT * FROM [ImagesEvent] WHERE ImageID = @1;";
            return _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = id }
            });
        }

        public DataSet GetImagesEventByEventID(int eventId)
        {
            string query = "SELECT * FROM [ImagesEvent] WHERE EventID = @1;";
            return _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = eventId }
            });
        }

        public string GetTop1ImageEventByEventID(int eventId)
        {
            string query = "SELECT TOP 1 * FROM [ImagesEvent] WHERE EventID = @1;";
            DataSet data = _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = eventId }
            });
            if (data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                return "";
            return data.Tables[0].Rows[0]["ImageUrl"].ToString();
        }

        public void InsertNewImageEvent(ImageEvent imagesEvent)
        {
            string query = @"
                INSERT INTO [ImagesEvent](ImageUrl, EventID)
                VALUES (@1, @2);
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = imagesEvent.ImageUrl },
                new Param() { Name = "@2", Value = imagesEvent.EventID }
            });
        }

        public void UpdateImageEvent(ImageEvent imagesEvent)
        {
            string query = @"
                UPDATE [ImagesEvent]
                SET [ImageUrl]=@1, [EventID]=@2
                WHERE [ImageID]=@3;
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = imagesEvent.ImageUrl },
                new Param() { Name = "@2", Value = imagesEvent.EventID },
                new Param() { Name = "@3", Value = imagesEvent.ImageID }
            });
        }

        public void DeleteImageEvent(int id)
        {
            string query = @"
                DELETE FROM [ImagesEvent]
                WHERE [ImageID]=@1;
            ";
            _db.SetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = id }
            });
        }


    }
}
