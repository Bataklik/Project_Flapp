using Flapp_BLL.Checkers;
using Flapp_BLL.Exceptions.CheckerExceptions;
using Xunit;

namespace Flapp_TESTS.UnitTests_Models
{
    public class ChassisChecker_UnitTest
    {
        #region Methods Tests
        [Fact]
        public void Test_controleChassisnummer_Valid()
        {
            string chassisnr = "vf32akfwa44853479";
            ChassisChecker cc = new ChassisChecker(chassisnr);
            cc.controleChassisnummer(chassisnr);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("vf32akfwa4485347I")]
        [InlineData("vf32akfoa44853479")]
        [InlineData("vf32aQfwa44853479")]
        [InlineData("vf32akfa44853479")]
        [InlineData("vf32aqfwa44853479")]
        [InlineData("vf32aOfwa44853479")]
        [InlineData("vf32aifwa44853479")]
        [InlineData("vf32akfwa44853479h")]
        public void Test_ControleChassisnummer_InValid(string chassisnr)
        {
            ChassisChecker cc = new ChassisChecker(chassisnr);
            Assert.Throws<ChassisnummerCheckerException>(() => cc.controleChassisnummer(chassisnr));
        }
        #endregion
    }
}
