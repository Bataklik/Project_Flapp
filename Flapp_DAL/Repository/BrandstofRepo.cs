using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository {
    public class BrandstofRepo : IBrandstofRepo {
        private string _connString;

        public BrandstofRepo(string connString) {
            _connString = connString;
        }

        #region BestaatBrandstof Methods
        public bool BestaatBrandstof(Brandstof b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE naam=@naam;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
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
        public bool BestaatBrandstof(int id) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE brandstofId = @id;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
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
        public bool BestaatBrandstof(string brandstof_naam) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE brandstofnaam = @brandstof_naam;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
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
        public Brandstof GeefBrandstof(Brandstof b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "SELECT * FROM Brandstof WHERE naam=@naam;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                cmd.CommandText = query;
                cmd.Parameters["@naam"].Value = b.Naam;

                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Brandstof brandstof = new((int)r["brandstofId"], (string)r["naam"]);
                    return brandstof;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public Brandstof GeefBrandstof(int id) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Brandstof WHERE brandstofId = @id;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.CommandText = query;
                cmd.Parameters["@id"].Value = id;

                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Brandstof brandstof = new((int)r["id"], (string)r["brandstofnaam"]);
                    return brandstof;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        #endregion

        #region VoegBrandstofToe Method
        public void VoegBrandstofToe(Brandstof b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([brandstofnaam]) VALUES (@brandstofnaam);";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
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
        public void UpdateBrandstof(Brandstof b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; UPDATE [dbo].[Brandstof] SET [brandstofnaam] = @brandstofnaam WHERE brandstofId = @brandstofId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@brandstofId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@brandstofnaam", SqlDbType.VarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@brandstofId"].Value = b.Id;
                    cmd.Parameters["@brandstofnaam"].Value = b.Naam;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public void UpdateTankkaartBrandstof(Brandstof b, Tankkaart t) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "UPDATE Brandstof_Tankkaart SET brandstofId=@brandstofId WHERE tankkaartId=@tankkaartId;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@brandstofId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@tankkaartId", SqlDbType.Int));

                    cmd.CommandText = query;

                    cmd.Parameters["@brandstofId"].Value = b.Id;
                    cmd.Parameters["@tankkaartId"].Value = t.Kaartnummer;

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
            string query = "DELETE FROM [dbo].[Brandstof] WHERE brandstofId = @brandstofId;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@brandstofId", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@brandstofId"].Value = id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public void VerwijderBrandstof(string brandstof_naam) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "DELETE FROM Brandstof WHERE brandstofnaam=@brandstofnaam;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                try {
                    cmd.Parameters.Add(new SqlParameter("@brandstofnaam", SqlDbType.VarChar));

                    cmd.CommandText = query;

                    cmd.Parameters["@brandstofnaam"].Value = brandstof_naam;

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
            string query = "SELECT * FROM [dbo].[Brandstof]";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = query;
                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read()) {
                        int id = (int)r["brandstofId"];
                        string brandstofnaam = (string)r["naam"];
                        Brandstof brandstof = new Brandstof(id, brandstofnaam);
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
