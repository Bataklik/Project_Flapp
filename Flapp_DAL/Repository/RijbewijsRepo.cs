using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
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
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Rijbewijs WHERE id = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
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
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Rijbewijs WHERE rijbewijs_naam = @naam;";
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
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Rijbewijs WHERE rijbewijs_naam = @naam;";
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
                    Rijbewijs rijb = new((int)r["id"], (string)r["rijbewijs_naam"]);
                    return rijb;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public Rijbewijs GeefRijbewijs(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Rijbewijs WHERE id = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.CommandText = query;
                cmd.Parameters["@id"].Value = id;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Rijbewijs rijb = new((int)r["id"], (string)r["rijbewijs_naam"]);
                    return rijb;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VoegRijbewijsToe Method
        public void VoegRijbewijsToe(Rijbewijs rijbewijs)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs]([rijbewijs_naam])VALUES (@rijbewijs_naam);";
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
        #endregion

        #region VerwijderRijbewijs Method
        public void VerwijderRijbewijs(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Rijbewijs] WHERE id = @id;";
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
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Rijbewijs] WHERE rijbewijs_naam = @naam;";
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
