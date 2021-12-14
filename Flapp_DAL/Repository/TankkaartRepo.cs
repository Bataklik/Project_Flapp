using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository {
    public class TankkaartRepo : ITankkaartRepo {
        private string _connString;

        public TankkaartRepo(string connString) {
            _connString = connString;

        }

        public bool BestaatBestuurder(Bestuurder bestuurder) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE bestuurderId = @bestuurderId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurder_id"].Value = bestuurder.Id;

                    int tankkaartBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (tankkaartBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public bool BestaatTankkaart(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM Tankkaart WHERE tankkaartid = @tankkaartid AND geldigheidsdatum = @geldigheidsdatum";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartid", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartid"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;

                    int tankkaartBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (tankkaartBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public bool BestaatTankkaart(int kaartnr) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartId"].Value = kaartnr;

                    int tankkaartBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (tankkaartBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public Dictionary<int, Tankkaart> GeefAlleTankkaarten() {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (tankkaarten.ContainsKey((int)r["tankkaartId"])) {
                            Tankkaart dicTankkaart = tankkaarten[(int)r["tankkaartId"]];
                            dicTankkaart.Brandstoffen.Add(new Brandstof((string)r["naam"]));
                        }
                        else {
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                            Tankkaart t = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], brandstof ,(bool)r["geblokkeerd"]);

                            tankkaarten.Add(t.Kaartnummer, t);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return tankkaarten;
        }

        public Dictionary<int, Tankkaart> GeefAlleTankkaarten(int? kaartnummer, DateTime? geldigheidsdatum) {
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            List<string> subQuery = new List<string>();
            int numberofparams = 0;

            bool kaartnrIsNull = true;
            if (kaartnummer != null) {
                kaartnrIsNull = false;
                if (numberofparams > 0) {
                    subQuery.Add(" AND ");
                }
                numberofparams++;
                subQuery.Add("tankkaartId=@tankkaartId");
            }

            bool geldigheidsdatumIsNull = true;
            if (geldigheidsdatum != null) {
                geldigheidsdatumIsNull = false;
                if (numberofparams > 0) {
                    subQuery.Add(" AND ");
                }
                numberofparams++;
                subQuery.Add("geldigheidsdatum=@geldigheidsdatum");
            }

            string query = "";
            if (numberofparams > 0) {
                query = $"SELECT * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId WHERE {String.Join("", subQuery)};";
            }

            SqlConnection conn = new SqlConnection(_connString);
            
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();

                    if (!kaartnrIsNull) {
                        cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                        cmd.Parameters["@tankkaartId"].Value = kaartnummer;
                    }
                    if (!geldigheidsdatumIsNull) {
                        cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));
                        cmd.Parameters["@geldigheidsdatum"].Value = geldigheidsdatum;
                    }

                    cmd.CommandText = query;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (tankkaarten.ContainsKey((int)r["tankkaartId"])) {
                            Tankkaart dicTankkaart = tankkaarten[(int)r["tankkaartId"]];
                            dicTankkaart.Brandstoffen.Add(new Brandstof((string)r["naam"]));
                        }
                        else {
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                            Tankkaart t = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]);
                            tankkaarten.Add(t.Kaartnummer, t);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return tankkaarten;
        }

        public Tankkaart GeefTankkaart(int kaartnr) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM dbo.Tankkaart WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@tankkaartId";
                pId.DbType = DbType.Int32;
                pId.Value = kaartnr;
                cmd.Parameters.Add(pId);
                conn.Open();

                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    List<Brandstof> b = null;//_bRepo.GeefBrandstof((int)r["brandstof_id"]); // Mag null zijn
                    Bestuurder bs = null;// _bsRepo.GeefBestuurder((int)r["bestuurder_id"]); // Mag null zijn
                    Tankkaart gevondenTankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], b, bs, (bool)r["geblokkeerd"]);
                    return gevondenTankkaart;
                }
                catch (Exception ex) { throw new Exception("TankkaartRepo", ex); }
                finally { conn.Close(); }
            }
        }

        public bool HeeftBestuurder(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM [dbo].[Tankkaart] WHERE bestuurderId=@bestuurderId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurderIds"].Value = b.Id;

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void UpdateTankkaart(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE Tankkaart SET geldigheidsdatum = @geldigheidsdatum, pincode = @pincode" +
                ", geblokkeerd = @geblokkeerd WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = t.Pincode;
                    cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        //public void VerwijderBestuurder(Bestuurder b)
        //{
        //    SqlConnection conn = new SqlConnection(_connString);
        //    string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Tankkaart] WHERE bestuurder_id = @bestuurder_id;";
        //    using (SqlCommand cmd = conn.CreateCommand())
        //    {
        //        conn.Open();
        //        try
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

        //            cmd.CommandText = query;

        //            cmd.Parameters["@bestuurder_id"].Value = b.Id;

        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //}

        public void VerwijderTankkaart(Tankkaart t) {
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "DELETE FROM [dbo].[Brandstof_Tankkaart] WHERE tankkaartid = @id1;";
                    cmd.Parameters.AddWithValue("@id1", t.Kaartnummer);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM [dbo].[Tankkaart] WHERE tankkaartid = @id2;";
                    cmd.Parameters.AddWithValue("@id2", t.Kaartnummer);
                    cmd.ExecuteNonQuery();

                    trx.Commit();
                }
                catch (Exception ex) {
                    trx.Rollback();
                    throw new Exception(ex.Message);
                }
                finally { conn.Close(); }
            }
        }

        //public void VoegBestuurderToe(Bestuurder b)
        //{
        //    SqlConnection conn = new SqlConnection(_connString);
        //    string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([bestuurder_id]) SELECT [dbo].[Bestuurder].[Id] FROM [dbo].[Bestuurder] WHERE [dbo].[Bestuurder].[Id]=@bestuurder_id;";
        //    using (SqlCommand cmd = conn.CreateCommand())
        //    {
        //        conn.Open();
        //        try
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

        //            cmd.CommandText = query;

        //            cmd.Parameters["@bestuurder_id"].Value = b.Id;

        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //}

        public void VoegTankkaartToe(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "INSERT INTO [dbo].[Tankkaart] ([tankkaartId] ,[geldigheidsdatum] ,[brandstoftype] ,[geblokkeerd]) VALUES (@tankkaartId ,@geldigheidsdatum ,@brandstoftype ,@geblokkeerd);";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@brandstoftype"].Value = t.Bestuurder;
                    cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
    }
}
