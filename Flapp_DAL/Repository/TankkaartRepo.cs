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

        public bool BestaatBestuurder(Bestuurder bestuurder)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE bestuurderId = @bestuurderId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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

        public bool BestaatTankkaart(Tankkaart t)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE kaartnr = @kaartnr AND geldigheidsdatum = @geldigheidsdatum AND pincode = @pincode" +
                "AND brandstoftype = @brandstoftype AND geblokkeerd = @geblokkeerd";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@pincode", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = t.Pincode;
                    cmd.Parameters["@brandstoftype"].Value = t.Brandstof;
                    cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

                    int tankkaartBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (tankkaartBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public bool BestaatTankkaart(int kaartnr)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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

        public Dictionary<int, Tankkaart> GeefAlleTankkaarten()
        {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId;";
            //string query = "SELECT * FROM [Project_Flapp_DB].[dbo].[Tankkaart];";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        if (tankkaarten.ContainsKey((int)r["tankkaartId"]))
                        {
                            Tankkaart dicTankkaart = tankkaarten[(int)r["tankkaartId"]];
                            Brandstof b = new Brandstof((string)r["naam"]);
                            dicTankkaart.Brandstof.Naam += $", {b.Naam}";
                        }
                        else
                        {
                            Brandstof b = null;
                            if (!r.IsDBNull(r.GetOrdinal("naam")))
                            {
                                b = new Brandstof((string)r["naam"]);
                            }
                            Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], b, (bool)r["geblokkeerd"]);
                            //Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], b);
                            tankkaarten.Add(tankkaart.Kaartnummer, tankkaart);
                        }

                    }
                    r.Close();
                    return tankkaarten;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public Tankkaart GeefTankkaart(int kaartnr)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM dbo.Tankkaart WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@tankkaartId";
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
                    Tankkaart gevondenTankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], b, bs, (bool)r["geblokkeerd"]);
                    return gevondenTankkaart;
                }
                catch (Exception ex) { throw new Exception("TankkaartRepo", ex); }
                finally { conn.Close(); }
            }
        }

        public bool HeeftBestuurder(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM [dbo].[Tankkaart] WHERE bestuurderId=@bestuurderId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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

        public void UpdateTankkaart(Tankkaart t)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE Tankkaart SET geldigheidsdatum = @geldigheidsdatum OR pincode = @pincode" +
                "OR brandstoftype = @brandstoftype OR geblokkeerd = @geblokkeerd WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@brandstoftype"].Value = t.Brandstof;
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

        public void VerwijderTankkaart(Tankkaart t)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Tankkaart] WHERE tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
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

        public void VoegTankkaartToe(Tankkaart t)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([tankkaartId] ,[geldigheidsdatum] ,[brandstoftype] ,[bestuurder_id] ,[geblokkeerd]) VALUES (@tankkaartId ,@geldigheidsdatum ,@brandstoftype ,@bestuurder_id ,@geblokkeerd);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@brandstoftype"].Value = t.Brandstof;
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
