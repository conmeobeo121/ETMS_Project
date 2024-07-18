using ETMS_DatabaseHandle.DAL;
using ETMS_DatabaseHandle.DTO;
using System;
using System.Data;
using System.Diagnostics;

namespace ETMS_DatabaseHandle.BLL
{
    public class OrdersBLL
    {
        private OrdersDAL _dal;

        public OrdersBLL()
        {
            _dal = new OrdersDAL();
        }

        internal OrdersBLL(string connectionString)
        {
            _dal = new OrdersDAL(connectionString);
        }

        public DataSet GetAllOrders()
        {
            return _dal.GetAllOrders();
        }

        public DataSet GetOrderById(int orderId)
        {
            return _dal.GetOrderById(orderId);
        }

        public void InsertNewOrder(int userId, DateTime paymentDate, long totalPrice, string status)
        {
            try
            {
                _dal.InsertNewOrder(new Order()
                {
                    UserID = userId,
                    PaymentDate = paymentDate,
                    TotalPrice = totalPrice,
                    Status = status.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public int InsertNewOrderAndReturnID(int userId, DateTime paymentDate, long totalPrice, string status)
        {
            try
            {
                return _dal.InsertNewOrderAndReturnID(new Order()
                {
                    UserID = userId,
                    PaymentDate = paymentDate,
                    TotalPrice = totalPrice,
                    Status = status.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void UpdateOrder(int userId, DateTime paymentDate, long totalPrice, string status, int orderId)
        {
            try
            {
                _dal.UpdateOrder(new Order()
                {
                    OrderID = orderId,
                    UserID = userId,
                    PaymentDate = paymentDate,
                    TotalPrice = totalPrice,
                    Status = status.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                _dal.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred from server.");
            }
        }
    }
}
