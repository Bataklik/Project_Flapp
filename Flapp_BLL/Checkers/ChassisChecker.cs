using Flapp_BLL.Exceptions.CheckerExceptions;
using System.Linq;

namespace Flapp_BLL.Checkers
{
    public class ChassisChecker
    {
        private string _chassisnummer;
        
        // Enkele letters mogen niet! IOQ
        public ChassisChecker(string chassisnummer)
        {
            _chassisnummer = chassisnummer;
        }

        public bool controleChassisnummer(string n)
        {
            int lengte = 17;
            string NietToegelatenLetters = "ioqIOQ";
            if (string.IsNullOrWhiteSpace(n)) { throw new ChassisnummerCheckerException("Mag niet leeg zijn"); }
            if (n.Count() != lengte) { throw new ChassisnummerCheckerException("Een chassisnummer bevat 17 karakters"); }

            foreach (char c in NietToegelatenLetters)
            {
                if (n.Contains(c))
                {
                    throw new ChassisnummerCheckerException("Bevat een niet toegelaten letter (I, O of Q)");
                }
            }
            return true;
        }        
    }
}
