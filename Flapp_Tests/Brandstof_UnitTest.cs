﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions;

namespace Flapp_TESTS
{
    public class Brandstof_UnitTest
    {
        #region Ctor Tests
        [Fact]
        public void Test_ctor_Valid()
        {
            Brandstof b = new Brandstof("Benzine");

            Assert.Equal("Benzine", b.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public void Test_ctor_BadNaam_InValid(string naam)
        {
            Assert.Throws<BrandstofException>(() => new Brandstof(naam));
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
            Brandstof b = new Brandstof("enzine");
            Assert.Throws<BrandstofException>(() => b.ZetBrandstofNaam(naam));
        }
        #endregion
    }
}
