using Flapp_BLL.Exceptions;
using Flapp_BLL.Models;
using System;
using Xunit;

namespace Flapp_TESTS
{
    public class Bestuurder_UnitTest
    {
        #region Ctor Tests
        // Ctor 1
        [Fact]
        public void Test_ctor1_Valid()
        {
            Bestuurder b = new("Balci", "Burak", Geslacht.M, "12/05/1999", "99.05.12-273.26", RijbewijsType.B);

            Assert.Equal("Balci", b.Naam);
            Assert.Equal("Burak", b.Voornaam);
            Assert.Equal(Geslacht.M, b.Geslacht);
            Assert.Equal(DateTime.Parse("12/05/1999"), b.Geboortedatum);
            Assert.Equal("99.05.12-273.26", b.Rijksregisternummer);
            Assert.Equal(RijbewijsType.B, b.RijbewijsType);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadNaam_InValid(string naam)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Burak", Geslacht.M, "12/05/1999", "99.05.12-273.26", RijbewijsType.B));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadVoorNaam_InValid(string voornaam)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Burak", voornaam, Geslacht.M, "12/05/1999", "99.05.12-273.26", RijbewijsType.B));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadGeboortedatum_InValid(string geboortedatum)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Burak", "Balci", Geslacht.M, geboortedatum, "99.05.12-273.26", RijbewijsType.B));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("98.05.12-273.26")]
        [InlineData("  ")]
        public void Test_ctor1_BadRijksregister_InValid(string rijksregister)
        {
            Assert.Throws<RijksregisternummerCheckerException>(() => new Bestuurder("Burak", "Balci", Geslacht.M, "12/05/1999", rijksregister, RijbewijsType.B));
        }


        // Ctor 2
        [Fact]
        public void Test_ctor2_Valid()
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", RijbewijsType.B, v, tk);

            Assert.Equal("Balci", b.Naam);
            Assert.Equal("Burak", b.Voornaam);
            Assert.Equal(Geslacht.M, b.Geslacht);
            Assert.Equal(DateTime.Parse("12/05/1999"), b.Geboortedatum);
            Assert.Equal("99.05.12-273.26", b.Rijksregisternummer);
            Assert.Equal(RijbewijsType.B, b.RijbewijsType);
            Assert.Equal(v, b.Voertuig);
            Assert.Equal(tk, b.Tankkaart);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadNaam_InValid(string naam)
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", RijbewijsType.B, v, tk));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadVoornaam_InValid(string voornaam)
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", voornaam, Geslacht.M, a, "12/05/1999", "99.05.12-273.26", RijbewijsType.B, v, tk));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadAdres_invalid(Adres adres)
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, adres, "12/05/1999", "99.05.12-273.26", RijbewijsType.B, v, tk));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadGeboortedatum_invalid(string geboortedatum)
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, geboortedatum, "99.05.12-273.26", RijbewijsType.B, v, tk));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("98.05.12-273.26")]
        [InlineData("  ")]
        public void Test_ctor2_BadRijksregisternummer_invalid(string rijksregister)
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, "12/05/1999", rijksregister, RijbewijsType.B, v, tk));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadVoertuig_invalid(Voertuig voertuig)
        {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", RijbewijsType.B, voertuig, tk));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadTankkaart_invalid(Tankkaart tankkaart)
        {
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", RijbewijsType.B, v, tankkaart));
        }
        #endregion

    }
}
