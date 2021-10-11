using System;
using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions;

namespace Flapp_TESTS
{
    public class Tankkaart_UnitTest {

        #region Ctor Tests
        [Fact]
        public void Test_ctor_Valid() {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"));

            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void Test_ctor_BadNumber_InValid(int nr) {
            Assert.Throws<TankkaartException>(() => new Tankkaart(nr));
        }
        #endregion

        #region ZetMethods Tests
        [Fact]
        public void Test_ZetTankkaartNr_Valid() {
            Tankkaart t = new Tankkaart(420);
            t.ZetKaartnummer(420);
            Assert.Equal(420, t.Kaartnummer);
        }

        [Fact]
        public void Test_ZetTankkaartNrGeldigheidsdatum_Valid() {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"));
            t.ZetKaartnummer(420);
            t.ZetGeldigheidsdatum(DateTime.Parse("06/08/2025"));
            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
        }
        #endregion
    }
}
