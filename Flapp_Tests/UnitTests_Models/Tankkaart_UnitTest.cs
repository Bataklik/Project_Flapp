using System;
using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ModelExpections;

namespace Flapp_TESTS.UnitTests_Models
{
    public class Tankkaart_UnitTest
    {

        #region Ctor Tests
        [Fact]
        public void Test_ctor_Valid()
        {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"));

            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
        }
        //Bad Kaartnr
        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void Test_ctor_BadKaartnr_InValid(int nr)
        {
            Assert.Throws<TankkaartException>(() => new Tankkaart(nr, DateTime.Parse("06/08/2025")));
        }

        //Bad Geldigheidsdatum
        [Theory]
        [InlineData("10/12/2021")]
        [InlineData("10/12/1999")]
        public void Test_ctor_BadGeldigheidsdatum_InValid(DateTime dt)
        {
            Assert.Throws<TankkaartException>(() => new Tankkaart(420, dt));
        }
        #endregion

        #region ZetMethods Tests
        //ZetTankkaartNr
        [Fact]
        public void Test_ZetTankkaartNr_Valid()
        {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"));
            t.ZetKaartnummer(420);
            Assert.Equal(420, t.Kaartnummer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-420)]
        public void Test_ZetTankkaartNr_InValid(int nr)
        {
            Tankkaart t = new Tankkaart(1, DateTime.Parse("06/08/2025"));
            Assert.Throws<TankkaartException>(() => t.ZetKaartnummer(nr)); ;
        }

        [Fact]
        public void Test_ZetGeldigheidsdatum_Valid()
        {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"));
            t.ZetGeldigheidsdatum(DateTime.Parse("06/08/2025"));
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
        }
        [Theory]
        [InlineData("09/12/2021")]
        [InlineData("10/12/1991")]
        public void Test_ctor_ZetGeldigheidsdatum_InValid(DateTime dt)
        {
            Tankkaart t = new Tankkaart(1, DateTime.Parse("06/08/2025"));
            Assert.Throws<TankkaartException>(() => t.ZetGeldigheidsdatum(dt));
        }

        //ZetGeblokkeerd
        [Fact]
        public void Test_ZetGeblokkeerd_Valid()
        {
            Tankkaart t = new Tankkaart(DateTime.Parse("06/08/2025"), "20", true) ;
            t.ZetGeblokkeerd(false);
            Assert.False(t.Geblokkeerd);
        }

        [Theory]
        [InlineData(true)]
        public void Test_ZetGeblokkeerd_InValid(bool b)
        {
            Tankkaart t = new Tankkaart(DateTime.Parse("06/08/2025"), "1", true);
            Assert.Throws<TankkaartException>(() => t.ZetGeblokkeerd(b));
        }
        #endregion
    }
}
