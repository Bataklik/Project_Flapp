using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_DAL.Repository
{
    public class VoertuigTypeRepo : IVoertuigTypeRepo
    {
        private string _connString;

        public VoertuigTypeRepo(string connString)
        {
            _connString = connString;            
        }
        public IReadOnlyList<string> GeefAlleVoertuigTypes()
        {
            SqlConnection conn = new SqlConnection(_connString);
            List<string> voertuigen = new List<string>();
            string query = "USE Project_Flapp_DB; SELECT typeNaam FROM[Project_Flapp_DB].[dbo].[VoertuigType];";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        //List<Brandstof> brandstof = new List<Brandstof> { new Brandstof((string)r["naam"]) };
                        //Voertuig voertuig = new Voertuig((int)r["voertuigId"], (string)r["merk"], (string)r["model"], (string)r["chassisnummer"], (string)r["nummerplaat"], brandstof, (string)r["type"], (string)r["kleur"], (int)r["deuren"]);
                        string vt = (string)r["typeNaam"];
                        voertuigen.Add(vt);
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { conn.Close(); }
            }
            return voertuigen;
        }
    }
}
