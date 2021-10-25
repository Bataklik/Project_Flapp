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

        public bool BestaatRijbewijs(RijbewijsType r)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM RijbewijsType WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = r.GetType();

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
        public bool BestaatRijbewijs(int id)
        {
            throw new NotImplementedException();
        }
        public bool BestaatRijbewijs(string naam)
        {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM RijbewijsType WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));

                    cmd.CommandText = query;

                    cmd.Parameters["@naam"].Value = r.GetType();

                    int bestuurderBestaat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (bestuurderBestaat == 1) { return true; }
                    return false;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }

        public RijbewijsType GeefRijbewijs(RijbewijsType r)
        {
            throw new NotImplementedException();
        }
        public RijbewijsType GeefRijbewijs(int id)
        {
            throw new NotImplementedException();
        }

        public void VerwijderRijbewijs(int id)
        {
            throw new NotImplementedException();
        }

        public void VerwijderRijbewijs(string naam)
        {
            throw new NotImplementedException();
        }

        public void VoegRijbewijsToe(RijbewijsType rijbewijs)
        {
            throw new NotImplementedException();
        }
    }
}
