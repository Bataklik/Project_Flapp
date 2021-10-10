using System;
using Xunit;
using Flapp_BLL.Models;

namespace Flapp_TESTS
{
    public class Tankkaart_UnitTest {

        #region Ctor Tests
        [Fact]
        public void Test_ctor1_Valid() {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("01/01/2025"));

            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("01/01/2025"), t.Geldigheidsdatum);
        }
        #endregion
    }
}
