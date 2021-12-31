using System;
using System.Linq;
using System.Runtime;
using Flapp_BLL.Exceptions.CheckerExceptions;

namespace Flapp_BLL.Checkers
{
    public class NummerplaatChecker
    {

        #region Props
        public string Nummerplaat { get; private set; }
        #endregion

        #region Constructors
        public NummerplaatChecker(string n)
        {
            Nummerplaat = n;
        }
        #endregion

        #region Methods
        public bool ControleNummerplaat(string n)
        {
            //1-ABC-123
            if (string.IsNullOrEmpty(n)) { throw new NummerplaatCheckerException("Een nummerplaat mag niet leeg zijn!"); }
            if (n.Count(e => char.IsDigit(e)) != 4) { throw new NummerplaatCheckerException("Een nummerplaat bevat 4 cijfers"); }
            if (n.Count(e => char.IsLetter(e)) != 3) { throw new NummerplaatCheckerException("Een nummerplaat bevat 3 letters"); }
            if (n.Count(e => e == '-') != 2) { throw new NummerplaatCheckerException("Het nummerplaat is ongeldig!"); }
            if (!ControleEersteGroep(n)) { throw new NummerplaatCheckerException("Controle op eerste groep van het nummerplaat is ongeldig!"); }
            if (!ControleKoppelteken(n)) { throw new NummerplaatCheckerException("Controle op koppeltekens van het nummerplaat is ongeldig!"); }
            if (!ControleLetters(n)) { throw new NummerplaatCheckerException("Controle op letters van het nummerplaat is ongeldig!"); }
            if (!ControleCijfers(n)) { throw new NummerplaatCheckerException("Controle op cijfers van het nummerplaat is ongeldig!"); }
            return true;
        }

        public bool ControleEersteGroep(string n)
        {
            string eersteGroep = n[0].ToString();
            if (eersteGroep == "1" || eersteGroep == "2")
                return true;
            else
                return false;
        }
        public bool ControleKoppelteken(string n)
        {
            string eerste = n[1].ToString();
            string tweede = n[5].ToString();
            if (eerste == "-" && tweede == "-")
                return true;
            else
                return false;
        }
        public bool ControleLetters(string n)
        {
            char eerste = n[2];
            char tweede = n[3];
            char derde = n[4];

            if (char.IsLetter(eerste) == true && char.IsLetter(tweede) == true && char.IsLetter(derde) == true)
                return true;
            else
                return false;
        }
        public bool ControleCijfers(string n)
        {
            char eerste = n[6];
            char tweede = n[7];
            char derde = n[8];

            if (char.IsDigit(eerste) == true && char.IsDigit(tweede) == true && char.IsDigit(derde) == true)
                return true;
            else
                return false;
        }

        

        #endregion
    }
}

