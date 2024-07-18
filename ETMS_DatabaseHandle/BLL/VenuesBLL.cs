using ETMS_DatabaseHandle.DAL;
using System;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class VenuesBLL
    {
        private VenuesDAL _dal;

        public VenuesBLL()
        {
            _dal = new VenuesDAL();
        }

        internal VenuesBLL(string connectionString)
        {
            _dal = new VenuesDAL(connectionString);
        }

        public DataSet GetAllVenues()
        {
            return _dal.GetAllVenues();
        }

        public DataSet GetVenueByID(int id)
        {
            return _dal.GetVenueByID(id);
        }

        public DataSet GetVenueByName(string name)
        {
            return _dal.GetVenueByName(name);
        }

        public DataSet FilterByName(string searchString)
        {
            return _dal.FilterByName(searchString);
        }

        public int GetVenueCapacity(int idVenue)
        {
            return _dal.GetVenueCapacity(idVenue);
        }

        public void InsertNewVenue(string name, string address, int capacity, string city, string state, string zipcode)
        {
            try
            {
                if (capacity < 0)
                {
                    throw new Exception("Capacity must be greater than zero.");
                }
                _dal.InsertNewVenue(new DTO.Venue()
                {
                    VenueName = name.Trim(),
                    VenueAddress = address.Trim(),
                    VenueCapacity = capacity,
                    VenueCity = city.Trim(),
                    VenueState = state.Trim(),
                    VenueZipCode = zipcode.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateVenue(string name, string address, int capacity, string city, string state, string zipcode, int id)
        {
            try
            {
                if (capacity < 0)
                {
                    throw new Exception("Capacity must be greater than zero.");
                }
                _dal.UpdateVenue(new DTO.Venue()
                {
                    VenueID = id,
                    VenueName = name.Trim(),
                    VenueAddress = address.Trim(),
                    VenueCapacity = capacity,
                    VenueCity = city.Trim(),
                    VenueState = state.Trim(),
                    VenueZipCode = zipcode.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteVenue(int id)
        {
            try
            {
                _dal.DeleteVenue(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }
}
