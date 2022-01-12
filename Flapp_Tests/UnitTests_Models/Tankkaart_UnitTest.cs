using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Flapp_TESTS.UnitTests_Models {
    public class Tankkaart_UnitTest {

        #region Ctor Tests
        // Ctor1
        [Fact]
        public void Test_ctor1_Valid() {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"), "4050", false);

            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
            Assert.Equal("4050", t.Pincode);
            Assert.False(t.Geblokkeerd);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void Test_ctor1_Kaartnummer_InValid(int nr) {
            Assert.Throws<TankkaartException>(() => new Tankkaart(nr, DateTime.Parse("06/08/2025"), "4050", false));
        }


        [Theory]
        [InlineData("10/12/2021")]
        [InlineData("10/12/1999")]
        public void Test_ctor1_Geldigheidsdatum_InValid(DateTime dt) {
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, dt, "4050", false));
        }

        [Theory]
        [InlineData("111")]
        [InlineData("11111")]
        public void Test_ctor1_Pincode_InValid(string pincode) {
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, DateTime.Parse("06/08/2025"), pincode, false));
        }

        // Ctor2
        [Fact]
        public void Test_ctor2_Valid() {
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"), "4050", brandstoffen,false);

            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
            Assert.Equal("4050", t.Pincode);
            Assert.Equal(brandstoffen, t.Brandstoffen);
            Assert.False(t.Geblokkeerd);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void Test_ctor2_Kaartnummer_InValid(int nr) {
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(nr, DateTime.Parse("06/08/2025"), "4050", brandstoffen, false));
        }


        [Theory]
        [InlineData("10/12/2021")]
        [InlineData("10/12/1999")]
        public void Test_ctor2_Geldigheidsdatum_InValid(DateTime dt) {
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, dt, "4050", brandstoffen, false));
        }

        [Theory]
        [InlineData("111")]
        [InlineData("11111")]
        public void Test_ctor2_Pincode_InValid(string pincode) {
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, DateTime.Parse("06/08/2025"), pincode, brandstoffen, false));
        }

        [Theory]
        [InlineData(null)]
        public void Test_ctor2_Brandstoffen_InValid(List<Brandstof> brandstoffen) {
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, DateTime.Parse("06/08/2025"), "4050", brandstoffen, false));
        }

        // Ctor3
        [Fact]
        public void Test_ctor3_Valid() {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"), "4050", brandstoffen, bestuurder,false);

            Assert.Equal(420, t.Kaartnummer);
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
            Assert.Equal("4050", t.Pincode);
            Assert.Equal(brandstoffen, t.Brandstoffen);
            Assert.Equal(bestuurder, t.Bestuurder);
            Assert.False(t.Geblokkeerd);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void Test_ctor3_Kaartnummer_InValid(int nr) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(nr, DateTime.Parse("06/08/2025"), "4050", brandstoffen, bestuurder, false));
        }


        [Theory]
        [InlineData("10/12/2021")]
        [InlineData("10/12/1999")]
        public void Test_ctor3_Geldigheidsdatum_InValid(DateTime dt) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, dt, "4050", brandstoffen, bestuurder, false));
        }

        [Theory]
        [InlineData("111")]
        [InlineData("11111")]
        public void Test_ctor3_Pincode_InValid(string pincode) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, DateTime.Parse("06/08/2025"), pincode, brandstoffen, bestuurder, false));
        }

        [Theory]
        [InlineData(null)]
        public void Test_ctor3_Brandstoffen_InValid(List<Brandstof> brandstoffen) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Assert.Throws<TankkaartException>(() => new Tankkaart(1, DateTime.Parse("06/08/2025"), "4050", brandstoffen, bestuurder, false));
        }

        // Ctor4
        [Fact]
        public void Test_ctor4_Valid() {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Tankkaart t = new Tankkaart(DateTime.Parse("06/08/2025"), "4050", false, brandstoffen, bestuurder);

            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
            Assert.Equal("4050", t.Pincode);
            Assert.Equal(brandstoffen, t.Brandstoffen);
            Assert.Equal(bestuurder, t.Bestuurder);
            Assert.False(t.Geblokkeerd);
        }

        [Theory]
        [InlineData("10/12/2021")]
        [InlineData("10/12/1999")]
        public void Test_ctor4_Geldigheidsdatum_InValid(DateTime dt) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(dt, "4050", true, brandstoffen, bestuurder));
        }

        [Theory]
        [InlineData("111")]
        [InlineData("11111")]
        public void Test_ctor4_Pincode_InValid(string pincode) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Brandstof brandstof = new Brandstof(1, "Diesel");
            List<Brandstof> brandstoffen = new List<Brandstof>();
            brandstoffen.Add(brandstof);
            Assert.Throws<TankkaartException>(() => new Tankkaart(DateTime.Parse("06/08/2025"), pincode, false, brandstoffen, bestuurder));
        }

        [Theory]
        [InlineData(null)]
        public void Test_ctor4_Brandstoffen_InValid(List<Brandstof> brandstoffen) {
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            Assert.Throws<TankkaartException>(() => new Tankkaart(DateTime.Parse("06/08/2025"), "4050", false, brandstoffen, bestuurder));
        }
        #endregion

        #region ZetMethods Tests
        [Fact]
        public void Test_ZetTankkaartNr_Valid() {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"), "5050", true);
            t.ZetKaartnummer(420);
            Assert.Equal(420, t.Kaartnummer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-420)]
        public void Test_ZetTankkaartNr_InValid(int nr) {
            Tankkaart t = new Tankkaart(1, DateTime.Parse("06/08/2025"), "5050", true);
            Assert.Throws<TankkaartException>(() => t.ZetKaartnummer(nr)); ;
        }

        [Fact]
        public void Test_ZetGeldigheidsdatum_Valid() {
            Tankkaart t = new Tankkaart(420, DateTime.Parse("06/08/2025"), "5050", true);
            t.ZetGeldigheidsdatum(DateTime.Parse("06/08/2025"));
            Assert.Equal(DateTime.Parse("06/08/2025"), t.Geldigheidsdatum);
        }

        [Theory]
        [InlineData("09/12/2021")]
        [InlineData("10/12/1991")]
        public void Test_ctor_ZetGeldigheidsdatum_InValid(DateTime dt) {
            Tankkaart t = new Tankkaart(1, DateTime.Parse("06/08/2025"), "5050", true);
            Assert.Throws<TankkaartException>(() => t.ZetGeldigheidsdatum(dt));
        }

        [Fact]
        public void Test_ZetGeblokkeerd_Valid() {
            Tankkaart t = new Tankkaart(1,DateTime.Parse("06/08/2025"), "2800", true);
            t.ZetGeblokkeerd(false);
            Assert.False(t.Geblokkeerd);
        }
        #endregion

        #region Methods
        [Fact]
        public void Test_VoegBestuurderToe_Valid() {
            Tankkaart tankkaart = new Tankkaart(1, DateTime.Now.AddDays(5), "5050", true);
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            tankkaart.VoegBestuurderToe(bestuurder);
        }

        [Theory]
        [InlineData(null)]
        public void Test_VoegBestuurderToe_InValid(Bestuurder bestuurder) {
            Tankkaart tankkaart = new Tankkaart(1, DateTime.Now.AddDays(5), "5050", true);
            Assert.Throws<TankkaartException>(() => tankkaart.VoegBestuurderToe(bestuurder));
        }

        [Fact]
        public void Test_VerwijderBestuurder_Valid() {
            Tankkaart tankkaart = new Tankkaart(1, DateTime.Now.AddDays(5), "5050", true);
            Bestuurder bestuurder = new Bestuurder(1, "Declerck", "Tibo", "06/08/1999");
            tankkaart.VoegBestuurderToe(bestuurder);
            tankkaart.VerwijderBestuurder(bestuurder);
        }

        [Theory]
        [InlineData(null)]
        public void Test_VerwijderBestuurder_InValid(Bestuurder bestuurder) {
            Tankkaart tankkaart = new Tankkaart(1, DateTime.Now.AddDays(5), "5050", true);
            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBestuurder(bestuurder));
        }
        #endregion
    }
}
