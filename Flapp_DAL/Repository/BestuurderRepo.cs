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
            throw new NotImplementedException();
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
                    List<RijbewijsType> rt = new();
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
                    List<RijbewijsType> rt = new(); //_rtRepo.GeefRijbewijs((int)r["rijbewijstype_id"]);
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
            IList<Bestuurder> alleBestuurders = new List<Bestuurder>();
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Bestuurder;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        // Bestuurder aanmaken en toevoegen aan list
                        // alleBestuurders.Add(new());
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return (IReadOnlyList<Bestuurder>)alleBestuurders;
        }

        public IReadOnlyList<Bestuurder> GeefAlleBestuurdersZonderTankkaarten()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
