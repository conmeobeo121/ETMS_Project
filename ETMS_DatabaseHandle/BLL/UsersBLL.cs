using ETMS_DatabaseHandle.DAL;
using System;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace ETMS_DatabaseHandle.BLL
{
    public class UsersBLL
    {
        private UsersDAL _dal;

        public UsersBLL()
        {
            _dal = new UsersDAL();
        }

        internal UsersBLL(string connectionString)
        {
            _dal = new UsersDAL(connectionString);
        }

        public DataSet GetAllOfUsers()
        {
            return _dal.GetAllUsers();
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển đổi chuỗi sang UTF-16 (Unicode) bytes, tương đương với NVARCHAR trong SQL Server
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                byte[] hashBytes = sha256Hash.ComputeHash(bytes);

                // Chuyển đổi bytes thành chuỗi hex lowercase
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        public bool CheckLoginValid(string username, string password)
        {
            DataSet data = _dal.GetUserByUserName(username);
            if (!(data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0))
                return false;
            string passwordUser = (string)data.Tables[0].Rows[0]["Password"];
            string passwordHash = HashPassword(password);
            return passwordUser == passwordHash;
        }

        public string GetRoleOfUser(string username)
        {
            DataSet data = _dal.GetRoleByUserName(username);
            if (!(data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0))
                return null;
            return (string)data.Tables[0].Rows[0]["UserType"];
        }

        public DataSet GetUserByUserName(string userName)
        {
            return _dal.GetUserByUserName(userName);
        }

        public void CreateNewUser(
                string username,
                string password,
                string userType,
                string email
            )
        {
            try
            {
                _dal.InsertNewUser(new DTO.User()
                {
                    Username = username.Trim(),
                    Password = password,
                    UserType = userType.Trim(),
                    UserEmail = email.Trim()
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred in server.");
            }
        }

        public void UpdateUser(
                string username,
                string password,
                string userType,
                string email,
                int id
            )
        {
            try
            {
                _dal.UpdateUser(new DTO.User()
                {
                    Username = username.Trim(),
                    Password = password,
                    UserType = userType.Trim(),
                    UserEmail = email.Trim(),
                    UserID = id
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred in server.");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _dal.DeleteUser(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("An error occurred in server.");
            }
        }
    }
}
