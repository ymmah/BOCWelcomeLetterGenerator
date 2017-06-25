using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOCWelcomeLetterGenerator {
    public class Account {
        private List<string> accountInfo;
        

        public Account(string accountType, string ccyCode, string intlNumber, string domesticNumber = "") {
            accountInfo = new List<string> { accountType, ccyCode, intlNumber, domesticNumber };
        }

        public List<string> GetAccountInfo() {
            return accountInfo;
        }

    }
}
