using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class VoertuigRepo : IVoertuigRepo
    {
        private BestuurderRepo _bRepo;
        private string _connString;

        public VoertuigRepo(string connString)
        {
            _connString = connString;
            _bRepo = new BestuurderRepo(connString);
        }

        #region zoekvoertuigen
        public IReadOnlyList<Voertuig> ZoekVoertuig(int? id, string merk, string model, string chassisNummer, string Nummerplaat, Brandstof brandstoftype, string voertuigType, string kleur, int deuren, Bestuurder bestuurder)
        {
            SqlConnection conn = new SqlConnection(_connString);
            List<Voertuig> voertuigen = new List<Voertuig>();
            string query = "SELECT * FROM vehicle WHERE merk=@merk,model=@model,chassisnummer=@chassisnummer,nummerplaat=@nummerplaat,type=@type,kleur=@kleur,deuren=@deuren";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@chasisNummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));

                    cmd.Parameters["@merk"].Value = merk;
                    cmd.Parameters["@model"].Value = model;
                    cmd.Parameters["@chassisnummer"].Value = chassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = Nummerplaat;
                    cmd.Parameters["@type"].Value = voertuigType;
                    cmd.Parameters["@kleur"].Value = kleur;
                    cmd.Parameters["@deuren"].Value = deuren;


                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    string brand = (string)["brand"];
                    //    string model = (string)reader["model"];
                    //    vehicles.Add(new Vehicle(brand,model,chassisNumber,licensePlate,new List<FuelType>(),vehicleType,color,doors));
                    //}
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return voertuigen.AsReadOnly();
        }
        #endregion

        #region BestaatVoertuig Method
        public bool BestaatVoertuig(Voertuig v)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT [voertuigid] ,[merk] ,[model] ,[chassisnummer] ,[nummerplaat] ,[type] ,[kleur] ,[deuren] FROM [Project_Flapp_DB].[dbo].[voertuig] WHERE merk = @merk AND model = @model AND chassisnummer = @chassisnummer AND nummerplaat = @nummerplaat AND type = @type AND kleur = @kleur AND deuren = @deuren";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
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
        public IReadOnlyList<Voertuig> GeefAlleVoertuigen()
        {
            SqlConnection conn = new SqlConnection(_connString);
            List<Voertuig> voertuigen = new List<Voertuig>();            
            string query = "SELECT * FROM Voertuig LEFT JOIN Brandstof_Voertuig ON Voertuig.voertuigId = Brandstof_Voertuig.voertuigId LEFT JOIN Brandstof ON Brandstof_Voertuig.brandstofId = Brandstof.brandstofId LEFT JOIN VoertuigType ON Voertuig.type = VoertuigType.voertuigTypeId";
            
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    {
                        List<Brandstof> brandstof = new List<Brandstof> {new Brandstof((string)r["naam"].ToString()) };
                        VoertuigType vt = new VoertuigType((string)r["typeNaam"]);
                        Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstof, vt, (string)r["kleur"], (int)r["deuren"]);

                        voertuigen.Add(voertuig);
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return voertuigen;
        }

        public Voertuig GeefVoertuigDoorID(int vId)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Voertuig WHERE id = @id";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.CommandText = query;

                cmd.Parameters["@id"].Value = vId;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    List<Brandstof> b = new List<Brandstof> { (null) };//_bRepo.GeefBrandstof((int)r["brandstof_id"]); // Mag null zijn
                    Bestuurder bs = null;// _bsRepo.GeefBestuurder((int)r["bestuurder_id"]); // Mag null zijn
                    VoertuigType vt = new VoertuigType((string)r["typeNaam"]);
                    Voertuig voertuig = new Voertuig((int)r["VoertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], b, vt, (string)r["kleur"], (int)r["deuren"], bs);
                    return voertuig;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }

            }
        }
        #endregion

        #region VoegVoertuigToe Method
        public void VoegVoertuigToe(Voertuig v)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Voertuig] ([merk] ,[model] ,[chassisnummer] ,[nummerplaat], [type], [kleur], [deuren]) VALUES " +
                "(@merk ,@model ,@chassisnummer ,@nummerplaat, @type ,@kleur ,@deuren);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    //cmd.Parameters.Add(new SqlParameter("@voertuigid", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));

                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                    //cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    //cmd.Parameters["@voertuigid"].Value = v.VoertuigID;
                    cmd.Parameters["@merk"].Value = v.Merk;
                    cmd.Parameters["@model"].Value = v.Model;
                    cmd.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = v.Nummerplaat;

                    cmd.Parameters["@type"].Value = v.VoertuigType;
                    cmd.Parameters["@kleur"].Value = v.Kleur;
                    cmd.Parameters["@deuren"].Value = v.Aantaldeuren;
                    //cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region UpdateVoertuig Method
        public void UpdateVoertuig(Voertuig v)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Voertuig] WHERE voertuigid = @voertuigid AND merk = @merk AND model = @model" +
                "AND chassisnummer = @chassisnummer AND nummerplaat = @nummerplaat AND type = @type" +
                "AND kleur = @kleur AND deuren = @deuren;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@voertuigid", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    //cmd.Parameters.Add(new SqlParameter("@brandstof_type", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                    //cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@voertuigid"].Value = v.VoertuigID;
                    cmd.Parameters["@merk"].Value = v.Merk;
                    cmd.Parameters["@model"].Value = v.Model;
                    cmd.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = v.Nummerplaat;
                    //cmd.Parameters["@brandstof_type"].Value = v.Brandstof.Id;
                    cmd.Parameters["@type"].Value = v.VoertuigType;
                    cmd.Parameters["@kleur"].Value = v.Kleur;
                    cmd.Parameters["@deuren"].Value = v.Aantaldeuren;
                    //cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VerwijderVoertuig Method
        public void VerwijderVoertuig(Voertuig v)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Voertuig] WHERE id = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = v.VoertuigID;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion
    }
}
