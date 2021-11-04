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

        #region BestaatAdres Method
        public bool BestaatAdres(Adres a)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 [id] ,[adres_straat] ,[adres_huisnummer] ,[adres_stad] ,[adres_postcode] FROM [Project_Flapp_DB].[dbo].[Adres] WHERE adres_straat = @straat AND adres_huisnummer = @huisnummer AND adres_stad = @stad AND adres_postcode = @postcode;";
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
        #endregion

        #region VoegAdresToe Method
        public void VoegAdresToe(Adres a)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([adres_straat] ,[adres_huisnummer] ,[adres_stad] ,[adres_postcode]) VALUES (@straat ,@huisnummer ,@stad ,@postcode);";
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
        #endregion

        #region GeefAdres Method
        public Adres GeefAdres(int aId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VerwijderAdres Method
        public void VerwijderAdres(Adres a)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region UpdateAdres Method
        public void UpdateAdres(Adres a)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
