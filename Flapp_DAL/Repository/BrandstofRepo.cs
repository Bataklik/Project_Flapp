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
    public class BrandstofRepo : IBrandstofRepo {
        private string _connString;

        public BrandstofRepo(string connString) {
            _connString = connString;
        }

        public bool BestaatBrandstof(Brandstof b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT 1 FROM Brandstof WHERE naam = @naam;";
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
        public Brandstof GeefBrandstof(Brandstof b) {
            SqlConnection conn = new SqlConnection(_connString);
            string query = "USE [Project_Flapp_DB]; SELECT * FROM Brandstof WHERE naam = @naam;";
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));

                cmd.CommandText = query;

                cmd.Parameters["@naam"].Value = b.Naam;

                conn.Open();
                try {
                    SqlDataReader r = cmd.ExecuteReader();
                    r.Read();
                    Brandstof brandstof = new((string)r["naam"]);
                    return brandstof;
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
        }
    }
}
