using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOCWelcomeLetterGenerator {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
            BranchInfo dummybranch = new BranchInfo(Branch.Brisbane);
            branchSelectionComboBox.DataSource = dummybranch.GetBranchNameList();
            LoadDefaultSettings();
        }

        private void Settings_Load(object sender, EventArgs e) {

        }

        private void LoadDefaultSettings() {
            emailInclusionCheckBox.Checked = Properties.Settings.Default.IncludeEmail;
            openFileCheckBox.Checked = Properties.Settings.Default.OpenFileAfterGeneration;
            branchSelectionComboBox.SelectedIndex = Properties.Settings.Default.DefaultBranchIndex;
        }

        private void UpdateDefaultSettings() {
            Properties.Settings.Default.IncludeEmail = emailInclusionCheckBox.Checked;
            Properties.Settings.Default.OpenFileAfterGeneration = openFileCheckBox.Checked;
            Properties.Settings.Default.DefaultBranchIndex = branchSelectionComboBox.SelectedIndex;
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e) {
            UpdateDefaultSettings();
            MessageBox.Show("Changes updated successfully! New settings will take effect next time!");
            this.Close();
        }
    }
}
