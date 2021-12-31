using Flapp_BLL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flapp_BLL.Interfaces
{
    public interface IRijbewijsRepo
    {
        Rijbewijs GeefRijbewijs(int id);
        Rijbewijs GeefRijbewijs(Rijbewijs rijbewijs);
        List<Rijbewijs> GeefAlleRijbewijzen();

        bool BestaatRijbewijs(int id);
        bool BestaatRijbewijs(Rijbewijs rijbewijs);

        void VoegRijbewijsToe(Rijbewijs rijbewijs);
        void VoegRijbewijsToeBestuurder(int bestuurder, int rijbewijs);

        void VerwijderRijbewijs(int id);
        void VerwijderRijbewijs(Rijbewijs rijbewijs);
    }
}
