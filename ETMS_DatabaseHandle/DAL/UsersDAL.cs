using ETMS_DatabaseHandle.DTO;
using System.Data;

namespace ETMS_DatabaseHandle.DAL
{
    internal class UsersDAL
    {
        private EventManagerDB _db;

        public UsersDAL()
        {
            _db = new EventManagerDB();
        }

        internal UsersDAL(string connectionString)
        {
            _db = new EventManagerDB(connectionString);
        }

        public DataSet GetAllUsers()
        {
            string query = "SELECT * FROM Users";
            return _db.GetData(query);
        }

        public DataSet GetUserByUserName(string userName)
        {
            string query = "SELECT * FROM Users WHERE Username=@1";
            return _db.GetData(query,
                new Param[]
                {
                    new Param() {Name="@1", Value=userName }
                });
        }

        public DataSet GetRoleByUserName(string userName)
        {
            string query = "SELECT [UserType] FROM Users WHERE Username=@1";
            return _db.GetData(query,
                new Param[]
                {
                    new Param() {Name="@1", Value=userName }
                });
        }

        public void InsertNewUser(User user)
        {
            string query = @"INSERT INTO [Users]([Username], [Email], [Password], [UserType]) 
                VALUES (@1, @2, @3, @4)";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=user.Username },
                    new Param() {Name="@2", Value=user.UserEmail },
                    new Param() {Name="@3", Value=user.Password },
                    new Param() {Name="@4", Value=user.UserType },
                });
        }

        public void UpdateUser(User user)
        {
            string query = @"UPDATE [Users]
                            SET [Username] = @1, 
                                [Email] = @2, 
                                [Password] = @3, 
                                [UserType] = @4
                            WHERE [UserID] = @5
                            ";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=user.Username },
                    new Param() {Name="@2", Value=user.UserEmail },
                    new Param() {Name="@3", Value=user.Password },
                    new Param() {Name="@4", Value=user.UserType },
                    new Param() {Name="@5", Value=user.UserID },
                });
        }

        public void DeleteUser(int user_id)
        {
            string query = @"DELETE FROM [Users]
                            WHERE [UserID] = @1
                            ";
            _db.SetData(query,
              new Param[]
                {
                    new Param() {Name="@1", Value=user_id },
                });
        }
    }
}
