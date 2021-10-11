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
            if (n.Count(e => char.IsDigit(e)) != 9) { throw new NummerplaatCheckerException("Een nummerplaat bevat 9 cijfers"); }
            if (n.Count(e => e == '-') != 2) { throw new NummerplaatCheckerException("Het nummerplaat is ongeldig!"); }
            if (!ControleEersteGroep(n)) { throw new NummerplaatCheckerException("Controle op eerste groep van het nummerplaat is ongeldig!"); }
            if (!ControleTweedeGroep(n)) { throw new NummerplaatCheckerException("Tweede groep van het nummerplaat moet bestaan uit letters!"); }
            if (!ControleDerdeGroep(n)) { throw new NummerplaatCheckerException("Derde groep van het nummerplaat moet bestaan uit cijfers!"); }
            return true;
        }

        private bool ControleEersteGroep(string n) {
            string eersteGroep = n[0].ToString();
            if (eersteGroep == "1" || eersteGroep == "2")
                return true;
            else
                return false;
        }

        private bool ControleTweedeGroep(string n) {
            if (!Char.IsDigit(n[2]) || !Char.IsDigit(n[3]) || !Char.IsDigit(n[4]))
                return true;
            else
                return false;
        }

        private bool ControleDerdeGroep(string n) {
            if (Char.IsDigit(n[6]) && Char.IsDigit(n[7]) && Char.IsDigit(n[8]))
                return true;
            else
                return false;
        }
        #endregion
    }
}

