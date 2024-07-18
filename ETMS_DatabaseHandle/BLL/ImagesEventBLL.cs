using ETMS_DatabaseHandle.DAL;
using ETMS_DatabaseHandle.DTO;
using System;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class ImagesEventBLL
    {
        private ImagesEventDAL _dal;

        public ImagesEventBLL()
        {
            _dal = new ImagesEventDAL();
        }

        internal ImagesEventBLL(string connectionString)
        {
            _dal = new ImagesEventDAL(connectionString);
        }

        public DataSet GetAllImagesEvent()
        {
            return _dal.GetAllImagesEvent();
        }

        public DataSet GetAllImagesEventWithName()
        {
            return _dal.GetAllImagesEventWithName();
        }

        public DataSet GetImagesEventByID(int id)
        {
            return _dal.GetImagesEventByID(id);
        }

        public DataSet GetImagesEventByEventID(int eventId)
        {
            return _dal.GetImagesEventByEventID(eventId);
        }

        public string GetTop1ImageEventByEventID(int eventID)
        {
            return _dal.GetTop1ImageEventByEventID(eventID);
        }

        public void InsertNewImageEvent(string imageUrl, int eventId)
        {
            try
            {
                _dal.InsertNewImageEvent(new ImageEvent()
                {
                    ImageUrl = imageUrl,
                    EventID = eventId
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateImageEvent(string imageUrl, int eventId, int imgID)
        {
            try
            {
                _dal.UpdateImageEvent(new ImageEvent()
                {
                    ImageID = imgID,
                    ImageUrl = imageUrl,
                    EventID = eventId
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteImageEvent(int imageId)
        {
            try
            {
                _dal.DeleteImageEvent(imageId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }
}
