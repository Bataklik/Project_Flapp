using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IRijbewijsRepo
    {
        Rijbewijs GeefRijbewijs(int id);
        Rijbewijs GeefRijbewijs(Rijbewijs rijbewijs);
        bool BestaatRijbewijs(int id);
        bool BestaatRijbewijs(Rijbewijs rijbewijs);
        void VoegRijbewijsToe(Rijbewijs rijbewijs);
        void VerwijderRijbewijs(int id);
        void VerwijderRijbewijs(Rijbewijs rijbewijs);
    }
}
