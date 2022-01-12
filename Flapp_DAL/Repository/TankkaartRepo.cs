using Flapp_BLL.Exceptions.ModelExpections;
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

        #region Bestaat
        public bool BestaatTankkaart(Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT 1 FROM Tankkaart WHERE tankkaartid = @tankkaartid AND geldigheidsdatum = @geldigheidsdatum";
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
            string query = "SELECT 1 FROM Tankkaart WHERE tankkaartId = @tankkaartId;";
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
        #endregion

        #region Crud
        public Dictionary<int, Tankkaart> GeefAlleTankkaarten() {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT * FROM Tankkaart LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId;";
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
        public Dictionary<int, Tankkaart> GeefTankkaarten(int? kaartnummer, DateTime? datum, int? bestuurderid, string naam, string voornaam, DateTime? geboortedatum, string rijksregister, bool strikt = true) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query = "SELECT * FROM Tankkaart LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE ";
            bool AND = false;
            bool naamNull = true;
            if (!string.IsNullOrWhiteSpace(naam)) {
                naamNull = false;
                AND = true;
                if (strikt) query += " Bestuurder.naam=@naam";
                else query += " UPPER(naam)=UPPER(@naam)";
            }
            bool voornaamNull = true;
            if (!string.IsNullOrWhiteSpace(voornaam)) {
                voornaamNull = false;
                if (AND) query += " AND "; else AND = false;
                if (strikt) query += " voornaam=@voornaam";
                else query += " UPPER(voornaam)=UPPER(@voornaam) ";
            }
            bool rijksregisterNull = true;
            if (!string.IsNullOrWhiteSpace(rijksregister)) {
                rijksregisterNull = false;
                if (AND) query += " AND "; else AND = false;
                if (strikt) query += " rijksregister=@rijksregister";
                else query += " UPPER(rijksregister)=UPPER(@rijksregister) ";
            }
            bool kaartnummerNull = true;
            if (kaartnummer != null) {
                kaartnummerNull = false;
                if (AND) query += " AND "; else AND = false;
                query += " Tankkaart.tankkaartId=@tankkaartId";
            }
            bool bestuurderidNull = true;
            if (bestuurderid != null) {
                bestuurderidNull = false;
                if (AND) query += " AND "; else AND = false;
                query += " Bestuurder.bestuurderId=@bestuurderId";
            }
            bool datumNull = true;
            if (datum != null) {
                datumNull = false;
                if (AND) query += " AND "; else AND = false;
                query += " geldigheidsdatum=@geldigheidsdatum";
            }
            bool geboortedatumNull = true;
            if (geboortedatum != null) {
                geboortedatumNull = false;
                if (AND) query += " AND "; else AND = false;
                query += " geboortedatum=@geboortedatum";
            }

            using (SqlCommand cmd = conn.CreateCommand()) {

                try {
                    conn.Open();
                    if (!naamNull) { cmd.Parameters.Add("@naam", SqlDbType.NVarChar); cmd.Parameters["@naam"].Value = naam; }
                    if (!voornaamNull) { cmd.Parameters.Add("@voornaam", SqlDbType.NVarChar); cmd.Parameters["@voornaam"].Value = voornaam; }
                    if (!rijksregisterNull) { cmd.Parameters.Add("@rijksregister", SqlDbType.NVarChar); cmd.Parameters["@rijksregister"].Value = rijksregister; }
                    if (!kaartnummerNull) { cmd.Parameters.Add("@tankkaartId", SqlDbType.Int); cmd.Parameters["@tankkaartId"].Value = kaartnummer; }
                    if (!bestuurderidNull) { cmd.Parameters.Add("@bestuurderId", SqlDbType.Int); cmd.Parameters["@bestuurderId"].Value = bestuurderid; }
                    if (!datumNull) { cmd.Parameters.Add("@geldigheidsdatum", SqlDbType.DateTime); cmd.Parameters["@geldigheidsdatum"].Value = datum; }
                    if (!geboortedatumNull) { cmd.Parameters.Add("@geboortedatum", SqlDbType.DateTime); cmd.Parameters["@geboortedatum"].Value = geboortedatum; }
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
        public Dictionary<int, Tankkaart> GeefAlleTankkaartenZonderBestuurder(DateTime? startDt, DateTime? endDt) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Tankkaart> tankkaarten = new Dictionary<int, Tankkaart>();
            string query;
            if (startDt == null && endDt == null) { query = "SELECT TOP(20) * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE Bestuurder.tankkaartId is NULL;"; }
            else { query = "SELECT TOP(20) * FROM [dbo].[Tankkaart] LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE Bestuurder.tankkaartId is NULL AND geldigheidsdatum >= @start AND geldigheidsdatum <= @end;"; }
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    if (startDt != null && endDt != null) {
                        cmd.Parameters.Add(new SqlParameter("@start", SqlDbType.Date));
                        cmd.Parameters.Add(new SqlParameter("@end", SqlDbType.Date));
                        cmd.CommandText = query;
                        cmd.Parameters["@start"].Value = startDt.Value.Date;
                        cmd.Parameters["@end"].Value = endDt.Value.Date;
                    }
                    else {
                        cmd.CommandText = query;
                    }
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
            string query = "SELECT * FROM Tankkaart LEFT JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId LEFT JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Tankkaart.tankkaartId=Bestuurder.tankkaartId LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId WHERE Tankkaart.tankkaartId = @tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));

                cmd.CommandText = query;

                cmd.Parameters["@tankkaartId"].Value = kaartnr;
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    

                    Tankkaart tankkaart = null;
                    while (r.Read()) {
                        if (!tankkaart.Brandstoffen.Contains(new Brandstof((string)r["naam"]))) {
                            tankkaart.Brandstoffen.Add(new Brandstof((string)r["naam"]));
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
                            tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], brandstof, b, (bool)r["geblokkeerd"]);
                        }
                    }
                    return tankkaart;

                }
                catch (Exception ex) { throw new Exception("TankkaartRepo", ex); }
                finally { conn.Close(); }
            }
        }
        public int VoegTankkaartToe(Tankkaart t) {
            int kaartnummer;
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "INSERT INTO Tankkaart ([geldigheidsdatum], [pincode], [geblokkeerd]) output INSERTED.tankkaartId VALUES (@geldigheidsdatum, @pincode, @geblokkeerd);";

                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = t.Pincode;
                    if (t.Geblokkeerd) cmd.Parameters["@geblokkeerd"].Value = 1;
                    cmd.Parameters["@geblokkeerd"].Value = 0;
                    kaartnummer = (int)cmd.ExecuteScalar();

                    if (t.Bestuurder != null) {
                        cmd.CommandText = "UPDATE Bestuurder SET tankkaartId=@tankkaartId WHERE bestuurderId=@bestuurderId;";
                        cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                        cmd.Parameters["@bestuurderId"].Value = t.Bestuurder.Id;
                        cmd.Parameters["@tankkaartId"].Value = kaartnummer;
                        cmd.ExecuteNonQuery();
                    }

                    t.Brandstoffen.ForEach(x => {
                        cmd.CommandText = $"INSERT INTO Brandstof_Tankkaart ([brandstofId] ,[tankkaartId]) VALUES(@brandstofId{x.Id}, @tankkaartId{x.Id + 1});";
                        cmd.Parameters.Add(new SqlParameter($"@brandstofId{x.Id}", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter($"@tankkaartId{x.Id + 1}", SqlDbType.Int));
                        cmd.Parameters[$"@brandstofId{x.Id}"].Value = x.Id;
                        cmd.Parameters[$"@tankkaartId{x.Id + 1}"].Value = kaartnummer;
                        cmd.ExecuteNonQuery();
                    });

                    trx.Commit();
                    return kaartnummer;
                }
                catch (Exception ex) {
                    trx.Rollback();
                    throw new Exception(ex.Message);
                }
                finally { conn.Close(); }
            }
        }
        public void UpdateTankkaart(Tankkaart t) {
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "UPDATE Tankkaart SET geldigheidsdatum = @geldigheidsdatum, pincode = @pincode, geblokkeerd = @geblokkeerd WHERE tankkaartId = @tankkaartId;";

                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.Bit));
                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.Parameters["@geldigheidsdatum"].Value = t.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = t.Pincode;
                    if (t.Geblokkeerd) cmd.Parameters["@geblokkeerd"].Value = 1;
                    cmd.Parameters["@geblokkeerd"].Value = 0;
                    cmd.ExecuteNonQuery();

                    if (t.Bestuurder == null) {
                        cmd.CommandText = "UPDATE Bestuurder SET tankkaartId=NULL WHERE bestuurderId=@bestuurderId;";
                        cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));
                        cmd.Parameters["@bestuurderId"].Value = t.Bestuurder.Id;
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "UPDATE Bestuurder SET tankkaartId=@tankkaartId WHERE bestuurderId=@bestuurderId;";
                    cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));
                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.Parameters["@bestuurderId"].Value = t.Bestuurder.Id;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Brandstof_Tankkaart WHERE tankkaartId=@tankkaartId;";
                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;
                    cmd.ExecuteNonQuery();

                    t.Brandstoffen.ForEach(x => {
                        cmd.CommandText = $"INSERT INTO Brandstof_Tankkaart ([brandstofId] ,[tankkaartId]) VALUES(@brandstofId{x.Id}, @tankkaartId{x.Id + 1});";
                        cmd.Parameters.Add(new SqlParameter($"@brandstofId{x.Id}", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter($"@tankkaartId{x.Id + 1}", SqlDbType.Int));
                        cmd.Parameters[$"@brandstofId{x.Id}"].Value = x.Id;
                        cmd.Parameters[$"@tankkaartId{x.Id + 1}"].Value = t.Kaartnummer;
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
        public void VerwijderTankkaart(Tankkaart t) {
            using (SqlConnection conn = new SqlConnection(_connString)) {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction trx;

                trx = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trx;

                try {
                    cmd.CommandText = "DELETE FROM Brandstof_Tankkaart WHERE tankkaartid=@id1;";
                    cmd.Parameters.AddWithValue("@id1", t.Kaartnummer);
                    cmd.ExecuteNonQuery();

                    if (t.Bestuurder != null) {
                        cmd.CommandText = "UPDATE Bestuurder SET tankkaartId=NULL WHERE tankkaartId=@id3;";
                        cmd.Parameters.AddWithValue("@id3", t.Kaartnummer);
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "DELETE FROM Tankkaart WHERE tankkaartid=@id2;";
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
        #endregion

    }
}
