using System.Data;
using System.Data.SqlClient;

namespace ETMS_DatabaseHandle
{
    internal class EventManagerDB
    {
        private string _conStr;

        public EventManagerDB(string conStr = null)
        {
            if (conStr == null)
                conStr = Config.ConnectionString;
            _conStr = conStr;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_conStr);
        }

        private SqlCommand BuildSqlCommand(string query, Param[] @params)
        {
            SqlCommand cmd = new SqlCommand(query);
            if (@params != null)
            {
                foreach (var param in @params)
                {
                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
            }
            return cmd;
        }

        public DataSet GetData(string query, Param[] @params = null)
        {
            SqlCommand cmd = BuildSqlCommand(query, @params);
            DataSet ds = new DataSet();
            cmd.Connection = GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public int SetData(string query, Param[] @params = null)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = BuildSqlCommand(query, @params);
            cmd.Connection = conn;
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        public object SetDataReturnValue(string query, Param[] @params = null)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = BuildSqlCommand(query, @params);
            cmd.Connection = conn;
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }
    }
}
