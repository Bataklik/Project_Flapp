using Flapp_BLL.Interfaces;
using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flapp_TESTS.UnitTests_Models
{
    public class BrandstofManager_UnitTest
    {
        private IBrandstofRepo repo;
        //[Fact]
        public void VoegBrandstofToe()
        {
            Brandstof brandstof = new Brandstof("Benzine");
            BrandstofManager bm = new BrandstofManager(repo);
            bm.VoegBrandstofToe(brandstof);
        }
    }
}
