using Xunit;
using Flapp_BLL.Exceptions.CheckerExceptions;
using Flapp_BLL.Checkers;

namespace Flapp_TESTS.UnitTests_Models
{
    public class NummerPlaatChecker_UnitTest
    {
        #region Methods Tests
        [Fact]
        public void Test_ControleNummerplaat_Valid()
        {
            string nrplaat = "1-ABC-123";
            NummerplaatChecker nc = new NummerplaatChecker(nrplaat);
            nc.ControleNummerplaat(nrplaat);
        }

        [Fact]
        public void Test_ControleEersteGroep1_Valid()
        {
            string nrplaat = "1-ABC-123";
            NummerplaatChecker nc = new NummerplaatChecker(nrplaat);
            nc.ControleEersteGroep(nrplaat);
        }

        [Fact]
        public void Test_ControleEersteGroep2_Valid()
        {
            string nrplaat = "2-ABC-123";
            NummerplaatChecker nc = new NummerplaatChecker(nrplaat);
            nc.ControleEersteGroep(nrplaat);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("1.ABC.ADE")]
        [InlineData("1.ABC-ABC")]
        [InlineData("5-ABC-ABC")]
        [InlineData("5-ABC-123")]
        public void Test_ControleNummerplaat_InValid(string nrplaat)
        {
            NummerplaatChecker nc = new NummerplaatChecker(nrplaat);
            Assert.Throws<NummerplaatCheckerException>(() => nc.ControleNummerplaat(nrplaat));
        }

        [Theory]
        [InlineData("5-ABC-123")]
        public void Test_ControleEersteGroep_InValid(string nrplaat)
        {
            NummerplaatChecker nc = new NummerplaatChecker(nrplaat);
            bool v = nc.ControleEersteGroep(nrplaat);
        }
        #endregion
    }
}
