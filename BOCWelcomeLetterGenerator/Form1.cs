using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfFileWriter;

namespace BOCWelcomeLetterGenerator {
    public partial class Form1 : Form {
        private const int NUM_OF_ADDRESS_LINES = 3;
        private const int MAX_ACCOUNTS_SUPPORTED = 4;

        private string[] accountTypes = {
            "",                         // 0
            "Demand Deposit Account",   // 1
            "Overseas Student Account", // 2
            "Online Saver Account",     // 3
            "Cheque Account",           // 4
            "CCE Account",              // 5
            "Saving Account",           // 6
            "Term Deposit Account"      // 7
        };

        private string[] ccyCodes = {
            "",     // 0
            "AUD",  // 1
            "USD",  // 2
            "CNY",  // 3
            "HKD",  // 4
            "JPY",  // 5
            "EUR",  // 6
            "XXX"   // 7
        };

        System.Windows.Forms.TextBox[] addressTextBoxes;
        System.Windows.Forms.CheckBox[] accountCheckBoxes;
        System.Windows.Forms.ComboBox[] accountTypeComboBoxes;
        System.Windows.Forms.ComboBox[] accountCcyComboBoxes;
        System.Windows.Forms.Label[] accountLongNumberLabels;
        System.Windows.Forms.TextBox[] accountLongNumberTextBoxes;
        System.Windows.Forms.Label[] accountShortNumberLabels;
        System.Windows.Forms.TextBox[] accountShortNumberTextBoxes;


        public Form1() {
            InitializeComponent();
            addressTextBoxes = new System.Windows.Forms.TextBox[NUM_OF_ADDRESS_LINES] {
                addressLineTextBox1, addressLineTextBox2, addressLineTextBox3
            };
            accountCheckBoxes = new CheckBox[MAX_ACCOUNTS_SUPPORTED] {
                accountCheckBox1, accountCheckBox2, accountCheckBox3, accountCheckBox4
            };
            accountTypeComboBoxes = new ComboBox[MAX_ACCOUNTS_SUPPORTED] {
                accountTypeComboBox1, accountTypeComboBox2, accountTypeComboBox3, accountTypeComboBox4
            };
            accountCcyComboBoxes = new ComboBox[MAX_ACCOUNTS_SUPPORTED] {
                accountCcyComboBox1, accountCcyComboBox2, accountCcyComboBox3, accountCcyComboBox4
            };
            accountLongNumberLabels = new Label[MAX_ACCOUNTS_SUPPORTED] {
                accountLongNumberLabel1, accountLongNumberLabel2, accountLongNumberLabel3, accountLongNumberLabel4
            };
            accountLongNumberTextBoxes = new System.Windows.Forms.TextBox[MAX_ACCOUNTS_SUPPORTED] {
                accountLongNumberTextBox1, accountLongNumberTextBox2, accountLongNumberTextBox3, accountLongNumberTextBox4
            };
            accountShortNumberLabels = new Label[MAX_ACCOUNTS_SUPPORTED] {
                accountShortNumberLabel1, accountShortNumberLabel2, accountShortNumberLabel3, accountShortNumberLabel4
            };
            accountShortNumberTextBoxes = new System.Windows.Forms.TextBox[MAX_ACCOUNTS_SUPPORTED] {
                accountShortNumberTextBox1, accountShortNumberTextBox2, accountShortNumberTextBox3, accountShortNumberTextBox4
            };
            for (int i = 0; i < MAX_ACCOUNTS_SUPPORTED; i++) {
                accountTypeComboBoxes[i].BindingContext = new BindingContext();
                accountTypeComboBoxes[i].DataSource = accountTypes;
                accountCcyComboBoxes[i].BindingContext = new BindingContext();
                accountCcyComboBoxes[i].DataSource = ccyCodes;
            }

            LoadDefaultSettings();
        }

        private void addressInclusionCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (addressInclusionCheckBox.Checked) {
                addressLineLabel1.Enabled = true;
                addressLineTextBox1.Enabled = true;
                addressLineLabel2.Enabled = true;
                addressLineTextBox2.Enabled = true;
                addressLineLabel3.Enabled = true;
                addressLineTextBox3.Enabled = true;
            } else {
                addressLineLabel1.Enabled = false;
                addressLineTextBox1.Enabled = false;
                addressLineLabel2.Enabled = false;
                addressLineTextBox2.Enabled = false;
                addressLineLabel3.Enabled = false;
                addressLineTextBox3.Enabled = false;
            }
        }

        private void accountCheckBox_CheckedChanged(object sender, EventArgs e) {
            int index = Array.IndexOf(accountCheckBoxes, (CheckBox)sender);
            accountTypeComboBoxes[index].Visible = accountCheckBoxes[index].Checked;
            accountCcyComboBoxes[index].Visible = accountCheckBoxes[index].Checked;
            accountLongNumberLabels[index].Visible = accountCheckBoxes[index].Checked;
            accountLongNumberTextBoxes[index].Visible = accountCheckBoxes[index].Checked;
            accountShortNumberLabels[index].Visible = accountCheckBoxes[index].Checked;
            accountShortNumberTextBoxes[index].Visible = accountCheckBoxes[index].Checked;
        }


        private void accountTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            int index = Array.IndexOf(accountTypeComboBoxes, (ComboBox)sender);
            if (accountTypeComboBoxes[index].SelectedIndex == 2 |
                accountTypeComboBoxes[index].SelectedIndex == 3 |
                accountTypeComboBoxes[index].SelectedIndex == 4 ) {
                // Set CCY to AUD for above accounts
                accountCcyComboBoxes[index].SelectedIndex = 1;
            }
            if (accountTypeComboBoxes[index].SelectedIndex == 7) {
                // Set CCY to XXX for above accounts
                accountCcyComboBoxes[index].SelectedIndex = 7;
            }
        }

        private void accountCcyComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            int index = Array.IndexOf(accountCcyComboBoxes, (ComboBox)sender);
            if (accountCcyComboBoxes[index].SelectedIndex <= 1) {
                // Enable short account number field for AUD and blank Ccy
                accountShortNumberLabels[index].Enabled = true;
                accountShortNumberTextBoxes[index].Enabled = true;
            } else {
                // For other selection, no short account number available
                accountShortNumberLabels[index].Enabled = false;
                accountShortNumberTextBoxes[index].Enabled = false;
                accountShortNumberTextBoxes[index].Text = "";
            }
        }

        private void accountLongNumberTextBox_TextChanged(object sender, CancelEventArgs e) {
            if (((System.Windows.Forms.TextBox)sender).Text == "") {
                return; // Do nothing if it is an empty text box
            }
            int index = Array.IndexOf(accountLongNumberTextBoxes, (System.Windows.Forms.TextBox)sender);
            string longNumber = "100001401" + accountLongNumberTextBoxes[index].Text;
            if (!AccountValidater.IntlAcctNumber(longNumber)) {
                // if accountnumber is invalid then popup message box and clear the field
                MessageBox.Show("Invalid International Account Number", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                accountLongNumberTextBoxes[index].Clear();
            }
        }

        private void accountShortNumberTextBox_TextChanged(object sender, CancelEventArgs e) {
            if (((System.Windows.Forms.TextBox)sender).Text == "") {
                return; // Do nothing if it is an empty text box
            }
            int index = Array.IndexOf(accountShortNumberTextBoxes, (System.Windows.Forms.TextBox)sender);
            string shortNumber = "9000" + accountShortNumberTextBoxes[index].Text;
            if (!AccountValidater.DomesticAcctNumber(shortNumber)) {
                // if accountnumber is invalid then popup message box and clear the field
                MessageBox.Show("Invalid Domestic Account Number", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                accountShortNumberTextBoxes[index].Clear();
            }
        }
        
        private void button1_Click(object sender, EventArgs e) {
            List<string> warningMessages = throughOutCheck();
            if (warningMessages.Count > 0) {
                if (!doesUserWantToProceed(warningMessages)) {
                    return; // if user decide to take another look, then return
                }
            }

            List<Account> accountList = new List<Account>();
            for (int i = 0; i < MAX_ACCOUNTS_SUPPORTED; i++) {
                if (accountCheckBoxes[i].Checked) {
                    string accountType = accountTypeComboBoxes[i].Text;
                    string ccyCode = accountCcyComboBoxes[i].Text;
                    string longNumber = "100001401" + accountLongNumberTextBoxes[i].Text;
                    string shortNumber = "";
                    if (accountShortNumberTextBoxes[i].Text != "") {
                        shortNumber = "9000" + accountShortNumberTextBoxes[i].Text;
                    }
                    accountList.Add(new Account(accountType, ccyCode, longNumber, shortNumber));
                }
            }

            CustomerInformation customer;
            if (addressInclusionCheckBox.Checked) {
                customer = new CustomerInformation(customerNameTextBox.Text,
                    addressLineTextBox1.Text, addressLineTextBox2.Text, addressLineTextBox3.Text);
            } else {
                customer = new CustomerInformation(customerNameTextBox.Text);
            }

            BranchInfo branch = new BranchInfo((Branch)(Properties.Settings.Default.DefaultBranchIndex));

            bool inclEmail = emailInclusionCheckBox.Checked;

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + customer.GetName() + ".pdf";
            Pdf_Generation_Class.GenerateWelcomeLetterSimple(fileName, customer, accountList, branch, inclEmail);

            if (openFileCheckBox.Checked) {
                System.Diagnostics.Process.Start(fileName);
            }
        }

        private List<string> throughOutCheck() {
            List<string> warningMessages = new List<string>();

            // Name Check
            const int MIN_NAME_LENGTH = 6;
            if (customerNameTextBox.Text.Length < MIN_NAME_LENGTH) {
                warningMessages.Add("Customer Name is abnormally short (<" + MIN_NAME_LENGTH + ")");
            }

            // Address Check
            const int MIN_ADDRESS_LENGTH = 20;
            if (addressInclusionCheckBox.Checked) {
                int addressLength = addressLineTextBox1.Text.Length +
                    addressLineTextBox2.Text.Length + addressLineTextBox3.Text.Length;
                if (addressLength < MIN_ADDRESS_LENGTH) {
                    warningMessages.Add("Address is abnormally short (<" + MIN_ADDRESS_LENGTH + ")");
                }
            }

            // Account Check
            for (int i = 0; i < MAX_ACCOUNTS_SUPPORTED; i++) {
                if (accountCheckBoxes[i].Checked) {
                    if (accountTypeComboBoxes[i].SelectedIndex == 0) {
                        warningMessages.Add("Account " + i + " has no Type selected");
                    }
                    if (accountCcyComboBoxes[i].SelectedIndex == 0) {
                        warningMessages.Add("Account " + i + " has no Currency Code selected");
                    }
                    if (accountLongNumberTextBoxes[i].Text == "") {
                        warningMessages.Add("Account " + i + " has no Intl Account Number");
                    }
                    if (accountShortNumberTextBoxes[i].Enabled && accountShortNumberTextBoxes[i].Text == "") {
                        warningMessages.Add("Account " + i + " has no Domestic Account Number");
                    }
                }
            }

            return warningMessages;
        }


        private bool doesUserWantToProceed(List<string> warnningMessages) {
            string messageBoxText;

            messageBoxText = "A list of " + warnningMessages.Count() + " warnning messages were found," + Environment.NewLine +
                "Please check carefully before you decide to proceed: " + Environment.NewLine + Environment.NewLine;

            int numOfMsg = 1;
            foreach (string warnningMessage in warnningMessages) {
                messageBoxText += numOfMsg + ". " + warnningMessage + Environment.NewLine;
                numOfMsg++;
            }

            messageBoxText += Environment.NewLine + "Do you still want to proceed with these warnnings?" + Environment.NewLine;
            messageBoxText += "Click \"Yes\" to ignore the Error and proceed with your ego" + Environment.NewLine;
            messageBoxText += "Alternatively click \"No\" to take another gentle and humble look";

            DialogResult usersDecision = MessageBox.Show(messageBoxText, "Attention!!! Warnning Message Found!!!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (usersDecision == DialogResult.Yes) {
                usersDecision = MessageBox.Show("Do you really really want to ignore the Error Messages?", "Attention!!! There is no step back!!!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (usersDecision == DialogResult.Yes) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }

        }

        private void resetButton_Click(object sender, EventArgs e) {
            // clearname
            customerNameTextBox.Clear();

            // clear address
            addressInclusionCheckBox.Checked = false;
            addressLineTextBox1.Clear();
            addressLineTextBox2.Clear();
            addressLineTextBox3.Clear();

            // clear account info
            for (int i = 0; i < MAX_ACCOUNTS_SUPPORTED; i++) {
                accountCheckBoxes[i].Checked = false;
                accountTypeComboBoxes[i].SelectedIndex = 0;
                accountCcyComboBoxes[i].SelectedIndex = 0;
                accountLongNumberTextBoxes[i].Clear();
                accountShortNumberTextBoxes[i].Clear();
            }

            // check first account box
            accountCheckBox1.Checked = true;
        }

        private void LoadDefaultSettings() {
            emailInclusionCheckBox.Checked = Properties.Settings.Default.IncludeEmail;
            openFileCheckBox.Checked = Properties.Settings.Default.OpenFileAfterGeneration;
        }

        private void button1_Click_1(object sender, EventArgs e) {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void button3_Click(object sender, EventArgs e) {
            MessageBox.Show("By using PdfFileWriter as part of this application, this application is under The Code Project Open License (CPOL) 1.02");
        }
    }
}

