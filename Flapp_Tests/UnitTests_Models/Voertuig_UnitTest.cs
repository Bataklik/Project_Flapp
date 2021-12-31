using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions;
using Flapp_BLL.Exceptions.CheckerExceptions;
using Flapp_BLL.Exceptions.ModelExpections;
using System.Collections.Generic;

namespace Flapp_TESTS.UnitTests_Models
{
    public class Voertuig_UnitTest
    {

        #region Ctor Tests
        #region ctor 1
        // Ctor 1
        [Fact]
        public void Test_ctor1_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Equal(420, v.VoertuigID);
            Assert.Equal("Tesla", v.Merk);
            Assert.Equal("Model X", v.Model);
            Assert.Equal("1abcd23efgh456789", v.ChassisNummer);
            Assert.Equal("2-ABC-123", v.Nummerplaat);
            Assert.Equal(b, v.Brandstof);
            Assert.Equal("Stationwagen", v.VoertuigType);
            Assert.Equal("Zwart", v.Kleur);
            Assert.Equal(5, v.Aantaldeuren);
        }
        [Theory]
        [InlineData(-20)]
        public void Test_ctor1_BadId_InValid(int id)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(id, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadMerk_InValid(string merk)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, merk, "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadModel_InValid(string model)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", model, "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345678123456az")]
        [InlineData("  ")]
        public void Test_ctor1_BadChassisnummer_InValid(string chassis)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
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
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<NummerplaatCheckerException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", nummerplaat, b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor1_BadBrandstof_InValid(List<Brandstof> br)
        {
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", br, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadTypeWagen_InValid(string type)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, type, "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadKleur_InValid(string kleur)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", kleur, 5));
        }
        [Theory]
        [InlineData(-4)]
        public void Test_ctor1_BadAantalDeuren_InValid(int aantalDeuren)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "zwart", aantalDeuren));
        }
        #endregion
        #region Ctor 2        
        [Fact]
        public void Test_ctor2_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig("Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen");
            Assert.Equal("Tesla", v.Merk);
            Assert.Equal("Model X", v.Model);
            Assert.Equal("1abcd23efgh456789", v.ChassisNummer);
            Assert.Equal("2-ABC-123", v.Nummerplaat);
            Assert.Equal(b, v.Brandstof);
            Assert.Equal("Stationwagen", v.VoertuigType);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadMerk_InValid(string merk)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, merk, "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadModel_InValid(string model)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", model, "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345678123456az")]
        [InlineData("  ")]
        public void Test_ctor2_BadChassisnummer_InValid(string chassis)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
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
        public void Test_ctor2_BadNummerplaat_InValid(string nummerplaat)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<NummerplaatCheckerException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", nummerplaat, b, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadBrandstof_InValid(List<Brandstof> br)
        {
           
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", br, "Stationwagen", "Zwart", 5));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadTypeWagen_InValid(string type)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Assert.Throws<VoertuigException>(() => new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, type, "Zwart", 5));
        }
        #endregion
        #endregion

        #region zetMethods tests
        // ZetVoertuigId
        [Fact]
        public void Test_ZetVoertuigId_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetVoeruigID(120);
            Assert.Equal(120, v.VoertuigID);
        }
        [Theory]
        [InlineData(-8)]
        public void Test_ZetVoertuigId_BadId_Valid(int id)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetVoeruigID(id));
        }
        //zetMerk
        [Fact]
        public void Test_ZetMerk_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetMerk("Renault");
            Assert.Equal("Renault", v.Merk);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetMerk_BadMerk_Valid(string merk)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetMerk(merk));
        }
        //zetModel
        [Fact]
        public void Test_ZetModel_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetModel("Clio");
            Assert.Equal("Clio", v.Model);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetModel_BadModel_Valid(string model)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetModel(model));
        }
        //zetChassisNummer
        [Fact]
        public void Test_ZetChassisNummer_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetChassisNummer("123456789abcdefgh");
            Assert.Equal("123456789abcdefgh", v.ChassisNummer);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345678123456az")]
        [InlineData("  ")]
        public void Test_ZetChassisNummer_BadChassisNummer_Valid(string chassisnummer)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<ChassisnummerCheckerException>(() => v.ZetChassisNummer(chassisnummer));
        }
        //zetNummerplaat
        [Fact]
        public void Test_ZetNummerplaat_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetNummerplaat("1-plf-679");
            Assert.Equal("1-plf-679", v.Nummerplaat);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1-abc-a12")]
        [InlineData("a-a1c1112")]
        [InlineData("1.abc.112")]
        [InlineData("4-abc-112")]
        [InlineData("  ")]
        public void Test_ZetNummerplaat_BadNummerplaat_Valid(string nummerplaat)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<NummerplaatCheckerException>(() => v.ZetNummerplaat(nummerplaat));
        }
        //zetBrandstofType
        [Fact]
        public void Test_ZetBrandstof_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            List<Brandstof> testBrandstof = new List<Brandstof> { new Brandstof("Diesel") };
            v.ZetBrandstofTypeLijst(testBrandstof);
            Assert.Equal(testBrandstof, v.Brandstof);
        }
        [Theory]
        [InlineData(null)]
        public void Test_ZetBrandstof_BadBrandstof_Valid(List<Brandstof> brandstof)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetBrandstofTypeLijst(brandstof));
        }
        //zetTypeWagen
        [Fact]
        public void Test_ZetVoertuigType_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetVoertuigType("Break");
            Assert.Equal("Break", v.VoertuigType);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetVoertuigType_BadVoertuigType_Valid(string type)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetVoertuigType(type));
        }
        //ZetKleur
        [Fact]
        public void Test_ZetKleur_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetKleur("Rood");
            Assert.Equal("Rood", v.Kleur);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetKleur_BadKleur_Valid(string kleur)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetKleur(kleur));
        }
        //zetAantalDeuren
        [Fact]
        public void Test_ZetAantalDeuren_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            v.ZetAantalDeuren(3);
            Assert.Equal(3, v.Aantaldeuren);
        }
        [Theory]
        [InlineData(8)]
        public void Test_ZetAantalDeuren_BadAantalDeuren_Valid(int aantaldeuren)
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);

            Assert.Throws<VoertuigException>(() => v.ZetAantalDeuren(aantaldeuren));
        }
        [Fact]
        public void Test_SetDriver_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);
            Bestuurder driver = new Bestuurder("Raf", "Troch", Geslacht.M, "11/05/1999", "99.05.11-273.26", new());
            v.ZetBestuurder(driver);
            Assert.Equal(driver, v.Bestuurder);
            Assert.Equal(v, driver.Voertuig);
        }

        /*
        [Fact]
        public void Test_SetDriver_Invalid()
        {
            Driver driver = new Driver("Elvis", "Presley", new DateTime(1997, 05, 20), "97.05.20-327.78", new List<string> { "B", "A1" });
            Vehicle vehicle = new Vehicle(1, "Porsche", "GT2RS", "1234-1234-1234-17", "KAPPER FURKAN", new FuelType("Gasoline"), "Sportauto", "Donkergrijs", 2, driver);
            Assert.Throws<VehicleException>(() => vehicle.SetDriver(driver));
        }
        */

        [Fact]
        public void Test_HasDriver_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Bestuurder d = new Bestuurder("Raf", "Troch", Geslacht.M, "11/05/1999", "99.05.11-273.26", new());
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5, d);
            Assert.True(v.HeeftBestuurder(d));
        }

        [Fact]
        public void Test_HasDriver_Invalid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Bestuurder d = new Bestuurder("Raf", "Troch", Geslacht.M, "11/05/1999", "99.05.11-273.26", new());
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5);
            Assert.False(v.HeeftBestuurder(d));
        }

        [Fact]
        public void Test_RemoveDriver_Valid()
        {
            List<Brandstof> b = new List<Brandstof>{ new Brandstof("Diesel") };
            Bestuurder d = new Bestuurder("Raf", "Troch", Geslacht.M, "11/05/1999", "99.05.11-273.26", new());
            Voertuig v = new Voertuig(420, "Tesla", "Model X", "1abcd23efgh456789", "2-ABC-123", b, "Stationwagen", "Zwart", 5, d);
            v.VerwijderBestuurder();
            Assert.Null(v.Bestuurder);
        }
        #endregion
    }
}
