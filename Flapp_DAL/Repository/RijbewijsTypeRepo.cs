using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_DAL.Repository
{
    public class RijbewijsTypeRepo
    {
        private string _connString;

        public RijbewijsTypeRepo(string connString)
        {
            _connString = connString;
        }

        public RijbewijsType GeefRijbewijs(int r)
        {
            throw new NotImplementedException();
        }
    }
}
