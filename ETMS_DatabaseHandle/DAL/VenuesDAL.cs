using ETMS_DatabaseHandle.DTO;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class VenuesDAL
    {
        private EventManagerDB _db;

        public VenuesDAL()
        {
            _db = new EventManagerDB();
        }

        internal VenuesDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllVenues()
        {
            string query = "SELECT * FROM Venues";
            return _db.GetData(query);
        }

        public DataSet GetVenueByID(int id)
        {
            string query = "SELECT * FROM Venues WHERE VenueID = @1;";
            return _db.GetData(query, new Param[]
                {
                    new Param() {Name = "@1", Value = id }
                });
        }

        public DataSet FilterByName(string searchString)
        {
            string query = @"
                SELECT * FROM Venues
                WHERE VenueName COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @1;
            ";
            return _db.GetData(query, new Param[]
                {
                    new Param() { Name = "@1", Value = "%" + searchString + "%" }
                });
        }

        public DataSet GetVenueByName(string name)
        {
            string query = "SELECT * FROM Venues WHERE VenueName LIKE N'%@1%'";
            return _db.GetData(query, new Param[]
                {
                    new Param() {Name = "@1", Value = name.Trim() }
                }
                );
        }

        public int GetVenueCapacity(int idVenue)
        {
            string query = @"
                SELECT [VenueCapacity] FROM [Venues]
                WHERE [VenueID] = @1;
            ";
            DataSet data = _db.GetData(query, new Param[]
            {
                new Param() { Name = "@1", Value = idVenue }
            });
            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0][0] != null)
            {
                return (int)data.Tables[0].Rows[0][0];
            }
            return -1;
        }

        public void InsertNewVenue(Venue venue)
        {
            string query = "INSERT INTO [Venues]([VenueName], [VenueAddress], [VenueCapacity], [VenueCity], [VenueState], [VenueZipCode]) " +
                "VALUES (@1, @2, @3, @4, @5, @6)";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=venue.VenueName },
                    new Param() {Name="@2", Value=venue.VenueAddress },
                    new Param() {Name="@3", Value=venue.VenueCapacity },
                    new Param() {Name="@4", Value=venue.VenueCity },
                    new Param() {Name="@5", Value=venue.VenueState },
                    new Param() {Name="@6", Value=venue.VenueZipCode },
                });
        }

        public void UpdateVenue(Venue venue)
        {
            string query = "UPDATE [Venues] SET [VenueName] = @1, [VenueAddress] = @2, [VenueCapacity] = @3, [VenueCity] = @4, [VenueState] = @5, [VenueZipCode] = @6 WHERE [VenueID] = @7";
            _db.SetData(query,
                new Param[]
                {
                    new Param() { Name = "@1", Value = venue.VenueName },
                    new Param() { Name = "@2", Value = venue.VenueAddress },
                    new Param() { Name = "@3", Value = venue.VenueCapacity },
                    new Param() { Name = "@4", Value = venue.VenueCity },
                    new Param() { Name = "@5", Value = venue.VenueState },
                    new Param() { Name = "@6", Value = venue.VenueZipCode },
                    new Param() { Name = "@7", Value = venue.VenueID }
                });
        }

        public void DeleteVenue(int id)
        {
            string query = "DELETE FROM [Venues] WHERE [VenueID] = @1";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=id }
                });
        }
    }
}
