using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class AdresRepo : IAdresRepo
    {
        private string _connString;

        public AdresRepo(string connString)
        {
            _connString = connString;
        }
        public bool BestaatAdres(Adres a)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 [id] ,[straat] ,[huisnummer] ,[stad] ,[postcode] FROM [Project_Flapp_DB].[dbo].[Adres] WHERE straat = @straat AND huisnummer = @huisnummer AND stad = @stad AND postcode = @postcode;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@straat", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.VarChar));

                    cmd.Parameters.Add(new SqlParameter("@stad", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@straat"].Value = a.Straat;
                    cmd.Parameters["@huisnummer"].Value = a.Huisnummer;

                    cmd.Parameters["@stad"].Value = a.Stad;
                    cmd.Parameters["@postcode"].Value = a.Postcode;

                    int AdresBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (AdresBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void VoegAdresToe(Adres a)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([straat] ,[huisnummer] ,[stad] ,[postcode]) VALUES (@straat ,@huisnummer ,@stad ,@postcode);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@straat", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@stad", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@straat"].Value = a.Straat;
                    cmd.Parameters["@huisnummer"].Value = a.Huisnummer;
                    cmd.Parameters["@stad"].Value = a.Stad;
                    cmd.Parameters["@postcode"].Value = a.Postcode;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
    }
}
