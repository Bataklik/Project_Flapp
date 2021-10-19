using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces {
    public interface ITankkaartRepo {
        bool BestaatTankkaart(Tankkaart tankkaart);
        bool BestaatTankkaartNr(int kaartnr);
        void VoegTankkaartToe(Tankkaart tankkaart);
        void VerwijderTankkaart(Tankkaart tankkaart);
        void UpdateTankkaart(Tankkaart tankkaart);
        IReadOnlyList<Tankkaart> GeefAlleTankkaarten();
    }
}
