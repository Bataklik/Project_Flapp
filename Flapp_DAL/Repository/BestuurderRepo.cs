using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Flapp_DAL.Repository {
    public class BestuurderRepo : IBestuurderRepo {
        private string _connString;

        public BestuurderRepo(string connString) {
            _connString = connString;
        }

        #region BestaatBestuurder Method
        public bool BestaatBestuurder(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Bestuurder WHERE naam = @naam AND voornaam = @voornaam AND geboortedatum = @geboorte AND rijksregister = @rijksregister AND " +
                "geslacht = @geslacht;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;

                    if (b.Geslacht == Geslacht.M) { cmd.Parameters["@geslacht"].Value = 1; }
                    else { cmd.Parameters["@geslacht"].Value = 0; }

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool BestaatBestuurderId(int id) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Bestuurder WHERE bestuurderId = @id;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = id;

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());
                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool HeeftBestuurderTankkaart(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT 1 FROM Bestuurder WHERE bestuurderId=@bestuurderId AND tankkaartId IS NOT NULL";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurderId"].Value = b.Id;

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());
                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VoegBestuurderToe Method
        public int VoegBestuurderToe(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB] INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[adresid] ,[voertuigId],[tankkaartId],[geslacht]) output INSERTED.bestuurderId  VALUES (@naam ,@voornaam ,@geboorte ,@rijksregister ,@adresid ,@voertuigid,@tankkaartid ,@geslacht)";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.AddWithValue("@naam", b.Naam);
                cmd.Parameters.AddWithValue("@voornaam", b.Voornaam);
                cmd.Parameters.AddWithValue("@geboorte", b.Geboortedatum);
                cmd.Parameters.AddWithValue("@rijksregister", b.Rijksregisternummer);
                if (b.Adres != null) { cmd.Parameters.AddWithValue("@adresid", b.Adres.Id); }
                else { cmd.Parameters.AddWithValue("@adresid", DBNull.Value); }
                if (b.Voertuig != null) { cmd.Parameters.AddWithValue("@voertuigid", b.Voertuig.VoertuigID); }
                else { cmd.Parameters.AddWithValue("@voertuigid", DBNull.Value); }
                if (b.Tankkaart != null) { cmd.Parameters.AddWithValue("@tankkaartid", b.Tankkaart.Kaartnummer); }
                else { cmd.Parameters.AddWithValue("@tankkaartid", DBNull.Value); }

                if (b.Geslacht == Geslacht.M) { cmd.Parameters.AddWithValue("@geslacht", 1); }
                else { cmd.Parameters.AddWithValue("@geslacht", 0); }
                try {
                    conn.Open();
                    cmd.CommandText = query;
                    return (int)cmd.ExecuteScalar();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public int VoegBestuurderToeZonderAdres(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB] INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[geslacht]) output INSERTED.bestuurderId VALUES (@naam ,@voornaam ,@geboorte ,@rijksregister  ,@geslacht)";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                    if (b.Geslacht == Geslacht.M) { cmd.Parameters["@geslacht"].Value = 1; }
                    else { cmd.Parameters["@geslacht"].Value = 0; }
                    int bestuurderId = (int)cmd.ExecuteScalar();
                    return bestuurderId;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region UpdateBestuurder Method
        public void UpdateBestuurder(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE [dbo].[Bestuurder] SET [naam] = @naam ,[voornaam] = @voornaam ,[geboortedatum] = @geboorte ,[rijksregister] = @rijksregister ,[adresId] = @adres ,[voertuigId] = @voertuig ,[tankkaartId] = @tankkaart ,[geslacht] = @geslacht WHERE bestuurderId = @id;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@voertuig", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaart", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = b.Id;
                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;

                    if (b.Adres != null) { cmd.Parameters["@adres"].Value = b.Adres.Id; }
                    else { cmd.Parameters["@adres"].Value = DBNull.Value; }

                    if (b.Voertuig != null) { cmd.Parameters["@voertuig"].Value = b.Voertuig.VoertuigID; }
                    else { cmd.Parameters["@voertuig"].Value = DBNull.Value; }

                    if (b.Tankkaart != null) { cmd.Parameters["@tankkaart"].Value = b.Tankkaart.Kaartnummer; }
                    else { cmd.Parameters["@tankkaart"].Value = DBNull.Value; }

                    if (b.Geslacht == Geslacht.M) { cmd.Parameters["@geslacht"].Value = 1; } else { cmd.Parameters["@geslacht"].Value = 0; }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public void VoegVoertuigToeAanBestuurder(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE [dbo].[Bestuurder] SET [voertuigId] = @voertuig WHERE bestuurderId = @id;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voertuig", SqlDbType.Int));


                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = b.Id;
                    cmd.Parameters["@voertuig"].Value = b.Voertuig.VoertuigID;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public void VoegTankkaartToeAanBestuurder(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE [dbo].[Bestuurder] SET tankkaartId=@tankkaartId WHERE bestuurderId=@bestuurderId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurderId"].Value = t.Bestuurder.Id;
                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VerwijderBestuurder Method
        public void VerwijderBestuurder(Bestuurder b) {
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "DELETE FROM [dbo].[Rijbewijs_Bestuurder] WHERE bestuurderid = @id1;";
                    cmd.Parameters.AddWithValue("@id1", b.Id);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM [dbo].[Bestuurder] WHERE bestuurderid = @id2;";
                    cmd.Parameters.AddWithValue("@id2", b.Id);
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
        public void VerwijderTankkaartVanBestuurder(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE [dbo].[Bestuurder] SET tankkaartId=NULL WHERE bestuurderId=@bestuurderId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@bestuurderId"].Value = b.Id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefBestuurder Method
        public Bestuurder GeefBestuurder(Bestuurder b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Bestuurder WHERE bestuurderid = @bestuurderid";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@bestuurderid", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@bestuurderid"].Value = b.Id;
                conn.Open();
                Bestuurder bestuurder = null;
                Geslacht bestuurderGeslacht = Geslacht.M;
                Adres bestuurderAdres = null;
                Dictionary<int, Rijbewijs> bestuurderRijbewijzen = null;
                Voertuig bestuurderVoertuig = null;
                Dictionary<int, Brandstof> bestuurderVoertuigBrandstof = new Dictionary<int, Brandstof>();
                List<Brandstof> bestuurderTankkaartBrandstof = new List<Brandstof>();
                Tankkaart bestuurderTankkaart = null;
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurder != null) {
                            if (!r.IsDBNull("rijbewijsId") && !bestuurderRijbewijzen.ContainsKey((int)r["rijbewijsId"])) { bestuurderRijbewijzen.Add((int)r["rijbewijsId"], new Rijbewijs((int)r["rijbewijsId"], (string)r[29])); }
                            if (!r.IsDBNull("brandstofId") && !bestuurderVoertuigBrandstof.ContainsKey((int)r["brandstofId"])) {
                                bestuurderVoertuigBrandstof.Add((int)r["brandstofId"], new Brandstof((int)r["brandstofId"], (string)r[33]));
                                bestuurder.Voertuig.ZetBrandstofTypeLijst(bestuurderVoertuigBrandstof.Values.ToList());
                            }
                        }
                        else {
                            if (!r.IsDBNull("adresId")) { bestuurderAdres = new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            if (!r.IsDBNull("rijbewijsId")) { bestuurderRijbewijzen = new Dictionary<int, Rijbewijs> { { (int)r["rijbewijsId"], new Rijbewijs((int)r["rijbewijsId"], (string)r[29]) } }; }
                            if (!r.IsDBNull("voertuigId")) {
                                bestuurderVoertuigBrandstof.Add((int)r["brandstofId"], new Brandstof((int)r["brandstofId"], (string)r[33]));
                                bestuurderVoertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], bestuurderVoertuigBrandstof.Values.ToList(), (string)r["type"], (string)r["kleur"], (int)r["deuren"]);
                            }
                            if (!r.IsDBNull("tankkaartId")) { bestuurderTankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], bestuurderTankkaartBrandstof, (bool)r["geblokkeerd"]); }
                            bestuurderGeslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            bestuurder = new((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], bestuurderGeslacht, bestuurderAdres, Convert.ToString(r["geboortedatum"]), (string)r["rijksregister"], bestuurderRijbewijzen.Values.ToList(), bestuurderVoertuig, bestuurderTankkaart);
                        }
                    }
                    return bestuurder;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }

            }
        }
        public Bestuurder GeefBestuurder(int bId) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM Bestuurder LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId  LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId WHERE Bestuurder.bestuurderId = @id;";
            using (SqlCommand cmd = conn.CreateCommand()) {

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                cmd.CommandText = query;

                cmd.Parameters["@id"].Value = bId;
                conn.Open();
                Bestuurder bestuurder = null;
                Geslacht bestuurderGeslacht = Geslacht.M;
                Adres bestuurderAdres = null;
                Dictionary<int, Rijbewijs> bestuurderRijbewijzen = null;
                Voertuig bestuurderVoertuig = null;
                Dictionary<int, Brandstof> bestuurderVoertuigBrandstof = new Dictionary<int, Brandstof>();
                List<Brandstof> bestuurderTankkaartBrandstof = new List<Brandstof>();
                Tankkaart bestuurderTankkaart = null;
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurder != null) {
                            if (!r.IsDBNull("rijbewijsId") && !bestuurderRijbewijzen.ContainsKey((int)r["rijbewijsId"])) { bestuurderRijbewijzen.Add((int)r["rijbewijsId"], new Rijbewijs((int)r["rijbewijsId"], (string)r[29])); }
                            if (!r.IsDBNull("brandstofId") && !bestuurderVoertuigBrandstof.ContainsKey((int)r["brandstofId"])) {
                                bestuurderVoertuigBrandstof.Add((int)r["brandstofId"], new Brandstof((int)r["brandstofId"], (string)r[33]));
                                bestuurder.Voertuig.ZetBrandstofTypeLijst(bestuurderVoertuigBrandstof.Values.ToList());
                            }
                        }
                        else {

                            if (!r.IsDBNull("adresId")) { bestuurderAdres = new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            if (!r.IsDBNull("rijbewijsId")) { bestuurderRijbewijzen = new Dictionary<int, Rijbewijs> { { (int)r["rijbewijsId"], new Rijbewijs((int)r["rijbewijsId"], (string)r[29]) } }; }
                            if (!r.IsDBNull("voertuigId")) {
                                bestuurderVoertuigBrandstof.Add((int)r["brandstofId"], new Brandstof((int)r["brandstofId"], (string)r[33]));
                                bestuurderVoertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], bestuurderVoertuigBrandstof.Values.ToList(), (string)r["type"], (string)r["kleur"], (int)r["deuren"]);
                            }
                            if (!r.IsDBNull("tankkaartId")) { bestuurderTankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], bestuurderTankkaartBrandstof, (bool)r["geblokkeerd"]); }
                            bestuurderGeslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            bestuurder = new((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], bestuurderGeslacht, bestuurderAdres, Convert.ToString(r["geboortedatum"]), (string)r["rijksregister"], bestuurderRijbewijzen.Values.ToList(), bestuurderVoertuig, bestuurderTankkaart);
                        }
                    }
                    return bestuurder;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefAlleBestuurders Methodbe
        public Dictionary<int, Bestuurder> GeefAlleBestuurders() {
            // !! BrandstofLijst is NULL in voertuig
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        var bestId = (int)r["bestuurderId"];
                        Bestuurder bestuurder;
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            bestuurder = bestuurders[(int)r["bestuurderId"]];
                            bestuurder.Rijbewijzen.Add(new Rijbewijs(r[12].ToString()));
                            if (DBNull.Value != r[29]) { bestuurders[(int)r["bestuurderId"]].Voertuig.Brandstof.Add(new Brandstof(r[29].ToString())); }
                        }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) { adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                string naam = r[29].ToString();
                                List<Brandstof> brandstoffen = new List<Brandstof> { new Brandstof(naam) };
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstoffen, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            if (!r.IsDBNull(r.GetOrdinal("tankkaartId"))) {
                                Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]);
                                if (DBNull.Value != r[29]) { tankkaart.Brandstoffen.Add(new Brandstof(r[29].ToString())); ; }
                                bestuurder.ZetTankkaart(tankkaart);
                            }

                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurders(bool heeftVoertuig) {
            // !! BrandstofLijst is NULL in voertuig
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voertuigId IS NOT null;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voertuigId IS null;"; }
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) { bestuurders[(int)r["bestuurderId"]].Rijbewijzen.Add(new Rijbewijs(r[12].ToString())); }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) { adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof>(), (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            //if (!r.IsDBNull(r.GetOrdinal("tankkaartId"))) {
                            //    var x = r["geldigheidsdatum"];
                            //    Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]);
                            //    bestuurder.ZetTankkaart(tankkaart);
                            //}
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderTankkaarten() {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.tankkaartId IS NULL;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) { bestuurders[(int)r["bestuurderId"]].Rijbewijzen.Add(new Rijbewijs(r[12].ToString())); bestuurders[(int)r["bestuurderId"]].Voertuig.Brandstof.Add(new Brandstof(int.Parse(r[28].ToString()), r[29].ToString())); }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) { adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);

                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                List<Brandstof> brandstoffen = new List<Brandstof> { new Brandstof(int.Parse(r[28].ToString()), r[29].ToString()) };
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstoffen, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }

                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaam(string naam, bool heeftVoertuig) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voertuigId IS NOT null;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voertuigId IS null;"; }
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = $"{naam}%";
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            Bestuurder dicBestuurder = bestuurders[(int)r["bestuurderId"]];
                            dicBestuurder.Rijbewijzen.Add(new Rijbewijs(r[12].ToString()));
                        }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof>(), (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpVoornaam(string voornaam, bool heeftVoertuig) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS NOT null;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS null;"; }
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@voornaam"].Value = $"{voornaam}%";
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            Bestuurder dicBestuurder = bestuurders[(int)r["bestuurderId"]];
                            dicBestuurder.Rijbewijzen.Add(new Rijbewijs(r[12].ToString()));
                        }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof>(), (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpDatum(DateTime geboorte, bool heeftVoertuig) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS NOT null;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS  null;"; }
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Date));
                cmd.CommandText = query;
                cmd.Parameters["@date"].Value = geboorte;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            Bestuurder dicBestuurder = bestuurders[(int)r["bestuurderId"]];
                            dicBestuurder.Rijbewijzen.Add(new Rijbewijs(r[12].ToString()));
                        }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof>(), (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaam(string naam, string voornaam, bool heeftVoertuig) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS NOT null;;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS null;;"; }

            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = $"{naam}%";
                cmd.Parameters["@voornaam"].Value = $"{voornaam}%";

                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            Bestuurder dicBestuurder = bestuurders[(int)r["bestuurderId"]];
                            dicBestuurder.Rijbewijzen.Add(new Rijbewijs(r[12].ToString()));
                        }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof>(), (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaamDatum(string naam, string voornaam, DateTime geboorte, bool heeftVoertuig) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS NOT null;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS null;"; }
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Date));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = $"{naam}%";
                cmd.Parameters["@voornaam"].Value = $"{voornaam}%";
                cmd.Parameters["@date"].Value = geboorte;


                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            Bestuurder dicBestuurder = bestuurders[(int)r["bestuurderId"]];
                            dicBestuurder.Rijbewijzen.Add(new Rijbewijs(r[12].ToString()));
                        }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) {
                                adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                            }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof>(), (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder);
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderVoertuig() {
            // !! BrandstofLijst is NULL in voertuig
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voertuigId IS NULL;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (bestuurders.ContainsKey((int)r["bestuurderId"])) { bestuurders[(int)r["bestuurderId"]].Rijbewijzen.Add(new Rijbewijs(r[12].ToString())); }
                        else {
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) { adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);

                            if (!r.IsDBNull(r.GetOrdinal("tankkaartId"))) {
                                Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]);
                                bestuurder.ZetTankkaart(tankkaart);
                            }

                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        #endregion
    }
}
