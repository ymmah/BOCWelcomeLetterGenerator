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
                accountTypeComboBox1, accountTypeComboBox2, accountCcyComboBox3, accountTypeComboBox4
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
            if (accountTypeComboBoxes[index].SelectedText == "Overseas Student Account" |
                accountTypeComboBoxes[index].SelectedText == "Online Saver Account" |
                accountTypeComboBoxes[index].SelectedText == "Cheque Account" ) {
                // Set CCY to AUD for above accounts
                accountCcyComboBoxes[index].SelectedIndex = 1;
            }
            if (accountTypeComboBoxes[index].SelectedText == "Term Deposit Account") {
                // Set CCY to XXX for above accounts
                accountCcyComboBoxes[index].SelectedIndex = 7;
            }
        }
        
        private void button1_Click(object sender, EventArgs e) {
            Pdf_Generation_Class.GenerateWelcomeLetter(false, "test.pdf");
        }
    }
}
