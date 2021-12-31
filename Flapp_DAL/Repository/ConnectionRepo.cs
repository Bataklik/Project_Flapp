using Flapp_BLL.Interfaces;
using System;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class ConnectionRepo : IConnectionRepo
    {
        private string _connString;

        public ConnectionRepo(string connString)
        {
            _connString = connString;
        }
        public bool IsServerConnected()
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.CommandText = query;
                    int connBestaat = Convert.ToInt32(cmd.ExecuteScalar());
                    if (connBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
    }
}
