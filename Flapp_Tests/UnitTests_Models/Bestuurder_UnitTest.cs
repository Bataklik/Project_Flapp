using Flapp_BLL.Exceptions.CheckerExceptions;
using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Flapp_TESTS.UnitTests_Models {
    public class Bestuurder_UnitTest {
        #region Ctor Tests
        // Ctor 1
        [Fact]
        public void Test_ctor1_Valid() {
            List<Rijbewijs> rt = new List<Rijbewijs> { new Rijbewijs("B") };
            Bestuurder b = new("Balci", "Burak", Geslacht.M, "12/05/1999", "99.05.12-273.26", rt);

            Assert.Equal("Balci", b.Naam);
            Assert.Equal("Burak", b.Voornaam);
            Assert.Equal(Geslacht.M, b.Geslacht);
            Assert.Equal(DateTime.Parse("12/05/1999"), b.Geboortedatum);
            Assert.Equal("99.05.12-273.26", b.Rijksregisternummer);
            Assert.Contains(new Rijbewijs("B"), b.Rijbewijzen);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadNaam_InValid(string naam) {
            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Burak", Geslacht.M, "12/05/1999", "99.05.12-273.26", new()));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadVoorNaam_InValid(string voornaam) {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Burak", voornaam, Geslacht.M, "12/05/1999", "99.05.12-273.26", new()));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor1_BadGeboortedatum_InValid(string geboortedatum) {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Burak", "Balci", Geslacht.M, geboortedatum, "99.05.12-273.26", new()));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("98.05.12-273.26")]
        [InlineData("  ")]
        public void Test_ctor1_BadRijksregister_InValid(string rijksregister) {
            Assert.Throws<RijksregisternummerCheckerException>(() => new Bestuurder("Burak", "Balci", Geslacht.M, "12/05/1999", rijksregister, new()));
        }


        // Ctor 2
        [Fact]
        public void Test_ctor2_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);
            List<Rijbewijs> rt = new List<Rijbewijs>();
            rt.Add(new Rijbewijs("B"));
            b.ZetRijbewijsLijst(rt);
            Assert.Equal("Balci", b.Naam);
            Assert.Equal("Burak", b.Voornaam);
            Assert.Equal(Geslacht.M, b.Geslacht);
            Assert.Equal(DateTime.Parse("12/05/1999"), b.Geboortedatum);
            Assert.Equal("99.05.12-273.26", b.Rijksregisternummer);
            Assert.Contains(new Rijbewijs("B"), b.Rijbewijzen);
            Assert.Equal(v, b.Voertuig);
            Assert.Equal(tk, b.Tankkaart);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadNaam_InValid(string naam) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadVoornaam_InValid(string voornaam) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", voornaam, Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadAdres_invalid(Adres adres) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, adres, "12/05/1999", "99.05.12-273.26", new(), v, tk));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ctor2_BadGeboortedatum_invalid(string geboortedatum) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, geboortedatum, "99.05.12-273.26", new(), v, tk));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("98.05.12-273.26")]
        [InlineData("  ")]
        public void Test_ctor2_BadRijksregisternummer_invalid(string rijksregister) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, "12/05/1999", rijksregister, new(), v, tk));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadVoertuig_invalid(Voertuig voertuig) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), voertuig, tk));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadTankkaart_invalid(Tankkaart tankkaart) {
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Balci", "", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tankkaart));
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor2_BadRijbewijs_invalid(List<Rijbewijs> rb) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);
            Assert.Throws<BestuurderException>(() => b.ZetRijbewijsLijst(rb));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ctor2_BadEenRijbewijs_invalid(string r) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Assert.Throws<RijbewijsException>(() => new Rijbewijs(r));
        }
        #endregion

        #region Zetmethods tests
        // ZetNaam
        [Fact]
        public void Test_ZetNaam_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            b.ZetNaam("Bob");
            Assert.Equal("Bob", b.Naam);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetNaam_BadNaam_Valid(string naam) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Burak", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Assert.Throws<BestuurderException>(() => b.ZetNaam(naam));
        }

        // ZetVoornaam
        [Fact]
        public void Test_ZetVoornaam_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            b.ZetVoornaam("Bob");
            Assert.Equal("Bob", b.Voornaam);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetVoornaam_BadVoornaam_Valid(string voornaam) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Assert.Throws<BestuurderException>(() => b.ZetVoornaam(voornaam));
        }

        // ZetAdres
        [Fact]
        public void Test_ZetAdres_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Adres testAdres = new("straat", "1", "stad", 9001);
            b.ZetAdres(testAdres);
            Assert.Equal(testAdres, b.Adres);
        }
        // Adres mag null zijn
        //[Theory]
        //[InlineData(null)]
        //public void Test_ZetAdres_BadAdres_Valid(Adres adres)
        //{
        //    Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
        //    Adres a = new Adres("Straat", "1", "Stad", 9000);
        //    Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
        //    Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

        //    Assert.Throws<BestuurderException>(() => b.ZetAdres(adres));
        //}

        // ZetGeboortedatum
        [Fact]
        public void Test_ZetGeboortedatum_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            b.ZetGeboortedatum("11/05/1998");
            Assert.Equal("11/05/1998", b.Geboortedatum.ToShortDateString());
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetGeboortedatum_BadGeboortedatum_Valid(string geboortedatum) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Assert.Throws<BestuurderException>(() => b.ZetGeboortedatum(geboortedatum));
        }

        // ZetRijksregisternummer
        [Fact]
        public void Test_ZetRijksregisternummer_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            b.ZetRijksregisternummer("99.05.12-213.26");
            Assert.Equal("99.05.12-213.26", b.Rijksregisternummer);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Test_ZetRijksregisternummer_BadRijksregisternummer_Valid(string rijksregisternummer) {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Assert.Throws<RijksregisternummerCheckerException>(() => b.ZetRijksregisternummer(rijksregisternummer));
        }

        // ZetVoertuig
        [Fact]
        public void Test_ZetVoertuig_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Voertuig testVoertuig = new Voertuig(2, "ERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            b.ZetVoertuig(testVoertuig);
            Assert.Equal(testVoertuig, b.Voertuig);
            Assert.Equal(testVoertuig.Bestuurder, b);
        }
        // Voertuig mag null zijn
        //[Theory]
        //[InlineData(null)]
        //public void Test_ZetVoertuig_BadVoertuig1_InValid(Voertuig voertuig) {
        //    Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
        //    Adres a = new Adres("Straat", "1", "Stad", 9000);
        //    Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
        //    Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

        //    Assert.Throws<BestuurderException>(() => b.ZetVoertuig(voertuig));
        //}
        //[Fact]  --> PROBLEMEN
        //public void Test_ZetVoertuig_BadVoertuig2_InValid()
        //{
        //    Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
        //    Adres a = new Adres("Straat", 1, "Stad", 9000);
        //    Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
        //    Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26",  new("B"), v, tk);


        //    v.zetBestuurder(b);
        //    Voertuig testVoertuig = new Voertuig(2, "ERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);

        //    testVoertuig.zetBestuurder((b));
        //    Assert.Throws<BestuurderException>(() => b.ZetVoertuig(testVoertuig));
        //}

        // ZetTankkaart
        [Fact]
        public void Test_ZetTankkaart_Valid() {
            Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
            Adres a = new Adres("Straat", "1", "Stad", 9000);
            Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
            Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

            Tankkaart testTankkaart = new(1, DateTime.Now.AddDays(5), "5050", true);
            b.ZetTankkaart(testTankkaart);
            Assert.Equal(testTankkaart, b.Tankkaart);
        }
        // Tankkaart mag null zijn
        //[Theory]
        //[InlineData(null)]
        //public void Test_ZetTankkaart_BadTankkaart_InValid(Tankkaart tankkaart) {
        //    Tankkaart tk = new Tankkaart(1, DateTime.Now.AddDays(4), "5050", true);
        //    Adres a = new Adres("Straat", "1", "Stad", 9000);
        //    Voertuig v = new Voertuig(1, "MERRY", "THICC", "13245678957903251", "1-ABC-123", new(), "Auto", "Geel", 4);
        //    Bestuurder b = new Bestuurder("Balci", "Burak", Geslacht.M, a, "12/05/1999", "99.05.12-273.26", new(), v, tk);

        //    Assert.Throws<BestuurderException>(() => b.ZetTankkaart(tankkaart));
        //}
        #endregion
    }
}
