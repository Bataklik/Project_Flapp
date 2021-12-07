using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class RijbewijsRepo : IRijbewijsRepo
    {
        private string _connString;

        public RijbewijsRepo(string connString)
        {
            _connString = connString;
        }

        #region BestaatRijbewijs Method
        public bool BestaatRijbewijs(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Rijbewijs WHERE rijbewijsId = @rijbewijsId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@rijbewijsId", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = id;

                    int rijbewijsBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (rijbewijsBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool BestaatRijbewijs(Rijbewijs rijbewijs)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Rijbewijs WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@naam"].Value = rijbewijs.Naam;

                    int rijbewijsBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (rijbewijsBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefRijbewijs Method
        public Rijbewijs GeefRijbewijs(Rijbewijs rijbewijs)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Rijbewijs WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = rijbewijs.Naam;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Rijbewijs rijb = new((int)r["rijbewijsId"], (string)r["naam"]);
                    return rijb;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public Rijbewijs GeefRijbewijs(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Rijbewijs WHERE rijbewijsId = @rijbewijsId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.CommandText = query;
                cmd.Parameters["@rijbewijsId"].Value = id;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Rijbewijs rijb = new((int)r["rijbewijsId"], (string)r["naam"]);
                    return rijb;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefAlleRijbewijzen
        public List<Rijbewijs> GeefAlleRijbewijzen()
        {
            SqlConnection conn = new SqlConnection(_connString);
            List<Rijbewijs> rijbewijzen = new List<Rijbewijs>();
            string query = "SELECT * FROM [Project_Flapp_DB].[dbo].[Rijbewijs];";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Rijbewijs rijbewijs = new Rijbewijs((int)r["rijbewijsId"], (string)r["naam"]);
                        rijbewijzen.Add(rijbewijs);
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return rijbewijzen;
        }
        #endregion

        #region VoegRijbewijsToe Method
        public void VoegRijbewijsToe(Rijbewijs rijbewijs)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs]([naam])VALUES (@naam);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@rijbewijs_naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@rijbewijs_naam"].Value = rijbewijs.Naam;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public void VoegRijbewijsToeBestuurder(int bestuurder, int rijbewijs)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES(@rId,@bId);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@rId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BId", SqlDbType.Int));

                    cmd.CommandText = query;
                    cmd.Parameters["@rId"].Value = rijbewijs;
                    cmd.Parameters["@bId"].Value = bestuurder;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VerwijderRijbewijs Method
        public void VerwijderRijbewijs(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Rijbewijs] WHERE rijbewijsId = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public void VerwijderRijbewijs(Rijbewijs rijbewijs)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Rijbewijs] WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@naam"].Value = rijbewijs.Naam;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion
    }
}
