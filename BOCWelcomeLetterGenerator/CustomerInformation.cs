using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOCWelcomeLetterGenerator {
    public class CustomerInformation {
        const int LINE_OF_ADDRESS = 3;

        private string customerName;
        private bool hasAddress;
        private List<string> customerAddress;

        public CustomerInformation(string name) {
            customerName = name;
            hasAddress = false;
        }
        
        public CustomerInformation(string name, string addLine1, string addLine2, string addLine3) {
            customerName = name;
            customerAddress = new List<string> { addLine1, addLine2, addLine3 };
            hasAddress = true;
        }

        public string GetName() {
            return customerName;
        }

        public List<string> GetAddress() {
            return customerAddress;
        }

        public bool HasAddress() {
            return hasAddress;
        }

    }
}
