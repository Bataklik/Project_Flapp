using System;
using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ModelExpections;

namespace Flapp_TESTS
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
            var ex = Assert.Throws<TankkaartException>(() => new Tankkaart(nr, DateTime.Parse("06/08/2025")));
            Assert.Equal("Tankkaart kaartnummer is kleiner dan 1!", ex.Message);
        }

        //Bad Geldigheidsdatum
        [Theory]
        [InlineData("10/12/2021")]
        [InlineData("10/12/1999")]
        public void Test_ctor_BadGeldigheidsdatum_InValid(DateTime dt)
        {
            var ex = Assert.Throws<TankkaartException>(() => new Tankkaart(420, dt));
            Assert.Equal("Tankkaart Geldigheidsdatum mag niet kleiner zijn dan vandaag!", ex.Message);
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
            var ex = Assert.Throws<TankkaartException>(() => t.ZetKaartnummer(nr)); ;
            Assert.Equal("Tankkaart kaartnummer is kleiner dan 1!", ex.Message);
        }

        //ZetGeldigheidsdatum
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
            var ex = Assert.Throws<TankkaartException>(() => t.ZetGeldigheidsdatum(dt));
            Assert.Equal("Tankkaart Geldigheidsdatum mag niet kleiner zijn dan vandaag!", ex.Message);
        }

        //ZetGeblokkeerd
        [Fact]
        public void Test_ZetGeblokkeerd_Valid()
        {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"), false);
            t.ZetGeblokkeerd(false);
            Assert.False(t.Geblokkeerd);
        }

        [Theory]
        [InlineData(true)]
        public void Test_ZetGeblokkeerd_InValid(bool b)
        {
            Tankkaart t = new Tankkaart(1, DateTime.Parse("06/08/2025"));
            var ex = Assert.Throws<TankkaartException>(() => t.ZetGeblokkeerd(b));
            Assert.Equal("Tankkaart is al geblokkeerd", ex.Message);
        }
        #endregion
    }
}
