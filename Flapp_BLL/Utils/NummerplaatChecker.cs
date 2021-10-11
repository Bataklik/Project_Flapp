using System;
using System.Linq;
using System.Runtime;
using Flapp_BLL.Exceptions;

namespace Flapp_BLL.Utils {
    public class NummerplaatChecker {

        #region Props
        public string Nummerplaat { get; private set; }
        #endregion

        #region Constructors
        public NummerplaatChecker(string n) {
            this.Nummerplaat = n;
            
        }
        #endregion

        #region Methods
        public bool ControleNummerplaat(string n) {
            //1-ABC-123
            if (n.Count(e => char.IsDigit(e)) != 4) { throw new NummerplaatCheckerException("Een nummerplaat bevat 4 cijfers"); }
            if (n.Count(e => !char.IsDigit(e)) != 5) { throw new NummerplaatCheckerException("Een nummerplaat bevat 3 letters"); }
            if (n.Count(e => e == '-') != 2) { throw new NummerplaatCheckerException("Het nummerplaat is ongeldig!"); }
            if (!ControleEersteGroep(n)) { throw new NummerplaatCheckerException("Controle op eerste groep van het nummerplaat is ongeldig!"); }
            return true;
        }

        private bool ControleEersteGroep(string n) {
            string eersteGroep = n[0].ToString();
            if (eersteGroep == "1" || eersteGroep == "2")
                return true;
            else
                return false;
        }
        #endregion
    }
}

