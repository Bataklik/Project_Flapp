using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class BestuurderRepo : IBestuurderRepo
    {
        private string _connString;
        public BestuurderRepo(string connString)
        {
            _connString = connString;
        }

        #region BestaatBestuurder Method
        public bool BestaatBestuurder(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Bestuurder WHERE bestuurder_naam = @naam AND bestuurder_voornaam = @voornaam AND bestuurder_geboortedatum = @geboorte AND bestuurder_rijksregister = @rijksregister AND " +
                "AND bestuurder_adres_id = @adres_id AND bestuurder_tankkaart_id = @tankkaart_id AND bestuurder_geslacht = @geslacht;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@adres_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaart_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                    cmd.Parameters["@adres_id"].Value = b.Adres.Id;
                    cmd.Parameters["@tankkaart_id"].Value = b.Tankkaart.Kaartnummer;
                    cmd.Parameters["@geslacht"].Value = b.Geslacht;

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool BestaatBestuurderId(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Bestuurder WHERE bestuurderId = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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
        #endregion

        #region VoegBestuurderToe Method
        public void VoegBestuurderToe(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB] INSERT INTO [dbo].[Bestuurder] ([bestuurder_naam] ,[bestuurder_voornaam] ,[bestuurder_geboortedatum] ,[bestuurder_rijksregister] ,[bestuurder_adres_id] ,[bestuurder_tankkaart_id] ,[bestuurder_geslacht]) VALUES (@naam ,@voornaam ,@geboorte ,@rijksregister ,@adres ,@tankkaart,@geslacht)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaart", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                    cmd.Parameters["@adres"].Value = b.Adres.Id;
                    cmd.Parameters["@tankkaart"].Value = b.Tankkaart.Kaartnummer;
                    if (b.Geslacht == Geslacht.M) { cmd.Parameters["@geslacht"].Value = 1; }
                    else { cmd.Parameters["@geslacht"].Value = 0; }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region UpdateBestuurder Method
        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VerwijderBestuurder Method
        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GeefBestuurder Method
        public Bestuurder GeefBestuurder(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Bestuurder WHERE bestuurder_naam = @naam AND bestuurder_voornaam = @voornaam AND bestuurder_geboortedatum = @geboorte AND bestuurder_rijksregister = @rijksregister AND " +
                "AND bestuurder_adres_id = @adres_id AND bestuurder_tankkaart_id = @tankkaart_id AND bestuurder_geslacht = @geslacht";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@adres_id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@tankkaart_id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                cmd.CommandText = query;

                cmd.Parameters["@naam"].Value = b.Naam;
                cmd.Parameters["@voornaam"].Value = b.Voornaam;
                cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                cmd.Parameters["@adres_id"].Value = b.Adres.Id;
                cmd.Parameters["@tankkaart_id"].Value = b.Tankkaart.Kaartnummer;
                cmd.Parameters["@geslacht"].Value = b.Geslacht;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Geslacht g = (int)r["geslacht"] == 1 ? Geslacht.M : Geslacht.V;
                    Adres a = null;// _aRepo.GeefAdres((int)r["adres_id"]);
                    string geboorte = Convert.ToString(r["geboortedatum"]);
                    List<Rijbewijs> rt = new();
                    Tankkaart t = null; //_tRepo.GeefTankkaart((int)r["tankkaart_id"]);
                    Bestuurder gevondenBestuurder = new((int)r["id"], (string)r["naam"], (string)r["voornaam"], g, a, geboorte, (string)r["rijksregister"], rt, null, t);
                    return gevondenBestuurder;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally { conn.Close(); }

            }
        }
        public Bestuurder GeefBestuurder(int bId)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Bestuurder WHERE id = @id";
            using (SqlCommand cmd = conn.CreateCommand())
            {

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                cmd.CommandText = query;

                cmd.Parameters["@id"].Value = bId;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Geslacht g = (int)r["geslacht"] == 1 ? Geslacht.M : Geslacht.V;
                    Adres a = null;//_aRepo.GeefAdres((int)r["adres_id"]);
                    string geboorte = Convert.ToString(r["geboortedatum"]);
                    // Repo's & Interfaces moeten nog gemaakt worden
                    List<Rijbewijs> rt = new(); //_rtRepo.GeefRijbewijs((int)r["rijbewijstype_id"]);
                    Voertuig v = null;//_vRepo.GeefVoertuig((int)r["voertuig_id"]);
                    Tankkaart t = null; //_tRepo.GeefTankkaart((int)r["tankkaart_id"]);
                    Bestuurder gevondenBestuurder = new((int)r["id"], (string)r["naam"], (string)r["voornaam"], g, a, geboorte, (string)r["rijksregister"], rt, v, t);
                    return gevondenBestuurder;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefAlleBestuurders Methodbe
        public IReadOnlyList<Bestuurder> GeefAlleBestuurders()
        {
            SqlConnection conn = new SqlConnection(_connString);
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            string query = "SELECT * FROM [Project_Flapp_DB].[dbo].[Bestuurder] INNER JOIN Rijbewijs_Bestuurder ON Bestuurder.bestuurderId = Rijbewijs_Bestuurder.bestuurderId INNER JOIN Rijbewijs ON Rijbewijs_Bestuurder.rijbewijsId = Rijbewijs.rijbewijsId INNER JOIN Adres ON Bestuurder.adresId = Adres.adresId INNER JOIN Voertuig ON Bestuurder.voertuigId = Voertuig.voertuigId INNER JOIN Tankkaart ON Bestuurder.tankkaartId = Tankkaart.tankkaartId INNER JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId INNER JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        // Bestuurder(int id, string naam, string voornaam, Geslacht geslacht, Adres adres, string geboortedatum, string rijksregisternummer, List<Rijbewijs> rijbewijs, Voertuig voertuig, Tankkaart tankkaart)
                        Adres adres = new Adres((int)r["adresId"], (string)r["straat"], (string)r["huisnummer"], (string)r["stad"], (int)r["postcode"]);
                        Geslacht geslacht = (bool)r["geslacht"] ? Geslacht.M : Geslacht.V;
                        List<Rijbewijs> rijbewijzen = new List<Rijbewijs> { new Rijbewijs(r[12].ToString()) };
                        Brandstof brandstof = new Brandstof(Convert.ToInt32(r[32]), r[33].ToString());
                        Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstof, (string)r["type"], (string)r["kleur"], (int)r["deuren"]);

                        Tankkaart tankkaart = new Tankkaart((int)r["tankkaartId"], (DateTime)r["geldigheidsdatum"], (string)r["pincode"], new Brandstof("NIETS"), (bool)r["geblokkeerd"]);


                        Bestuurder bestuurder = new Bestuurder((int)r["bestuurderId"], (string)r["naam"], (string)r["voornaam"], geslacht, adres, Convert.ToDateTime(r["geboortedatum"]).ToString("dd/MM/yyyy"), (string)r["rijksregister"], rijbewijzen, voertuig, tankkaart);
                        tankkaart.ZetBestuurder(bestuurder);
                        bestuurders.Add(bestuurder);
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return bestuurders;
        }

        public IReadOnlyList<Bestuurder> GeefAlleBestuurdersZonderTankkaarten()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
