using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            int bestuurderId;
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "INSERT INTO Bestuurder ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[adresid] ,[voertuigId],[tankkaartId],[geslacht]) output INSERTED.bestuurderId VALUES(@naam , @voornaam , @geboorte , @rijksregister , @adresid , @voertuigid, @tankkaartid , @geslacht); ";

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
                    bestuurderId = (int)cmd.ExecuteScalar();

                    b.Rijbewijzen.ForEach(x => {
                        cmd.CommandText = $"INSERT INTO Rijbewijs_Bestuurder ([rijbewijsId] ,[bestuurderId]) VALUES(@rijbewijsId{x.Id}, @bestuurderId{x.Id + 1});";
                        cmd.Parameters.Add(new SqlParameter($"@rijbewijsId{x.Id}", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter($"@bestuurderId{x.Id + 1}", SqlDbType.Int));
                        cmd.Parameters[$"@rijbewijsId{x.Id}"].Value = x.Id;
                        cmd.Parameters[$"@bestuurderId{x.Id + 1}"].Value = bestuurderId;
                        cmd.ExecuteNonQuery();
                    });

                    trx.Commit();
                    return bestuurderId;
                }
                catch (Exception ex) {
                    trx.Rollback();
                    throw new Exception(ex.Message);
                }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region UpdateBestuurder Method
        public void UpdateBestuurder(Bestuurder b) {
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "UPDATE Bestuurder SET [naam] = @naam ,[voornaam] = @voornaam ,[geboortedatum] = @geboorte ,[rijksregister] = @rijksregister ,[adresId] = @adresid ,[voertuigId] = @voertuigid ,[tankkaartId] = @tankkaartid ,[geslacht] = @geslacht WHERE bestuurderId = @id;";

                    cmd.Parameters.AddWithValue("@id", b.Id);
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
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Rijbewijs_Bestuurder WHERE bestuurderId=@id;";
                    cmd.Parameters["@id"].Value = b.Id;
                    cmd.ExecuteNonQuery();

                    b.Rijbewijzen.ForEach(x => {
                        cmd.CommandText = $"INSERT INTO Rijbewijs_Bestuurder ([rijbewijsId] ,[bestuurderId]) VALUES(@rijbewijsId{x.Id}, @bestuurderId{x.Id + 1});";
                        cmd.Parameters.Add(new SqlParameter($"@rijbewijsId{x.Id}", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter($"@bestuurderId{x.Id + 1}", SqlDbType.Int));
                        cmd.Parameters[$"@rijbewijsId{x.Id}"].Value = x.Id;
                        cmd.Parameters[$"@bestuurderId{x.Id + 1}"].Value = b.Id;
                        cmd.ExecuteNonQuery();
                    });

                    trx.Commit();
                }
                catch (Exception ex) {
                    trx.Rollback();
                    throw new Exception(ex.Message);
                }
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

        public void UpdateBestuurder_voertuigId(int VoertuigID)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE [dbo].[Bestuurder] SET [voertuigId] = NULL WHERE voertuigId = @voertuigId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    //cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voertuigId", SqlDbType.Int));


                    cmd.CommandText = query;
                    
                    cmd.Parameters["@voertuigId"].Value = VoertuigID;

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

        #region GeefAlleBestuurders Methods

        public Dictionary<int, Bestuurder> GeefBestuurders(string naam = null, string voornaam = null, DateTime? geboorte = null, bool heeftVoertuig = false) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query;
            if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND CONVERT(varchar(10),Bestuurder.geboortedatum,101) LIKE @date AND Bestuurder.voertuigId IS NOT null;"; }
            else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND CONVERT(varchar(10),Bestuurder.geboortedatum,101) LIKE @date AND Bestuurder.voertuigId IS null;"; }

            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = !string.IsNullOrWhiteSpace(naam) ? $"{naam}%" : "%";
                cmd.Parameters["@voornaam"].Value = !string.IsNullOrWhiteSpace(voornaam) ? $"{voornaam}%" : "%";
                cmd.Parameters["@date"].Value = DateTime.TryParse(geboorte.ToString(), out _) ? geboorte.Value.ToString("dd/MM/yyyy") : "%";
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    Bestuurder bestuurder = null;
                    while (r.Read()) {
                        if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
                            bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
                            Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
                            Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
                            Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


                            if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
                            bestuurder.ZetAdres(adres);
                            bestuurder.ZetVoertuig(voertuig);
                            bestuurder.ZetTankkaart(tankkaart);
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                        else {
                            bestuurder = bestuurders[(int)r["bestuurderId"]];

                            if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
                            if (bestuurder.Voertuig != null) {
                                if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
                            }
                        }
                    }
                    foreach (SqlParameter p in cmd.Parameters) {
                        query = query.Replace(p.ParameterName, p.Value.ToString());

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
            string query = "SELECT TOP(20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId = Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Tankkaart.tankkaartId IS NULL; ";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    Bestuurder bestuurder = null;
                    while (r.Read()) {
                        if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
                            bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
                            Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
                            Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
                            Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


                            if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
                            bestuurder.ZetAdres(adres);
                            bestuurder.ZetVoertuig(voertuig);
                            bestuurder.ZetTankkaart(tankkaart);
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                        else {
                            bestuurder = bestuurders[(int)r["bestuurderId"]];

                            if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
                            if (bestuurder.Voertuig != null) {
                                if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
                            }
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderVoertuig() {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
            string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voertuigId IS NULL;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    Bestuurder bestuurder = null;
                    while (r.Read()) {
                        if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
                            List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
                            bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
                            Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
                            Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
                            Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


                            if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
                            bestuurder.ZetAdres(adres);
                            bestuurder.ZetVoertuig(voertuig);
                            bestuurder.ZetTankkaart(tankkaart);
                            bestuurders.Add(bestuurder.Id, bestuurder);
                        }
                        else {
                            bestuurder = bestuurders[(int)r["bestuurderId"]];

                            if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
                            if (bestuurder.Voertuig != null) {
                                if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
                            }
                        }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }
        //public Dictionary<int, Bestuurder> GeefBestuurders(string naam, string voornaam) {
        //    Dictionary<int, Bestuurder> bestuurders = new();
        //    SqlConnection conn = new SqlConnection(_connString);
        //    string query = "SELECT * FROM Bestuurder LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId  LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
        //        cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));

        //        cmd.CommandText = query;

        //        cmd.Parameters["@naam"].Value = $"{naam}%";
        //        cmd.Parameters["@voornaam"].Value = $"{voornaam}%";

        //        conn.Open();
        //        Bestuurder bestuurder = null;
        //        Geslacht bestuurderGeslacht = Geslacht.M;
        //        Adres bestuurderAdres = null;
        //        Dictionary<int, Rijbewijs> bestuurderRijbewijzen = null;
        //        Voertuig bestuurderVoertuig = null;
        //        Dictionary<int, Brandstof> bestuurderVoertuigBrandstof = new Dictionary<int, Brandstof>();
        //        List<Brandstof> bestuurderTankkaartBrandstof = new List<Brandstof>();
        //        Tankkaart bestuurderTankkaart = null;
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            while (r.Read()) {
        //                if (bestuurder != null) {
        //                    if (!r.IsDBNull("rijbewijsId") && !bestuurderRijbewijzen.ContainsKey((int)r["rijbewijsId"])) { bestuurderRijbewijzen.Add((int)r["rijbewijsId"], new Rijbewijs((int)r["rijbewijsId"], (string)r[29])); }
        //                    if (!r.IsDBNull("brandstofId") && !bestuurderVoertuigBrandstof.ContainsKey((int)r["brandstofId"])) {
        //                        bestuurderVoertuigBrandstof.Add((int)r["brandstofId"], new Brandstof((int)r["brandstofId"], (string)r[33]));
        //                        bestuurder.Voertuig.ZetBrandstofTypeLijst(bestuurderVoertuigBrandstof.Values.ToList());
        //                    }
        //                }
        //                else {
        //                    if (!r.IsDBNull("adresId")) { bestuurderAdres = new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
        //                    if (!r.IsDBNull("rijbewijsId")) { bestuurderRijbewijzen = new Dictionary<int, Rijbewijs> { { (int)r["rijbewijsId"], new Rijbewijs((int)r["rijbewijsId"], (string)r[29]) } }; }
        //                    if (!r.IsDBNull("voertuigId")) {
        //                        bestuurderVoertuigBrandstof.Add((int)r["brandstofId"], new Brandstof((int)r["brandstofId"], (string)r[33]));
        //                        bestuurderVoertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], bestuurderVoertuigBrandstof.Values.ToList(), (string)r["type"], (string)r["kleur"], (int)r["deuren"]);
        //                    }
        //                    if (!r.IsDBNull("tankkaartId")) { bestuurderTankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], bestuurderTankkaartBrandstof, (bool)r["geblokkeerd"]); }
        //                    bestuurderGeslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
        //                    bestuurder = new((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], bestuurderGeslacht, bestuurderAdres, Convert.ToString(r["geboortedatum"]), (string)r["rijksregister"], bestuurderRijbewijzen.Values.ToList(), bestuurderVoertuig, bestuurderTankkaart);
        //                }
        //                bestuurders.Add(bestuurder.Id, bestuurder);
        //            }
        //            return bestuurders;
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }

        //    }
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurders() {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId;";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.CommandText = query;
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"])) && r["rijbewijsId"] != DBNull.Value) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurders(bool heeftVoertuig) {
        //    // !! BrandstofLijst is NULL in voertuig
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query;
        //    if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Voertuig.voertuigId IS NOT NULL;"; }
        //    else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Voertuig.voertuigId IS NULL;"; }
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.CommandText = query;
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = heeftVoertuig ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurder.ZetVoertuig(voertuig);

        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderTankkaarten() {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Tankkaart.tankkaartId IS NULL;";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.CommandText = query;
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaam(string naam, bool heeftVoertuig) {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query;
        //    if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voertuigId IS NOT NULL;"; }
        //    else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voertuigId IS NULL;"; }
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
        //        cmd.CommandText = query;
        //        cmd.Parameters["@naam"].Value = $"{naam}%";
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpVoornaam(string voornaam, bool heeftVoertuig) {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query;
        //    if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS NOT null;"; }
        //    else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS null;"; }
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
        //        cmd.CommandText = query;
        //        cmd.Parameters["@voornaam"].Value = $"{voornaam}%";
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpDatum(DateTime geboorte, bool heeftVoertuig) {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query;
        //    if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS NOT null;"; }
        //    else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS null;"; }
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Date));
        //        cmd.CommandText = query;
        //        cmd.Parameters["@date"].Value = geboorte;
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaam(string naam, string voornaam, bool heeftVoertuig) {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query;
        //    if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS NOT null;;"; }
        //    else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.voertuigId IS null;;"; }

        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
        //        cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
        //        cmd.CommandText = query;
        //        cmd.Parameters["@naam"].Value = $"{naam}%";
        //        cmd.Parameters["@voornaam"].Value = $"{voornaam}%";
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaamDatum(string naam, string voornaam, DateTime geboorte, bool heeftVoertuig) {
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query;
        //    if (heeftVoertuig) { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS NOT null;"; }
        //    else { query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.naam LIKE @naam AND Bestuurder.voornaam LIKE @voornaam AND Bestuurder.geboortedatum = @date AND Bestuurder.voertuigId IS null;"; }

        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
        //        cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
        //        cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Date));
        //        cmd.CommandText = query;
        //        cmd.Parameters["@naam"].Value = $"{naam}%";
        //        cmd.Parameters["@voornaam"].Value = $"{voornaam}%";
        //        cmd.Parameters["@date"].Value = geboorte;
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        //public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderVoertuig() {
        //    // !! BrandstofLijst is NULL in voertuig
        //    SqlConnection conn = new SqlConnection(_connString);
        //    Dictionary<int, Bestuurder> bestuurders = new Dictionary<int, Bestuurder>();
        //    string query = "SELECT TOP (20) * FROM Bestuurder LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId LEfT JOIN Brandstof_Voertuig ON Voertuig.voertuigId=Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof.brandstofId=Brandstof_Voertuig.brandstofId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId WHERE Bestuurder.voertuigId IS NULL;";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        cmd.CommandText = query;
        //        conn.Open();
        //        try {
        //            SqlDataReader r = cmd.ExecuteReader();
        //            Bestuurder bestuurder = null;
        //            while (r.Read()) {
        //                if (!bestuurders.ContainsKey((int)r["bestuurderId"])) {
        //                    List<Rijbewijs> rijbewijzen = r[12] != DBNull.Value ? new() { new Rijbewijs((int)r[11], (string)r[12]) } : new();
        //                    bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], (bool)r["geslacht"] ? Geslacht.M : Geslacht.V, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen);
        //                    Adres adres = r["adresId"] != DBNull.Value ? new((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]) : null;
        //                    Tankkaart tankkaart = r["tankkaartId"] != DBNull.Value ? new((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]) : null;
        //                    Voertuig voertuig = r["voertuigId"] != DBNull.Value ? new((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], new List<Brandstof> { new Brandstof((int)r[28], (string)r[29]) }, (string)r["type"], (string)r["kleur"], (int)r["deuren"], bestuurder) : null;


        //                    if (tankkaart != null) tankkaart.ZetBestuurder(bestuurder);
        //                    bestuurder.ZetAdres(adres);
        //                    bestuurder.ZetVoertuig(voertuig);
        //                    bestuurder.ZetTankkaart(tankkaart);
        //                    bestuurders.Add(bestuurder.Id, bestuurder);
        //                }
        //                else {
        //                    bestuurder = bestuurders[(int)r["bestuurderId"]];

        //                    if (!bestuurder.Rijbewijzen.Any(e => e.Id.Equals((int)r["rijbewijsId"]))) { bestuurder.Rijbewijzen.Add(new Rijbewijs((int)r[11], (string)r[12])); }
        //                    if (bestuurder.Voertuig != null) {
        //                        if (!bestuurder.Voertuig.Brandstof.Any(e => e.Id.Equals((int)r[28]))) { bestuurder.Voertuig.voegBrandstofToe(new Brandstof((int)r[28], (string)r[29])); }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //        finally { conn.Close(); }
        //    }
        //    return bestuurders;
        //}
        #endregion
    }
}
