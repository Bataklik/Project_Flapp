using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Flapp_DAL.Repository
{
    public class RijbewijsTypeRepo : IRijbewijsTypeRepo
    {
        private string _connString;

        public RijbewijsTypeRepo(string connString)
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
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
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
        public bool BestaatRijbewijs(RijbewijsType rijbewijs)
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
        public RijbewijsType GeefRijbewijs(RijbewijsType rijbewijs)
        {
            throw new NotImplementedException();
        }
        public RijbewijsType GeefRijbewijs(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VoegRijbewijsToe Method
        public void VoegRijbewijsToe(RijbewijsType rijbewijs)
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
            throw new NotImplementedException();
        }
        public void VerwijderRijbewijs(RijbewijsType rijbewijs)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
