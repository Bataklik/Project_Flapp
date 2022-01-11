using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository {
    public class VoertuigRepo : IVoertuigRepo {
        private BestuurderRepo _bRepo;
        private string _connString;

        public VoertuigRepo(string connString) {
            _connString = connString;
            _bRepo = new BestuurderRepo(connString);
        }

        #region Zoekvoertuig Method
        public Dictionary<int, Voertuig> ZoekVoertuig(string merk, string model, string nplaat) {
            Dictionary<int, Voertuig> voertuigen = new Dictionary<int, Voertuig>();
            List<string> subquerylist = new List<string>();
            int numberofparams = 0;
            bool merkIsNull = true;
            if (!String.IsNullOrWhiteSpace(merk)) {
                merkIsNull = false;
                if (numberofparams > 0) {
                    subquerylist.Add(" AND ");
                }
                numberofparams++;
                subquerylist.Add("merk=@merk");
            }
            bool modelisNull = true;
            if (!String.IsNullOrWhiteSpace(model)) {
                modelisNull = false;
                if (numberofparams > 0) {
                    subquerylist.Add(" AND ");
                }
                numberofparams++;
                subquerylist.Add("model=@model");
            }
            bool nummerplaatIssNull = true;
            if (!String.IsNullOrWhiteSpace(nplaat)) {
                nummerplaatIssNull = false;
                if (numberofparams > 0) {
                    subquerylist.Add(" AND ");
                }
                numberofparams++;
                subquerylist.Add("nummerplaat=@nummerplaat");

            }

            string query = $"SELECT * FROM Voertuig LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId WHERE {String.Join("", subquerylist)}";

            SqlConnection cn = new SqlConnection(_connString);
            using (SqlCommand cmd = cn.CreateCommand()) {
                cn.Open();
                try {
                    if (!merkIsNull) {
                        cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                        cmd.Parameters["@merk"].Value = merk;
                    }
                    if (!modelisNull) {
                        cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                        cmd.Parameters["@model"].Value = model;
                    }
                    if (!nummerplaatIssNull) {
                        cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                        cmd.Parameters["@nummerplaat"].Value = nplaat;
                    }
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        if (voertuigen.ContainsKey((int)reader["voertuigId"])) {
                            Voertuig dicVoertuig = voertuigen[(int)reader["voertuigId"]];
                            dicVoertuig.Brandstof.Add(new Brandstof(reader["naam"].ToString()));
                        }
                        else {
                            int voertuigId = (int)reader["voertuigId"];
                            string merkr = (string)reader["merk"];
                            string modelr = (string)reader["model"];
                            string cnummer = (string)reader["chassisNummer"];
                            string nplaatr = (string)reader["nummerplaat"];
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)reader["naam"]) };
                            string type = (string)reader["type"];
                            string kleur = (string)reader["kleur"];
                            int deuren = (int)reader["deuren"];
                            Voertuig v = new Voertuig(voertuigId, merkr, modelr, cnummer, nplaatr, brandstof, type, kleur, deuren);
                            voertuigen.Add(v.VoertuigID, v);
                        }

                    }
                    return voertuigen;
                }
                catch (Exception ex) {

                    throw new Exception(ex.Message);
                }
                finally {
                    cn.Close();
                }
            }
        }
        public Dictionary<int, Voertuig> ZoekVoertuigen(string merk, string model, string nummerplaat) {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Voertuig> voertuigen = new Dictionary<int, Voertuig>();
            string query = "SELECT TOP(20) * FROM [Project_Flapp_DB].[dbo].[Voertuig] LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId WHERE merk LIKE @merk AND model LIKE @model AND nummerplaat LIKE @nummerplaat";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.VarChar));


                cmd.CommandText = query;
                cmd.Parameters["@merk"].Value = $"{merk}%";
                cmd.Parameters["@model"].Value = $"{model}%";
                cmd.Parameters["@nummerplaat"].Value = $"{nummerplaat}%";


                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (!voertuigen.ContainsKey((int)r["voertuigId"])) {
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                            voertuigen.Add((int)r["voertuigId"], new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstof, (string)r["type"], (string)r["kleur"], (int)r["deuren"]));
                        }
                        else { voertuigen[(int)r["voertuigId"]].Brandstof.Add(new Brandstof((string)r["naam"])); }
                    }
                    return voertuigen;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region BestaatVoertuig Method
        public bool BestaatVoertuig(Voertuig v) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT [voertuigid] ,[merk] ,[model] ,[chassisnummer] ,[nummerplaat] ,[type] ,[kleur] ,[deuren] FROM [Project_Flapp_DB].[dbo].[voertuig] WHERE merk = @merk AND model = @model AND chassisnummer = @chassisnummer AND nummerplaat = @nummerplaat AND type = @type AND kleur = @kleur AND deuren = @deuren";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.VarChar));

                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.VarChar));
                    //cmd.Parameters.Add(new SqlParameter("@brandstof_type", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                    //cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@merk"].Value = v.Merk;
                    cmd.Parameters["@model"].Value = v.Model;

                    cmd.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = v.Nummerplaat;
                    //cmd.Parameters["@brandstof_type"].Value = v.Brandstof;
                    cmd.Parameters["@type"].Value = v.VoertuigType;

                    cmd.Parameters["@kleur"].Value = v.Kleur;
                    cmd.Parameters["@deuren"].Value = v.Aantaldeuren;
                    //cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;


                    int VoertuigBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (VoertuigBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefVoertuig(en) Method
        public Dictionary<int, Voertuig> GeefVoertuigen() {
            SqlConnection conn = new SqlConnection(_connString);
            Dictionary<int, Voertuig> voertuigen = new Dictionary<int, Voertuig>();
            string query = "SELECT TOP(20) * FROM Voertuig LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId LEFT JOIN Bestuurder ON Voertuig.voertuigId = Bestuurder.voertuigId LEFT JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId LEFT JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId  LEFT JOIN Adres ON Bestuurder.adresId = Adres.adresId LEFT JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        if (voertuigen.ContainsKey((int)r["voertuigId"])) {
                            Voertuig dicVoertuig = voertuigen[(int)r["voertuigId"]];
                            dicVoertuig.Brandstof.Add(new Brandstof(r["naam"].ToString()));
                        }
                        else {
                            //List<Brandstof> brandstof =  geefbrandstoffenVanVoertuig((int)r["voertuigId"]);
                            Bestuurder bestuurder;
                            List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                            string voertuigtype = (string)r["type"];
                            Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstof, voertuigtype, (string)r["kleur"], (int)r["deuren"]);
                            Adres adres = null;
                            if (!r.IsDBNull(r.GetOrdinal("adresId")) && !r.IsDBNull(r.GetOrdinal("straat")) && !r.IsDBNull(r.GetOrdinal("huisnummer")) && !r.IsDBNull(r.GetOrdinal("stad")) && !r.IsDBNull(r.GetOrdinal("postcode"))) { adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]); }
                            Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                            List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                            bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r[14], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, null, null);
                            if (!r.IsDBNull(r.GetOrdinal("voertuigId"))) {
                                string naam = r[29].ToString();
                                List<Brandstof> brandstoffen = new List<Brandstof> { new Brandstof(naam) };
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            if (!r.IsDBNull(r.GetOrdinal("tankkaartId"))) {
                                Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], (bool)r["geblokkeerd"]);
                                if (DBNull.Value != r[29]) { tankkaart.Brandstoffen.Add(new Brandstof(r[29].ToString())); ; }
                                bestuurder.ZetTankkaart(tankkaart);
                            }
                            voertuig.ZetBestuurder(bestuurder);

                            voertuigen.Add(voertuig.VoertuigID, voertuig);
                        }

                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return voertuigen;
        }

        public Voertuig GeefVoertuigDoorID(int vId) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM Voertuig LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId WHERE Voertuig.voertuigId = @voertuigId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@voertuigId", SqlDbType.Int));
                cmd.CommandText = query;
                cmd.Parameters["@voertuigId"].Value = vId;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    List<Brandstof> b = new List<Brandstof> { new Brandstof((string)r["naam"]) };//_bRepo.GeefBrandstof((int)r["brandstof_id"]); // Mag null zijn
                    Bestuurder bs = null;// _bsRepo.GeefBestuurder((int)r["bestuurder_id"]); // Mag null zijn
                    string vt = (string)r["type"];
                    Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], b, vt, (string)r["kleur"], (int)r["deuren"], bs);
                    return voertuig;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }

            }
        }
        #endregion

        #region VoegVoertuigToe Method
        public int VoegVoertuigToe(Voertuig v) {
            //int voertuigId;
            //var brandstoffen = v.geefBrandstoffen();
            string query = "INSERT INTO [dbo].[Voertuig] (merk, model, chassisnummer, nummerplaat, type, kleur, deuren)output INSERTED.voertuigId VALUES (@merk, @model, @chassisnummer, @nummerplaat, @type, @kleur, @deuren)";
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = new(query, conn);
            try {
                conn.Open();

                cmd.Parameters.AddWithValue("@merk", v.Merk);
                cmd.Parameters.AddWithValue("@model", v.Model);
                cmd.Parameters.AddWithValue("@chassisnummer", v.ChassisNummer);
                cmd.Parameters.AddWithValue("@nummerplaat", v.Nummerplaat);
                cmd.Parameters.AddWithValue("@type", v.VoertuigType);
                cmd.Parameters.AddWithValue("@kleur", v.Kleur);
                cmd.Parameters.AddWithValue("@deuren", v.Aantaldeuren);

                //command.ExecuteNonQuery();
                int voertuigId = (int)cmd.ExecuteScalar();
                //int bestuurderId = (int)command.ExecuteScalar();
                return voertuigId;
            }
            catch (Exception ex) { throw new VoertuigException(ex.Message); }
            finally { conn.Close(); }
        }
        #endregion

        #region UpdateVoertuig Method
        public void UpdateVoertuig(Voertuig v) {
            var brandstoffen = v.geefBrandstoffen();
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Voertuig] SET merk = @merk , model = @model" +
                ", chassisnummer = @chassisnummer , nummerplaat = @nummerplaat , type = @type" +
                ", kleur = @kleur , deuren = @deuren WHERE voertuigId = @voertuigId;";
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand command = new(query, conn);
            conn.Open();

            try {
                command.Parameters.Add(new SqlParameter("@voertuigId", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                //cmd.Parameters.Add(new SqlParameter("@brandstof_type", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                //cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));
                command.CommandText = query;
                command.Parameters["@voertuigid"].Value = v.VoertuigID;
                command.Parameters["@merk"].Value = v.Merk;
                command.Parameters["@model"].Value = v.Model;
                command.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                command.Parameters["@nummerplaat"].Value = v.Nummerplaat;
                //cmd.Parameters["@brandstof_type"].Value = v.Brandstof.Id;
                command.Parameters["@type"].Value = v.VoertuigType;
                command.Parameters["@kleur"].Value = v.Kleur;
                command.Parameters["@deuren"].Value = v.Aantaldeuren;
                //cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { conn.Close(); }

        }
        #endregion

        #region VerwijderVoertuig Method
        public void VerwijderVoertuig(Voertuig v) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Voertuig] WHERE voertuigId = @voertuigId;";
            string query2 = "DELETE FROM [dbo].[Brandstof_Voertuig] WHERE voertuigID = @voertuigId";
            SqlCommand command = new(query, conn);
            SqlCommand command2 = new(query2, conn);
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;
            command2.Transaction = transaction;
            try {
                command2.Parameters.Add(new SqlParameter("@voertuigId", SqlDbType.Int));
                command2.CommandText = query2;
                command2.Parameters["@voertuigId"].Value = v.VoertuigID;
                command2.ExecuteNonQuery();
                command.Parameters.Add(new SqlParameter("@voertuigId", SqlDbType.Int));
                command.CommandText = query;
                command.Parameters["@voertuigId"].Value = v.VoertuigID;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex) { transaction.Rollback(); throw new Exception(ex.Message); }
            finally { conn.Close(); }

        }
        #endregion

        #region GeefMerk Method
        public IReadOnlyList<string> GeefMerken() {
            SqlConnection conn = new SqlConnection(_connString);
            List<string> merken = new List<string>();
            string query = "SELECT DISTINCT merk FROM [dbo].[Voertuig]";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        string merknaam = (string)r["merk"];
                        //Brandstof brandstof = new Brandstof(id, brandstofnaam);
                        merken.Add(merknaam);
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return merken;

        }
        #endregion

        #region GeefModellen Method
        public IReadOnlyList<string> GeefModellen(string merk) {
            List<string> modellen = new();
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT DISTINCT model FROM Voertuig WHERE merk = @merk ORDER BY model";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                cmd.CommandText = query;

                cmd.Parameters["@merk"].Value = merk;
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        string model = (string)r["model"];
                        modellen.Add(model);
                    }
                    return modellen;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }

            }
        }
        #endregion
    }
}
