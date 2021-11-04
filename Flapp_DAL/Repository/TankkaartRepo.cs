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
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE bestuurder_id = @bestuurder_id;";
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
                "AND brandstoftype = @brandstoftype AND bestuurder_id = @bestuurder_id AND geblokkeerd = @geblokkeerd" +
                "AND geblokkeerd = @geblokkeerd;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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

        public bool BestaatTankkaart(int kaartnr)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Tankkaart WHERE kaartnr = @kaartnr;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = kaartnr;

                    int tankkaartBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (tankkaartBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public IReadOnlyList<Tankkaart> GeefAlleTankkaarten()
        {
            throw new NotImplementedException();
        }

        // Bro hoezo vraag je alle kaarten maar je wilt een kaartnr??? LOGIC 101
        //public IReadOnlyList<Tankkaart> GeefAlleTankkaarten(int kaartnr, DateTime geldigheidsdatum, string pincode, Brandstof brandstof, Bestuurder bestuurder, bool geblokkeerd, bool strikt = true) {
        //    List<Tankkaart> truitjes = new List<Tankkaart>();
        //    SqlConnection conn = new SqlConnection();
        //    string query = "SELECT * FROM [dbo].[Tankkaart] WHERE ";
        //    bool AND = false;
        //    if (kaartnr > 0) {
        //        AND = true;
        //        if (strikt) query += " kaartnr=@kaartnr";
        //        else query += " UPPER(kaartnr)=UPPER(@kaartnr)";
        //    }
        //    if (geldigheidsdatum.GetHashCode() == 0) {
        //        if (AND) query += " AND "; else AND = false;
        //        if (strikt) query += " geldigheidsdatum=@geldigheidsdatum";
        //        else query += " UPPER(geldigheidsdatum)=UPPER(@geldigheidsdatum) ";
        //    }
        //    if (!string.IsNullOrWhiteSpace(pincode)) {
        //        if (AND) query += " AND "; else AND = false;
        //        if (strikt) query += " seizoen=@seizoen";
        //        else query += " UPPER(seizoen)=UPPER(@seizoen) ";
        //    }
        //    if (brandstof != null) {
        //        if (AND) query += " AND "; else AND = false;
        //        if (strikt) query += " brandstof_naam=@brandstof_naam";
        //        else query += " UPPER(brandstof_naam)=UPPER(@brandstof) ";
        //    }
        //    if (bestuurder != null) {
        //        if (AND) query += " AND "; else AND = false;
        //        if (strikt) query += " bestuurder=@bestuurder_id";
        //        else query += " UPPER(bestuurder)=UPPER(@bestuurder) ";
        //    }
        //    if (AND) query += " AND "; else AND = false;
        //    if (strikt) query += " geblokkeerd=@geblokkeerd";
        //    else query += " UPPER(geblokkeerd)=UPPER(@geblokkeerd) ";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        conn.Open();
        //        try {
        //            cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));
        //            cmd.CommandText = query;
        //            cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;

        //        } catch (Exception ex) { throw new Exception(ex.Message); } finally { conn.Close(); }
        //    }
        //}

        public IReadOnlyList<Voertuig> GeefAlleTankkaartenZonderBestuurders()
        {
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

        public bool HeeftBestuurder(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM [dbo].[Tankkaart] WHERE bestuurder_id=@bestuurder_id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurder_id"].Value = b.Id;

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
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Tankkaart] WHERE kaartnr = @kaartnr AND geldigheidsdatum = @geldigheidsdatum AND pincode = @pincode" +
                "AND brandstoftype = @brandstoftype AND bestuurder_id = @bestuurder_id AND geblokkeerd = @geblokkeerd;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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

        public void VerwijderBestuurder(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Tankkaart] WHERE bestuurder_id = @bestuurder_id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurder_id"].Value = b.Id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void VerwijderTankkaart(Tankkaart t)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Tankkaart] WHERE kaartnr = @kaartnr;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@kaartnr", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@kaartnr"].Value = t.Kaartnummer;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void VoegBestuurderToe(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([bestuurder_id]) SELECT [dbo].[Bestuurder].[Id] FROM [dbo].[Bestuurder] WHERE [dbo].[Bestuurder].[Id]=@bestuurder_id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurder_id"].Value = b.Id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public void VoegTankkaartToe(Tankkaart t)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([kaartnr] ,[geldigheidsdatum] ,[brandstoftype] ,[bestuurder_id] ,[geblokkeerd]) VALUES (@kaartnr ,@geldigheidsdatum ,@brandstoftype ,@bestuurder_id ,@geblokkeerd);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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

        IReadOnlyList<Tankkaart> ITankkaartRepo.GeefAlleTankkaartenZonderBestuurders()
        {
            throw new NotImplementedException();
        }
    }
}
