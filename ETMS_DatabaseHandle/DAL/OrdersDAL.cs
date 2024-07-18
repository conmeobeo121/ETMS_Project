using ETMS_DatabaseHandle.DTO;
using System;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class OrdersDAL
    {
        private EventManagerDB _db;

        public OrdersDAL()
        {
            _db = new EventManagerDB();
        }

        internal OrdersDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllOrders()
        {
            string query = "SELECT * FROM Orders";
            return _db.GetData(query);
        }

        public DataSet GetOrderById(int orderId)
        {
            string query = "SELECT * FROM Orders WHERE OrderID = @1";
            return _db.GetData(query, new Param[]
                {
                new Param() { Name = "@1", Value = orderId }
                });
        }

        public void InsertNewOrder(Order order)
        {
            string query = "INSERT INTO Orders (UserID, PaymentDate, TotalPrice, Status) VALUES (@1, @2, @3, @4)";
            _db.SetData(query,
                new Param[]
                {
                    new Param() { Name = "@1", Value = order.UserID },
                    new Param() { Name = "@2", Value = order.PaymentDate },
                    new Param() { Name = "@3", Value = order.TotalPrice },
                    new Param() { Name = "@4", Value = order.Status }
                });
        }

        public int InsertNewOrderAndReturnID(Order order)
        {
            string query = "INSERT INTO Orders (UserID, PaymentDate, TotalPrice, Status) VALUES (@1, @2, @3, @4); SELECT SCOPE_IDENTITY();";
            int newID = Convert.ToInt32(_db.SetDataReturnValue(query,
                new Param[]
            {
                new Param() { Name = "@1", Value = order.UserID },
                new Param() { Name = "@2", Value = order.PaymentDate },
                new Param() { Name = "@3", Value = order.TotalPrice },
                new Param() { Name = "@4", Value = order.Status }
            }));
            return newID;
        }

        public void UpdateOrder(Order order)
        {
            string query = "UPDATE Orders SET UserID = @1, PaymentDate = @2, TotalPrice = @3, Status = @4 WHERE OrderID = @5";
            _db.SetData(query,
                new Param[]
                {
                new Param() { Name = "@1", Value = order.UserID },
                new Param() { Name = "@2", Value = order.PaymentDate },
                new Param() { Name = "@3", Value = order.TotalPrice },
                new Param() { Name = "@4", Value = order.Status },
                new Param() { Name = "@5", Value = order.OrderID }
                });
        }

        public void DeleteOrder(int orderId)
        {
            string query = "DELETE FROM Orders WHERE OrderID = @1";
            _db.SetData(query,
                new Param[]
                {
                new Param() { Name = "@1", Value = orderId }
                });
        }
    }

}
