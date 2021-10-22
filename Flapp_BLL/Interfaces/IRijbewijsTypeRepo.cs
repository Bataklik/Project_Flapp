using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Interfaces
{
    public interface IRijbewijsTypeRepo
    {
        RijbewijsType GeefRijbewijs(int rId);
    }
}
