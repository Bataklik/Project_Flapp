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

        #region BestaatVoertuig Method
        public bool BestaatVoertuig(Voertuig v)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT [id] ,[merk] ,[model] ,[chassisnummer] ,[nummerplaat] ,[brandstof_type] ,[voertuig_type] ,[kleur] ,[deuren] ,[bestuurder_id] FROM [Project_Flapp_DB].[dbo].[voertuig] WHERE merk = @merk AND model = @model AND chassisnummer = @chassisnummer AND nummerplaat = @nummerplaat AND brandstof_type = @brandstof_type AND voertuig_type = @voertuig_type AND kleur = @kleur AND deuren = @deuren AND bestuurder_id = @bestuurder_id";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.VarChar));

                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@brandstof_type", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@voertuig_type", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@merk"].Value = v.Merk;
                    cmd.Parameters["@model"].Value = v.Model;

                    cmd.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = v.Nummerplaat;
                    cmd.Parameters["@brandstof_type"].Value = v.Brandstoftype;
                    cmd.Parameters["@voertuig_type"].Value = v.VoertuigType;

                    cmd.Parameters["@kleur"].Value = v.Kleur;
                    cmd.Parameters["@deuren"].Value = v.Aantaldeuren;
                    cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;


                    int AdresBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (AdresBestaat == 1) { return true; }
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
            throw new NotImplementedException();
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
                    Brandstof b = null;//_bRepo.GeefBrandstof((int)r["brandstof_id"]); // Mag null zijn
                    Bestuurder bs = null;// _bsRepo.GeefBestuurder((int)r["bestuurder_id"]); // Mag null zijn
                    Voertuig voertuig = new Voertuig((int)r["VoertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], b, (string)r["voertuigType"], (string)r["kleur"], (int)r["deuren"], bs);
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
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Voertuig] ([id] ,[merk] ,[model] ,[chassisnummer] ,[nummerplaat], [brandstof_type], [voertuig_type], [kleur], [deuren], [bestuurder_id]) VALUES " +
                "(@id ,@merk ,@model ,@chassisnummer ,@nummerplaat, @brandstof_type ,@voertuig_type ,@kleur ,@deuren ,@bestuurder_id);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@brandstof_type", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@voertuig_type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@id"].Value = v.VoertuigID;
                    cmd.Parameters["@merk"].Value = v.Merk;
                    cmd.Parameters["@model"].Value = v.Model;
                    cmd.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = v.Nummerplaat;
                    cmd.Parameters["@brandstof_type"].Value = v.Brandstoftype.Id;
                    cmd.Parameters["@voertuig_type"].Value = v.VoertuigType;
                    cmd.Parameters["@kleur"].Value = v.Kleur;
                    cmd.Parameters["@deuren"].Value = v.Aantaldeuren;
                    cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;


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
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Voertuig] WHERE id = @id AND merk = @merk AND model = @model" +
                "AND chassisnummer = @chassisnummer AND nummerplaat = @nummerplaat AND brandstof_type = @brandstof_type AND voertuig_type = @voertuig_type"+
                "AND kleur = @kleur AND deuren = @deuren AND bestuurder_id = @bestuurder_id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@brandstof_type", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@voertuig_type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@deuren", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@bestuurder_id", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@id"].Value = v.VoertuigID;
                    cmd.Parameters["@merk"].Value = v.Merk;
                    cmd.Parameters["@model"].Value = v.Model;
                    cmd.Parameters["@chassisnummer"].Value = v.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = v.Nummerplaat;
                    cmd.Parameters["@brandstof_type"].Value = v.Brandstoftype.Id;
                    cmd.Parameters["@voertuig_type"].Value = v.VoertuigType;
                    cmd.Parameters["@kleur"].Value = v.Kleur;
                    cmd.Parameters["@deuren"].Value = v.Aantaldeuren;
                    cmd.Parameters["@bestuurder_id"].Value = v.Bestuurder.Id;
                    

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
