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
        RijbewijsType GeefRijbewijs(RijbewijsType rijbewijs);
        RijbewijsType GeefRijbewijs(int id);
        bool BestaatRijbewijs(RijbewijsType rijbewijs);
        bool BestaatRijbewijs(int id);
        bool BestaatRijbewijs(string naam);
        void VoegRijbewijsToe(RijbewijsType rijbewijs);
        void VerwijderRijbewijs(int id);
        void VerwijderRijbewijs(string naam);
    }
}
