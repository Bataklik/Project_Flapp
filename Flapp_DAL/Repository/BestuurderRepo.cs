using Flapp_BLL.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class BestuurderRepo
    {
        private string _connString;

        public BestuurderRepo(string connString)
        {
            _connString = connString;
        }

        #region VoegBestuurderToe Method
        public void VoegBestuurderToe(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Voetbaltruitjes_App]; INSERT INTO [dbo].[Klant] ([klantNaam] ,[klantAdres]) VALUES (@klantnaam ,@klantadres)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@klantnaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@klantadres", SqlDbType.VarChar));

                    cmd.CommandText = query;

                    cmd.Parameters["@klantnaam"].Value = b.Naam;
                    cmd.Parameters["@klantadres"].Value = b.Adres;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        #endregion
    }
}
