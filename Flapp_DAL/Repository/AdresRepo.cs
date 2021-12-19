using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            string query = "USE [Project_Flapp_DB]; SELECT 1 [adresId] ,[straat] ,[huisnummer] ,[stad] ,[postcode] FROM [Project_Flapp_DB].[dbo].[Adres] WHERE straat = @straat AND huisnummer = @huisnummer AND stad = @stad AND postcode = @postcode;";
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
        public bool BestaatAdres(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 [adresId] ,[straat] ,[huisnummer] ,[stad] ,[postcode] FROM [Project_Flapp_DB].[dbo].[Adres] WHERE adresId = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@id"].Value = id;

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
        #endregion

        #region GeefAdres Method
        public Adres GeefAdres(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Adres WHERE adresId = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.CommandText = query;
                cmd.Parameters["@id"].Value = id;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Adres adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                    return adres;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public Adres GeefAdres(Adres adres)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Adres WHERE straat = @straat " +
                "AND huisnummer = @huisnummer AND stad = @stad AND postcode = @postcode;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@straat", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@stad", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));

                cmd.CommandText = query;
                cmd.Parameters["@straat"].Value = adres.Straat;
                cmd.Parameters["@huisnummer"].Value = adres.Huisnummer;
                cmd.Parameters["@stad"].Value = adres.Stad;
                cmd.Parameters["@postcode"].Value = adres.Postcode;


                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    return new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public Dictionary<int, string> GeefAlleSteden()
        {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, string> steden = new Dictionary<int, string>();
            string query = "SELECT DISTINCT [stad],[postcode] FROM [Project_Flapp_DB].[dbo].[Adres]";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        ;
                        steden.Add((int)r["postcode"], (string)r["stad"]);
                    }
                    return steden;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public ObservableCollection<Adres> GeefAdressen()
        {
            SqlConnection conn = new SqlConnection(_connString);
            ObservableCollection<Adres> adressen = new ObservableCollection<Adres>();
            string query = "SELECT TOP(20) * FROM Adres ORDER BY stad;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Adres adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                        adressen.Add(adres);
                    }

                    return adressen;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public ObservableCollection<string> GeefStratenStad(int postcode, string stad)
        {
            SqlConnection conn = new SqlConnection(_connString);
            ObservableCollection<string> straten = new ObservableCollection<string>();
            string query = "SELECT DISTINCT straat FROM [Project_Flapp_DB].[dbo].[Adres] WHERE postcode = @postcode AND stad = @stad;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@stad", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@postcode"].Value = postcode;
                cmd.Parameters["@stad"].Value = stad;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        straten.Add((string)r["straat"]);
                    }
                    return straten;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VerwijderAdres Method
        public void VerwijderAdres(Adres a)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Adres] WHERE adresid = @adresid;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@adresid", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@adresid"].Value = a.Id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region UpdateAdres Method
        public void UpdateAdres(Adres a)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Adres] WHERE adresid = @adresid AND straat = @straat AND huisnummer = @huisnummer" +
                "AND stad = @stad AND postcode = @postcode";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@adresid", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@straat", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@stad", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@adresid"].Value = a.Id;
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

        #region ZoekAdressen Method
        public ObservableCollection<Adres> ZoekAdressen(int postcode, string stad, string straat)
        {
            SqlConnection conn = new SqlConnection(_connString);
            ObservableCollection<Adres> adressen = new ObservableCollection<Adres>();
            string query = "SELECT * FROM [Project_Flapp_DB].[dbo].[Adres] WHERE postcode = @postcode AND stad = @stad AND straat = @straat;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@stad", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@straat", SqlDbType.VarChar));

                cmd.CommandText = query;
                cmd.Parameters["@postcode"].Value = postcode;
                cmd.Parameters["@stad"].Value = stad;
                cmd.Parameters["@straat"].Value = straat;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        adressen.Add(new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]));
                    }
                    return adressen;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion
    }
}
