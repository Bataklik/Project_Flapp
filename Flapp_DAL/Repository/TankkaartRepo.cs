﻿using Flapp_BLL.Exceptions.ModelExpections;
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
            string query = "SELECT * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId;";
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
                            Adres a = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                a = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }

                            Bestuurder b = null;
                            if (!r.IsDBNull(r.GetOrdinal("geslacht")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("bestuurderId")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("voornaam")) && !r.IsDBNull(r.GetOrdinal("geboortedatum")) && !r.IsDBNull(r.GetOrdinal("rijksregister"))) {
                                Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                                List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[25].ToString()) };
                                b = new Bestuurder((int)r["bestuurderId"], r[9].ToString(), (string)r["voornaam"], geslacht, a, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            }
                            Tankkaart t = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], brandstof, b, (bool)r["geblokkeerd"]);

                            tankkaarten.Add(t.Kaartnummer, t);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return tankkaarten;
        }

        public Dictionary<int, Tankkaart> GeefAlleTankkaartenOpKaartnummer(int kaartnummer) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE Tankkaart.tankkaartId=@tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));

                cmd.CommandText = query;

                cmd.Parameters["@tankkaartId"].Value = kaartnummer;
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
                            Adres a = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                a = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }

                            Bestuurder b = null;
                            if (!r.IsDBNull(r.GetOrdinal("geslacht")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("bestuurderId")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("voornaam")) && !r.IsDBNull(r.GetOrdinal("geboortedatum")) && !r.IsDBNull(r.GetOrdinal("rijksregister"))) {
                                Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                                List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[25].ToString()) };
                                b = new Bestuurder((int)r["bestuurderId"], r[9].ToString(), (string)r["voornaam"], geslacht, a, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            }
                            Tankkaart t = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], brandstof, b, (bool)r["geblokkeerd"]);

                            tankkaarten.Add(t.Kaartnummer, t);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return tankkaarten;
        }

        public Dictionary<int, Tankkaart> GeefAlleTankkaartenOpGeldigheidsdatum(DateTime geldigheidsdatum) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE geldigheidsdatum=@geldigheidsdatum;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));

                    cmd.CommandText = query;

                    cmd.Parameters["@geldigheidsdatum"].Value = geldigheidsdatum;

                    SqlDataReader r = cmd.ExecuteReader();

                    while (r.Read()) {
                        if (tankkaarten.ContainsKey((int)r["tankkaartId"])) {
                            Tankkaart dicTankkaart = tankkaarten[(int)r["tankkaartId"]];
                            dicTankkaart.Brandstoffen.Add(new Brandstof((string)r["naam"]));
                        }
                        else {
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                            Adres a = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                a = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }

                            Bestuurder b = null;
                            if (!r.IsDBNull(r.GetOrdinal("geslacht")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("bestuurderId")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("voornaam")) && !r.IsDBNull(r.GetOrdinal("geboortedatum")) && !r.IsDBNull(r.GetOrdinal("rijksregister"))) {
                                Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                                List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[25].ToString()) };
                                b = new Bestuurder((int)r["bestuurderId"], r[9].ToString(), (string)r["voornaam"], geslacht, a, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            }
                            Tankkaart t = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], brandstof, b, (bool)r["geblokkeerd"]);

                            tankkaarten.Add(t.Kaartnummer, t);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return tankkaarten;
        }


        public Dictionary<int, Tankkaart> GeefAlleTankkaartenZonderBestuurder() {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT TOP(20) * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE Bestuurder.tankkaartId is NULL;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    cmd.CommandText = query;
                    SqlDataReader r = cmd.ExecuteReader();

                    while (r.Read()) {
                        if (tankkaarten.ContainsKey((int)r["tankkaartId"])) {
                            Tankkaart dicTankkaart = tankkaarten[(int)r["tankkaartId"]];
                            dicTankkaart.Brandstoffen.Add(new Brandstof((string)r["naam"]));
                        }
                        else {
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                            Adres a = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                a = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }

                            Bestuurder b = null;
                            if (!r.IsDBNull(r.GetOrdinal("geslacht")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("bestuurderId")) && !r.IsDBNull(r.GetOrdinal("naam")) && !r.IsDBNull(r.GetOrdinal("voornaam")) && !r.IsDBNull(r.GetOrdinal("geboortedatum")) && !r.IsDBNull(r.GetOrdinal("rijksregister"))) {
                                Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                                List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[25].ToString()) };
                                b = new Bestuurder((int)r["bestuurderId"], r[9].ToString(), (string)r["voornaam"], geslacht, a, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            }
                            Tankkaart t = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], brandstof, b, (bool)r["geblokkeerd"]);

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
                    if (t.Geblokkeerd) { cmd.Parameters["@geblokkeerd"].Value = 1; }
                    else { cmd.Parameters["@geblokkeerd"].Value = 0; }

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
                    cmd.CommandText = "DELETE FROM [dbo].[Brandstof_Tankkaart] WHERE tankkaartid=@id1;";
                    cmd.Parameters.AddWithValue("@id1", t.Kaartnummer);
                    cmd.ExecuteNonQuery();

                    if (t.Bestuurder != null) {
                        cmd.CommandText = "UPDATE [dbo].[Bestuurder] SET tankkaartId=NULL WHERE bestuurderId=@id3;";
                        cmd.Parameters.AddWithValue("@id3", t.Bestuurder.Id);
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "DELETE FROM [dbo].[Tankkaart] WHERE tankkaartid=@id2;";
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

        //public void VoegTankkaartToe(Tankkaart t) {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    string query = "INSERT INTO [dbo].[Tankkaart] ([tankkaartId] ,[geldigheidsdatum] ,[brandstoftype] ,[geblokkeerd]) VALUES (@tankkaartId ,@geldigheidsdatum ,@brandstoftype ,@geblokkeerd);";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        conn.Open();
        //        try {
        //            cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
        //            cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.Date));
        //            cmd.Parameters.Add(new SqlParameter("@brandstoftype", SqlDbType.NVarChar));
        //            cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

        //            cmd.CommandText = query;

        //            cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
        //            cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
        //            cmd.Parameters["@brandstoftype"].Value = t.Bestuurder;
        //            cmd.Parameters["@geblokkeerd"].Value = t.Geblokkeerd;

        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //}

        //public int VoegTankkaartToe(Tankkaart t) {
        //    string query = "INSERT INTO [dbo].[Tankkaart] ([geldigheidsdatum], [pincode], [geblokkeerd]) output INSERTED.tankkaartId  VALUES (@geldigheidsdatum ,@pincode ,@geblokkeerd)";
        //    SqlConnection conn = new SqlConnection(_connString);
        //    SqlCommand cmd = new(query, conn);
        //    try {
        //        conn.Open();

        //        cmd.Parameters.AddWithValue("@geldigheidsdatum", t.Geldigheidsdatum);
        //        cmd.Parameters.AddWithValue("@pincode", t.Pincode);
        //        if (t.Geblokkeerd) { cmd.Parameters.AddWithValue("@geblokkeerd", 1); }
        //        else { cmd.Parameters.AddWithValue("@geblokkeerd", 0); }

        //        int tankkaartId = (int)cmd.ExecuteScalar();

        //        return tankkaartId;
        //    }
        //    catch (Exception ex) { throw new TankkaartException(ex.Message); }
        //    finally { conn.Close(); }
        //}

        public int VoegTankkaartToe(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "INSERT INTO Tankkaart ([geldigheidsdatum], [pincode], [geblokkeerd]) output INSERTED.tankkaartId VALUES (@geldigheidsdatum, @pincode, @geblokkeerd);";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = t.Pincode;
                    if (t.Geblokkeerd) cmd.Parameters["@geblokkeerd"].Value = 1;
                    cmd.Parameters["@geblokkeerd"].Value = 0;

                    int tankkaartId = Convert.ToInt32(cmd.ExecuteScalar());
                    return tankkaartId;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

    }
}
