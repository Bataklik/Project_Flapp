using Flapp_BLL.Checkers;
using Flapp_BLL.Exceptions.CheckerExceptions;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flapp_TESTS {
    public class RijskregisternummerChecker_UnitTest {
        #region Methods Tests
        [Fact]
        public void Test_ControleRijksregisternummer_Valid() {
            string r = "99.05.12-273.26";
            DateTime dt = DateTime.Parse("12/05/1999");
            Geslacht g = Geslacht.M;
            RijksregisternummerChecker rc = new RijksregisternummerChecker(r, dt, g);
            rc.ControleRijksregisternummer(r, dt, g);
        }

        [Fact]
        public void Test_ControleEersteGroep_Valid() {
            string r = "99.05.12-273.26";
            DateTime dt = DateTime.Parse("12/05/1999");
            Geslacht g = Geslacht.M;
            RijksregisternummerChecker rc = new RijksregisternummerChecker(r, dt, g);
            rc.ControleEersteGroep(r, dt);
        }

        [Fact]
        public void Test_ControleTweedeGroep_Valid() {
            string r = "99.05.12-273.26";
            DateTime dt = DateTime.Parse("12/05/1999");
            Geslacht g = Geslacht.M;
            RijksregisternummerChecker rc = new RijksregisternummerChecker(r, dt, g);
            rc.ControleTweedeGroep(r, g);
        }
        
        #endregion
    }
}
