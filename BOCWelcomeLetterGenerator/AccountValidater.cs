using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOCWelcomeLetterGenerator {
    static public class AccountValidater {
        static public bool IntlAcctNumber(string accountNumber) {

            if (!accountNumber.All(Char.IsNumber)) {
                return false;
            }

            if (accountNumber.Length != 15) {
                return false;
            }

            // The real validation algorithm is hidden due to confidentiality
            // Only number of digits is checked here

            return true;
        }

        static public bool DomesticAcctNumber (string accountNumber) {
            if (!accountNumber.All(Char.IsNumber)) {
                return false;
            }

            if (accountNumber.Length == 9) {
                return true;
            } else {
                return false;
            }
        }
    }
}
