using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;

namespace Flapp_DAL.Repository
{
    public class TankkaartRepo : ITankkaartRepo
    {
        private string _connString;

        public TankkaartRepo(string connString)
        {
            _connString = connString;

        }

        public bool BestaatTankkaart(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE kaartnr = @kaartnr AND geldigheidsdatum = @geldigheidsdatum AND pincode = @pincode" +
                "AND brandstoftype = @brandstoftype AND bestuurder_id = @bestuurder_id AND geblokkeerd = @geblokkeerd" +
                "AND geblokkeerd = @geblokkeerd;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@pincode", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = t.Pincode;
                    cmd.Parameters["@brandstoftype"].Value = t.Brandstoftype;
                    cmd.Parameters["@bestuurder_id"].Value = 1;
                    cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

                    int tankkaartBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (tankkaartBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public bool BestaatTankkaartNr(int kaartnr) {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Tankkaart> GeefAlleTankkaarten() {
            throw new NotImplementedException();
        }

        public Tankkaart GeefTankkaart(int kaartnr)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM dbo.Tankkaart WHERE kaartnr = @kaartnr;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@kaartnr";
                pId.DbType = DbType.Int32;
                pId.Value = kaartnr;
                cmd.Parameters.Add(pId);
                conn.Open();

                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Brandstof b = null;//_bRepo.GeefBrandstof((int)r["brandstof_id"]); // Mag null zijn
                    Bestuurder bs = null;// _bsRepo.GeefBestuurder((int)r["bestuurder_id"]); // Mag null zijn
                    Tankkaart gevondenTankkaart = new Tankkaart((int)r["kaartnr"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], b, bs, (bool)r["geblokkeerd"]);
                    return gevondenTankkaart;
                }
                catch (Exception ex) { throw new Exception("TankkaartRepo", ex); }
                finally { conn.Close(); }
            }
        }

        public void UpdateTankkaart(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Tankkaart] WHERE kaartnr = @kaartnr AND geldigheidsdatum = @geldigheidsdatum AND pincode = @pincode" +
                "AND brandstoftype = @brandstoftype AND bestuurder_id = @bestuurder_id AND geblokkeerd = @geblokkeerd;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@brandstoftype"].Value = t.Brandstoftype;
                    cmd.Parameters["@bestuurder_id"].Value = t.Bestuurder.Id;
                    cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void VerwijderTankkaart(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Tankkaart] WHERE kaartnr = @kaartnr;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void VoegTankkaartToe(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([kaartnr] ,[geldigheidsdatum] ,[brandstoftype] ,[bestuurder_id] ,[geblokkeerd]) VALUES (@kaartnr ,@geldigheidsdatum ,@brandstoftype ,@bestuurder_id ,@geblokkeerd);";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@brandstoftype"].Value = t.Brandstoftype;
                    cmd.Parameters["@bestuurder_id"].Value = t.Bestuurder.Id;
                    cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
    }
}
