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
        private TankkaartRepo _tRepo;
        private VoertuigRepo _vRepo;
        private AdresRepo _aRepo;
        private RijbewijsTypeRepo _rtRepo;
        private string _connString;

        public BestuurderRepo(string connString)
        {
            _connString = connString;
            _aRepo = new AdresRepo(connString);
            _rtRepo = new RijbewijsTypeRepo(connString);
            _vRepo = new VoertuigRepo(connString);
            _tRepo = new TankkaartRepo(connString);
        }

        #region BestaatBestuurder Method
        public bool BestaatBestuurder(Bestuurder b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Bestuurder WHERE naam = @naam AND voornaam = @voornaam AND geboortedatum = @geboorte AND rijksregister = @rijksregister AND rijbewijstype_id = @rijbewijstype_id " +
                "AND adres_id = @adres_id AND voertuig_id = @voertuig_id AND tankkaart_id = @tankkaart_id AND geslacht = @geslacht;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@rijbewijstype_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@adres_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@voertuig_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaart_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                    cmd.Parameters["@rijbewijstype_id"].Value = 1;
                    cmd.Parameters["@adres_id"].Value = b.Adres.Id;
                    if (b.Voertuig == null) { cmd.Parameters["@voertuig_id"].Value = 1; }
                    else { cmd.Parameters["@voertuig_id"].Value = b.Voertuig.VoertuigID; }
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
            string query = "USE [Project_Flapp_DB] INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[rijbewijstype_id] ,[adres_id] ,[voertuig_id] ,[tankkaart_id] ,[geslacht]) VALUES (@naam ,@voornaam ,@geboorte ,@rijksregister ,@rijbewijs ,@adres ,@voertuig ,@tankkaart,@geslacht)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@rijbewijs", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@voertuig", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaart", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));



                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = b.Naam;
                    cmd.Parameters["@voornaam"].Value = b.Voornaam;
                    cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                    cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                    // Rijbewijs type moet een ID zijn
                    cmd.Parameters["@rijbewijs"].Value = 1;
                    // Adres moet een ID zijn
                    cmd.Parameters["@adres"].Value = b.Adres.Id;
                    // Voertuig moet een ID zijn
                    cmd.Parameters["@voertuig"].Value = b.Voertuig.VoertuigID;
                    // Tankkaart moet een ID zijn
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
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Bestuurder WHERE naam = @naam AND voornaam = @voornaam AND geboortedatum = @geboorte AND rijksregister = @rijksregister AND rijbewijstype_id = @rijbewijstype_id " +
                "AND adres_id = @adres_id AND voertuig_id = @voertuig_id AND tankkaart_id = @tankkaart_id AND geslacht = @geslacht";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@rijbewijstype_id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@adres_id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@voertuig_id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@tankkaart_id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@geslacht", SqlDbType.Bit));

                cmd.CommandText = query;

                cmd.Parameters["@naam"].Value = b.Naam;
                cmd.Parameters["@voornaam"].Value = b.Voornaam;
                cmd.Parameters["@geboorte"].Value = b.Geboortedatum;
                cmd.Parameters["@rijksregister"].Value = b.Rijksregisternummer;
                cmd.Parameters["@rijbewijstype_id"].Value = 1;
                cmd.Parameters["@adres_id"].Value = b.Adres.Id;
                if (b.Voertuig == null) { cmd.Parameters["@voertuig_id"].Value = 1; }
                else { cmd.Parameters["@voertuig_id"].Value = b.Voertuig.VoertuigID; }
                cmd.Parameters["@tankkaart_id"].Value = b.Tankkaart.Kaartnummer;
                cmd.Parameters["@geslacht"].Value = b.Geslacht;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Geslacht g = (int)r["geslacht"] == 1 ? Geslacht.M : Geslacht.V;
                    Adres a = _aRepo.GeefAdres((int)r["adres_id"]);
                    string geboorte = Convert.ToString(r["geboortedatum"]);
                    // Repo's & Interfaces moeten nog gemaakt worden
                    RijbewijsType rt = _rtRepo.GeefRijbewijs((int)r["rijbewijstype_id"]);
                    Voertuig v = _vRepo.GeefVoertuig((int)r["voertuig_id"]);
                    Tankkaart t = _tRepo.GeefTankkaart((int)r["tankkaart_id"]);
                    Bestuurder gevondenBestuurder = new((int)r["id"], (string)r["naam"], (string)r["voornaam"], g, a, geboorte, (string)r["rijksregister"], rt, v, t);
                    return gevondenBestuurder;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
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
                    Adres a = _aRepo.GeefAdres((int)r["adres_id"]);
                    string geboorte = Convert.ToString(r["geboortedatum"]);
                    // Repo's & Interfaces moeten nog gemaakt worden
                    RijbewijsType rt = _rtRepo.GeefRijbewijs((int)r["rijbewijstype_id"]);
                    Voertuig v = _vRepo.GeefVoertuig((int)r["voertuig_id"]);
                    Tankkaart t = _tRepo.GeefTankkaart((int)r["tankkaart_id"]);
                    Bestuurder gevondenBestuurder = new((int)r["id"], (string)r["naam"], (string)r["voornaam"], g, a, geboorte, (string)r["rijksregister"], rt, v, t);
                    return gevondenBestuurder;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefAlleBestuurders Method
        public IReadOnlyList<Bestuurder> GeefAlleBestuurders()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
