using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_DAL.Repository
{
    public class BrandstofRepo : IBrandstofRepo
    {
        private string _connString;

        public BrandstofRepo(string connString)
        {
            _connString = connString;
        }

        #region BestaatBrandstof Methods
        public bool BestaatBrandstof(Brandstof b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@naam"].Value = b.Naam;

                    int brandstofBestaat = Convert.ToInt32(cmd.ExecuteScalar());
                    if (brandstofBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool BestaatBrandstof(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE brandstofId = @id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = id;

                    int brandstofBestaat = Convert.ToInt32(cmd.ExecuteScalar());
                    if (brandstofBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool BestaatBrandstof(string brandstof_naam)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE naam = @brandstof_naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@brandstof_naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@brandstof_naam"].Value = brandstof_naam;

                    int brandstofBestaat = Convert.ToInt32(cmd.ExecuteScalar());
                    if (brandstofBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefBrandstof Methods
        public Brandstof GeefBrandstof(Brandstof b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Brandstof WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = b.Naam;

                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Brandstof brandstof = new((int)r["id"], (string)r["naam"]);
                    return brandstof;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public Brandstof GeefBrandstof(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Brandstof WHERE brandstofId = @id;";
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
                    Brandstof brandstof = new((int)r["id"], (string)r["naam"]);
                    return brandstof;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VoegBrandstofToe Method
        public void VoegBrandstofToe(Brandstof b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([naam]) VALUES (@brandstof_naam);";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@brandstof_naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@brandstof_naam"].Value = b.Naam;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        #endregion

        #region UpdateBrandstof Method
        public void UpdateBrandstof(Brandstof b)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Brandstof] SET [naam] = @brandstof_naam WHERE brandstofId = @brandstof_id;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@brandstof_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@brandstof_naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@brandstof_id"].Value = b.Id;
                    cmd.Parameters["@brandstof_naam"].Value = b.Naam;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VerwijderBrandstof Methods
        public void VerwijderBrandstof(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Brandstof] WHERE brandstofId = @id;";
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
        public void VerwijderBrandstof(string brandstof_naam)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; DELETE FROM [dbo].[Brandstof] WHERE naam = @brandstof_naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@brandstof_naam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@brandstof_naam"].Value = brandstof_naam;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region GeefAlleBrandstoffen
        public IReadOnlyList<Brandstof> GeefAlleBrandstoffen() {
            SqlConnection conn = new SqlConnection(_connString);
            List<Brandstof> brandstoffen = new List<Brandstof>();
            string query = "SELECT * FROM [Project_Flapp_DB].[dbo].[Tankkaart] INNER JOIN Brandstof_Tankkaart ON Tankkaart.tankkaartId = Brandstof_Tankkaart.tankkaartId INNER JOIN Brandstof ON Brandstof_Tankkaart.brandstofId = Brandstof.brandstofId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        string naam = (string)r["naam"];
                        Brandstof brandstof = new Brandstof(naam);
                        brandstoffen.Add(brandstof);
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return brandstoffen;
        }
        #endregion
    }
}
