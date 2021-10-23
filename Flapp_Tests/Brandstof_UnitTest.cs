using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ModelExpections;

namespace Flapp_TESTS
{
    public class Brandstof_UnitTest
    {
        #region Ctor Tests
        [Fact]
        public void Test_ctor_Naam_Valid()
        {
            Brandstof b = new Brandstof("Benzine");
            Assert.Equal("Benzine", b.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void Test_ctor_Naam_InValid(string naam)
        {
            var ex = Assert.Throws<BrandstofException>(() => new Brandstof(naam));
            Assert.Equal("Brandstof naam mag niet leeg zijn!", ex.Message);
        }
        #endregion

        #region ZetMethods Tests
        [Fact]
        public void Test_ZetBrandstofNaam_Valid()
        {
            Brandstof b = new Brandstof("enzine");
            b.ZetBrandstofNaam("Benzine");
            Assert.Equal("Benzine", b.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void Test_ZetBrandstofNaam_BadNaam_InValid(string naam)
        {
            Brandstof b = new Brandstof("Benzine");
            var ex = Assert.Throws<BrandstofException>(() => b.ZetBrandstofNaam(naam));
            Assert.Equal("Brandstof naam mag niet leeg zijn!", ex.Message);
        }
        #endregion
    }
}
