using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
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

        public Brandstof GeefBrandstof(int bId)
        {
            throw new NotImplementedException();
        }
    }
}
