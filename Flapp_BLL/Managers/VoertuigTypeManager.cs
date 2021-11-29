using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Managers
{
    public class VoertuigTypeManager
    {
        private IVoertuigTypeRepo _repo;
        public VoertuigTypeManager(IVoertuigTypeRepo repo)
        {
            _repo = repo;
        }
        public IReadOnlyList<string> GeefAlleVoertuigTypes()
        {
            try
            {
                return _repo.GeefAlleVoertuigTypes();
            }
            catch (Exception ex) { throw new VoertuigManagerException("VoertuigManager: Geef alle voertuigen:", ex); }
        }
    }
}
