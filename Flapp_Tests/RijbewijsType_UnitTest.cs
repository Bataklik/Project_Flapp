using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Models;
using Xunit;

namespace Flapp_TESTS
{
    public class RijbewijsType_UnitTest
    {
        #region Ctor Tests
        [Fact]
        public void Test_ctor_Naam_Valid()
        {
            RijbewijsType rb = new RijbewijsType("B");
            Assert.Equal("B", rb.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void Test_ctor_Naam_InValid(string naam)
        {
            var ex = Assert.Throws<RijbewijsTypeException>(() => new RijbewijsType(naam));
            Assert.Equal("RijbewijsType: ZetNaam: Naam mag niet leeg zijn!", ex.Message);
        }
        #endregion

        #region ZetMethods Tests
        [Fact]
        public void Test_ZetId_Valid()
        {
            RijbewijsType rb = new RijbewijsType(1, "B");
            rb.ZetId(2);
            Assert.Equal(2, rb.Id);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_BadId_InValid(int id)
        {
            RijbewijsType rb = new RijbewijsType("B");
            Assert.Throws<RijbewijsTypeException>(() => rb.ZetId(id));
        }

        [Fact]
        public void Test_ZetNaam_Valid()
        {
            RijbewijsType rb = new RijbewijsType("B");
            rb.ZetNaam("A");
            Assert.Equal("A", rb.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("a")]
        public void Test_ZetNaam_BadNaam_InValid(string naam)
        {
            RijbewijsType rb = new RijbewijsType("B");
            Assert.Throws<RijbewijsTypeException>(() => rb.ZetNaam(naam));
        }
        #endregion
    }
}
