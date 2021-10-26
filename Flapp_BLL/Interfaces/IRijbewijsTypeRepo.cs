using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IRijbewijsTypeRepo
    {
        RijbewijsType GeefRijbewijs(int id);
        RijbewijsType GeefRijbewijs(RijbewijsType rijbewijs);
        bool BestaatRijbewijs(int id);
        bool BestaatRijbewijs(RijbewijsType rijbewijs);
        void VoegRijbewijsToe(RijbewijsType rijbewijs);
        void VerwijderRijbewijs(int id);
        void VerwijderRijbewijs(RijbewijsType rijbewijs);
    }
}
