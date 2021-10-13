using Flapp_BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Utils
{
    public class ChassisChecker
    {
        private string _chassisnummer;

        public ChassisChecker(string chassisnummer)
        {
            _chassisnummer = chassisnummer;
        }

        public bool controleChassisnummer(string n)
        {
            if (string.IsNullOrWhiteSpace(n)) { throw new ChassisnummerCheckerException("Mag niet leeg zijn"); }
            if (n.Count() != 17) { throw new ChassisnummerCheckerException("Een chassisnummer bevat 17 karakters"); }
            return true;
        }

        private bool ControleEersteGroep(string n)
        {
            string eersteGroep = n[0].ToString();
            if (eersteGroep.All(char.IsDigit) == true)
                return true;
            else
                return false;
        }
        private bool ControleTweedeGroep(string n)
        {
            string eerste = (n[1].ToString());
            string tweede = (n[2].ToString());
            string derde = (n[3].ToString());
            string vierde = (n[4].ToString());
            if (!eerste.All(char.IsDigit) == true && !tweede.All(char.IsDigit) == true && !derde.All(char.IsDigit) == true && !vierde.All(char.IsDigit) == true)
                return true;
            else
                return false;
        }
        private bool ControleDerdeGroep(string n)
        {
            string eerste = (n[5].ToString());
            string tweede = (n[6].ToString());
            if (eerste.All(char.IsDigit) == true && tweede.All(char.IsDigit))
                return true;
            else
                return false;
        }
        private bool ControleVierdeGroep(string n)
        {
            string eerste = (n[7].ToString());
            string tweede = (n[8].ToString());
            string derde = (n[9].ToString());
            string vierde = (n[10].ToString());
            if (!eerste.All(char.IsDigit) == true && !tweede.All(char.IsDigit) == true && !derde.All(char.IsDigit) == true && !vierde.All(char.IsDigit) == true)
                return true;
            else
                return false;
        }
        private bool ControleVijfdeGroep(string n)
        {
            string eerste = (n[11].ToString());
            string tweede = (n[12].ToString());
            string derde = (n[13].ToString());
            string vierde = (n[14].ToString());
            string vijfde = (n[15].ToString());
            string zesde = (n[16].ToString());
            string tweedeGroep = eerste + tweede + derde + vierde;
            if (eerste.All(char.IsDigit) == true && tweede.All(char.IsDigit) == true && derde.All(char.IsDigit) == true && vierde.All(char.IsDigit) == true &&
                vijfde.All(char.IsDigit) == true && zesde.All(char.IsDigit) == true)
                return true;
            else
                return false;
        }
    }
}
