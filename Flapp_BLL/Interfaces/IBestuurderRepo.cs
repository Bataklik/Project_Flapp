using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IBestuurderRepo
    {
        bool BestaatBestuurder(Bestuurder bestuurder);
        bool BestaatBestuurderId(int id);
        void VoegBestuurderToe(Bestuurder bestuurder);
        void VerwijderBestuurder(Bestuurder bestuurder);
        void UpdateBestuurder(Bestuurder bestuurder);
        IReadOnlyList<Bestuurder> GeefAlleBestuurders();
        Bestuurder GeefBestuurder(Bestuurder b);
    }
}
