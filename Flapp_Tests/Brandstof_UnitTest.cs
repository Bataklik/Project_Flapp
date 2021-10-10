using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Flapp_BLL.Models;

namespace Flapp_TESTS {
    public class Brandstof_UnitTest {
        #region Ctor Tests
        [Fact]
        public void Test_ctor1_Valid() {
            Brandstof b = new Brandstof("Benzine");

            Assert.Equal("Benzine", b.Naam);
        }
        #endregion
    }
}
