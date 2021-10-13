using System;
using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions;

namespace Flapp_TESTS
{
    public class Voertuig_UnitTest {

        #region Ctor Tests
        // ctor 1
        [Fact]
        public void Test_ctor_Valid() {
            Brandstof b = new Brandstof("Elektrisch");
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Equal(420, v.VoertuigID);
            Assert.Equal("Tesla", v.Merk);
            Assert.Equal("Model X", v.Model);
            Assert.Equal("1abcd23efgh456789", v.ChassisNummer);
            Assert.Equal("2-ABC-123", v.Nummerplaat);
            Assert.Equal(b, v.Brandstoftype);
            Assert.Equal("Stationwagen", v.VoertuigType);
            Assert.Equal("Zwart", v.Kleur);
            Assert.Equal(5, v.Aantaldeuren);
        }
        [Theory]
        [InlineData(-20)]        
        public void Test_ctor1_BadId_InValid(int id)
        {
            Brandstof b = new Brandstof("Elektrisch");
            Assert.Throws<VoertuigException>(() => new Voertuig(id, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadMerk_InValid(string merk)
        {
            Brandstof b = new Brandstof("Elektrisch");
            Assert.Throws<VoertuigException>(() => new Voertuig(420, merk, "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadModel_InValid(string model)
        {
            Brandstof b = new Brandstof("Elektrisch");
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", model, "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345678123456az")]               
        [InlineData("  ")]
        public void Test_ctor1_BadChassisnummer_InValid(string chassis)
        {
            Brandstof b = new Brandstof("Elektrisch");
            Assert.Throws<ChassisnummerCheckerException>(() => new Voertuig(420, "Tesla", "Model X", chassis, "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1-abc-a12")]
        [InlineData("a-a1c1112")]
        [InlineData("1.abc.112")]
        [InlineData("4-abc-112")]
        [InlineData("  ")]
        public void Test_ctor1_BadNummerplaat_InValid(string nummerplaat)
        {
            Brandstof b = new Brandstof("Elektrisch");
            Assert.Throws<NummerplaatCheckerException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", nummerplaat, b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadBrandstof_InValid(Brandstof br)
        {
            Brandstof br = "";
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", br, "Stationwagen", "Zwart", 5));
        }
        #endregion
    }
}
