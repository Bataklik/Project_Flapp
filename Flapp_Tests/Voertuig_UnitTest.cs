using System;
using Xunit;
using Flapp_BLL.Models;
namespace Flapp_TESTS
{
    public class Voertuig_UnitTest {

        #region Ctor Tests
        [Fact]
        public void Test_ctor_Valid() {
            Brandstof b = new Brandstof("Elektrisch");
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "13245678957903251", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Equal(420, v.VoertuigID);
            Assert.Equal("Tesla", v.Merk);
            Assert.Equal("Model X", v.Model);
            Assert.Equal("13245678957903251", v.ChassisNummer);
            Assert.Equal("2-ABC-123", v.Nummerplaat);
            Assert.Equal(b, v.Brandstoftype);
            Assert.Equal("Stationwagen", v.VoertuigType);
            Assert.Equal("Zwart", v.Kleur);
            Assert.Equal(5, v.Aantaldeuren);
        }
        #endregion
    }
}
