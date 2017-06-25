using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOCWelcomeLetterGenerator {
    public enum Branch {
        Banking = 0,
        Haymarket = 1,
        Parramatta = 2,
        Hurstville = 3,
        Chatswood = 4,
        Melbourne = 5,
        Boxhill = 6,
        Perth = 7,
        Brisbane = 8,
        Adelaide = 9
    }
    public class BranchInfo {
        

        private static string[,] branchContact = new string[,] {
            {"Banking Dept","39-41 York Street","Sydney NSW 2000", "+61 2 8235 5888", "banking.au@bankofchina.com"},
            {"Haymarket Branch", "681 George Street","Haymarket NSW 2000", "+61 2 9212 3877", "haymarket.au@bankofchina.com" },
            {"Parramatta Branch", "143 Church Street", "Parramatta NSW 2150","+61 2 8837 6288", "parramatta.au@bankofchina.com"},
            {"Hurstville Branch", "213 Forest Road", "Hurstville NSW 2200", "+61 2 9586 3205", "hurstville.au@bankofchina.com"},
            {"Chatswood Branch", "12/409 Victoria Avenue", "Chatswood NSW 2067", "+61 2 8440 8600", "chatswood.au@bankofchina.com"},
            {"Melbourne Branch", "270 Queen Street", "Melbourne VIC 3000", "+61 3 9602 3655", "melbourne.au@bankofchina.com" },
            {"Boxhill Branch", "916-918 Whitehorse Road", "Box Hill VIC 3128", "+61 3 9899 7988", "boxhill.au@bankofchina.com"},
            {"Perth Branch", "G/F 179 St Georges Tce", "Perth WA 6000", " +61 8 9263 3777", "perth.au@bankofchina.com" },
            {"Brisbane Branch","G/F 307 Queen Street", "Brisbane QLD 4000", "+61 7 3221 3988", "brisbane.au@bankofchina.com"},
            {"Adelaide Branch","L8/1 King William Street", "Adelaide SA 5000", "+61 8 8210 898", "adelaide.au@bankofchina.com"}
        };

        private string branchName;
        private string addLine1;
        private string addLine2;
        private string phoneNumber;
        private string email;



        public BranchInfo(Branch branch) {
            branchName = branchContact[(int)branch, 0];
            addLine1 = branchContact[(int)branch, 1];
            addLine2 = branchContact[(int)branch, 2];
            phoneNumber = branchContact[(int)branch, 3];
            email = branchContact[(int)branch, 4];
        }

        public List<string> GetContact(bool inclEmail) {
            List<string> contact = new List<string>();
            contact.Add(branchName);
            contact.Add(addLine1);
            contact.Add(addLine2);
            contact.Add(phoneNumber);
            if (inclEmail) {
                contact.Add(email);
            }
            return contact;
        }

        public List<string> GetAddress() {
            List<string> address = new List<string>();
            address.Add(branchName);
            address.Add(addLine1);
            address.Add(addLine2);
            return address;
        }

        public string[,] GetFullDataSet() {
            return branchContact;
        }

        public List<string> GetBranchNameList() {
            List<string> branchNameList = new List<string>();
            for (int i = 0; i < branchContact.GetLength(0); i++) {
                branchNameList.Add(branchContact[i, 0]);
            }
            return branchNameList;
        }

    }
}
