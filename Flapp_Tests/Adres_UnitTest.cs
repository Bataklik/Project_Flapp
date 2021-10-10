using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Flapp_BLL.Models;

namespace Flapp_TESTS {
    public class Adres_UnitTest {

        #region Ctor Tests
        [Fact]
        public void Test_ctor1_Valid() {
            Adres a = new Adres("Frans Uyttenhovestraat", 91, "Gent", 9040);

            Assert.Equal("Frans Uyttenhovestraat", a.Straat);
            Assert.Equal(91, a.Huisnummer);
            Assert.Equal("Gent", a.Stad);
            Assert.Equal(9040, a.Postcode);
        }
        #endregion
    }
}
