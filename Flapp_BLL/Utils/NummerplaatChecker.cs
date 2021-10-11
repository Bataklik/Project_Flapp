using System;
using Flapp_BLL.Models;

namespace Flapp_BLL.Utils {
    public class NummerplaatChecker {

        #region Props
        public string Nummerplaat { get; private set; }
        #endregion

        #region Constructors
        public NummerplaatChecker(string n) {
            this.Nummerplaat = n;
            Bestuurder b = new Bestuurder();
        }
        #endregion

        #region Methods
        public bool ControleNummerplaat(string n) {
            //1-ABC-123
            if (n.Count(e => char.IsDigit(e)) != 9) { throw new NummerplaatCheckerException("Een nummerplaat bevat 9 cijfers"); }
            if (r.Count(e => e == '-') != 2) { throw new NummerplaatCheckerException("Het nummerplaat is ongeldig!"); }
        }
        #endregion
    }
}

