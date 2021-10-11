using Flapp_BLL.Exceptions;
using Flapp_BLL.Models;
using Flapp_BLL.Utils;
using System;
using Xunit;

namespace Flapp_TESTS
{
    public class Bestuurder_UnitTest
    {
        // Ctor 1
        #region Ctor Tests
        [Fact]
        public void Test_ctor1_Valid()
        {
            Bestuurder b = new("Balci", "Burak", Geslacht.M, DateTime.Parse("12/05/1999"), "99.05.12-273.26", RijbewijsType.B);

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
            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Burak", Geslacht.M, DateTime.Parse("12/05/1999"), "99.05.12-273.26", RijbewijsType.B));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadVoorNaam_InValid(string voornaam)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Burak", voornaam, Geslacht.M, DateTime.Parse("12/05/1999"), "99.05.12-273.26", RijbewijsType.B));
        }

        // Ctor 2
        [Fact]
        public void Test_ctor2_Valid()
        {
            //Working on it
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4));
            Adres a = new Adres("Straat", 1, "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new Brandstof("Benzine"), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, DateTime.Parse("12/05/1999"), "99.05.12-273.26", RijbewijsType.B, v, tk);

            Assert.Equal("Balci", b.Naam);
            Assert.Equal("Burak", b.Voornaam);
            Assert.Equal(Geslacht.M, b.Geslacht);
            Assert.Equal(DateTime.Parse("12/05/1999"), b.Geboortedatum);
            Assert.Equal("99.05.12-273.26", b.Rijksregisternummer);
            Assert.Equal(RijbewijsType.B, b.RijbewijsType);
            Assert.Equal(v, b.Voertuig);
            Assert.Equal(tk, b.Tankkaart);
        }
        #endregion

    }
}
