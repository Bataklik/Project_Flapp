using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions;

namespace Flapp_TESTS
{
    public class Adres_UnitTest
    {
        #region Ctor Tests
        [Fact]
        public void Test_ctor_Valid()
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);

            Assert.Equal("Frans Uyttenhovestraat", a.Straat);
            Assert.Equal("91", a.Huisnummer);
            Assert.Equal("Gent", a.Stad);
            Assert.Equal(9040, a.Postcode);
        }
        // Bad Straat
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Test_ctor_BadStraat_InValid(string straat)
        {
            Assert.Throws<AdresException>(() => new Adres(straat, "91", "Gent", 9040));
        }
        // Bad Huisnummer
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_ctor_BadHuisnummer_InValid(string huisnr)
        {
            Assert.Throws<AdresException>(() => new Adres("Frans Uyttenhovestraat", huisnr, "Gent", 9040));
        }
        // Bad Stad
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Test_ctor_BadStad_InValid(string stad)
        {
            Assert.Throws<AdresException>(() => new Adres("Frans Uyttenhovestraat", "91", stad, 9040));
        }
        // Bad Postcode
        [Theory]
        [InlineData(0)]
        [InlineData(-70)]
        [InlineData(999)]
        [InlineData(10000)]
        public void Test_ctor_BadPostcode_InValid(int postc)
        {
            Assert.Throws<AdresException>(() => new Adres("Frans Uyttenhovestraat", "1", "Gent", postc));
        }
        #endregion

        #region ZetMethods Tests
        // ZetStraat
        [Fact]
        public void Test_ZetStraat_Valid()
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            a.ZetStraat("Bobstraat");
            Assert.Equal("Bobstraat", a.Straat);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_ZetStraat_BadStraat_InValid(string straat)
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            Assert.Throws<AdresException>(() => a.ZetStraat(straat));
        }
        // ZetHuisnummer
        [Fact]
        public void Test_ZetHuisnummer_Valid()
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            a.ZetHuisnummer("99");
            Assert.Equal("99", a.Huisnummer);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_ZetHuisnummer_BadHuisnummer_InValid(string huisnr)
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            Assert.Throws<AdresException>(() => a.ZetHuisnummer(huisnr));
        }
        // ZetStad
        [Fact]
        public void Test_ZetStad_Valid()
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            a.ZetStad("Ghent");
            Assert.Equal("Ghent", a.Stad);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_ZetStad_BadStad_InValid(string stad)
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            Assert.Throws<AdresException>(() => a.ZetStraat(stad));
        }
        // Postcode
        [Fact]
        public void Test_ZetPostcode_Valid()
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            a.ZetPostcode(9000);
            Assert.Equal(9000, a.Postcode);
        }
        [Theory]
        [InlineData(999)]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(10000)]
        public void Test_ZetPostcode_BadPostcode_InValid(int postc)
        {
            Adres a = new Adres("Frans Uyttenhovestraat", "91", "Gent", 9040);
            Assert.Throws<AdresException>(() => a.ZetPostcode(postc));
        }
        #endregion
    }
}
