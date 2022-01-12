using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Models;
using Xunit;

namespace Flapp_TESTS.UnitTests_Models
{
    public class Rijbewijs_UnitTest
    {
        #region Ctor Tests
        [Fact]
        public void Test_ctor_Naam_Valid()
        {
            Rijbewijs rb = new Rijbewijs("B");
            Assert.Equal("B", rb.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void Test_ctor_Naam_InValid(string naam)
        {
            var ex = Assert.Throws<RijbewijsException>(() => new Rijbewijs(naam));
            Assert.Equal("RijbewijsType: ZetNaam: Naam mag niet leeg zijn!", ex.Message);
        }
        #endregion

        #region ZetMethods Tests
        [Fact]
        public void Test_ZetId_Valid()
        {
            Rijbewijs rb = new Rijbewijs(1, "B");
            rb.ZetId(2);
            Assert.Equal(2, rb.Id);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_BadId_InValid(int id)
        {
            Rijbewijs rb = new Rijbewijs("B");
            Assert.Throws<RijbewijsException>(() => rb.ZetId(id));
        }

        [Fact]
        public void Test_ZetNaam_Valid()
        {
            Rijbewijs rb = new Rijbewijs("B");
            rb.ZetNaam("A");
            Assert.Equal("A", rb.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void Test_ZetNaam_BadNaam_InValid(string naam)
        {
            Rijbewijs rb = new Rijbewijs("B");
            Assert.Throws<RijbewijsException>(() => rb.ZetNaam(naam));
        }
        #endregion
    }
}
