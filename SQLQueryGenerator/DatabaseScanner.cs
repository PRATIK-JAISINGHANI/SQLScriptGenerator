using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLQueryGenerator
{
    public partial class DatabaseScanner : Form
    {
        public DatabaseScanner()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void CompareTable_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CompareTable_RadioButton.Checked == true)
                tablename_textbox.Enabled = true;
            else
                tablename_textbox.Enabled = false;
        }
        
        #region Helper Methods

        private void SetDefaults()
        {
            tablename_textbox.Enabled = false;
        }

        private void DisableAllControls()
        {
            ConnectionType_ComboBox1.ResetText();
            ConnectionType_ComboBox2.ResetText();
            DefaultConnectionString_textBox1.Text = string.Empty;
            DeafultConnectionString_textBox2.Text = string.Empty;
            DefaultConnectionString_textBox1.Enabled = false;
            DeafultConnectionString_textBox2.Enabled = false;
            Server_textBox1.Text = string.Empty;
            Server_textBox2.Text = string.Empty;
            Server_textBox1.Enabled = false;
            Server_textBox2.Enabled = false;
            Username_textBox1.Text = string.Empty;
            Username_textBox2.Text = string.Empty;
            Username_textBox1.Enabled = false;
            Username_textBox2.Enabled = false;
            Password_textBox1.Text = string.Empty;
            Password_textBox2.Text = string.Empty;
            Password_textBox1.Enabled = false;
            Password_textBox2.Enabled = false;
            DatabaseName_textBox1.Text = string.Empty;
            DatabaseName_textBox2.Text = string.Empty;
            DatabaseName_textBox1.Enabled = false;
            DatabaseName_textBox2.Enabled = false;
        }



        private string GetConnectionString(Connection connectionNo)
        {
            var result = string.Empty;
            switch (connectionNo)
            {
                case Connection.FirstDB:
                    {
                        if (ConnectionType_ComboBox1.SelectedIndex == 0)
                            result = BuildFirstConnectionString(true);
                        //
                        if (ConnectionType_ComboBox1.SelectedIndex == 1)
                            result = BuildFirstConnectionString(false);
                    }
                    break;
                case Connection.SecondDB:
                    {
                        if (ConnectionType_ComboBox2.SelectedIndex == 0)
                            result = BuildSecondConnectionString(true);
                        if (ConnectionType_ComboBox2.SelectedIndex == 1)
                            result = BuildSecondConnectionString(false);
                    }
                    break;
            }
            return result;
        }

        private string BuildFirstConnectionString(bool IsDefault)
        {
            //defaultConnection = IsDefault;
            if (IsDefault)
                return DefaultConnectionString_textBox1.Text;//string.Format("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrialDB-3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            else
            {
                if (string.IsNullOrEmpty(Server_textBox1.Text) ||
                string.IsNullOrEmpty(Username_textBox1.Text) ||
                string.IsNullOrEmpty(Password_textBox1.Text) ||
                string.IsNullOrEmpty(DatabaseName_textBox1.Text))
                {
                    //throw new Exception("Please insert the Server / UserName & Password to connect.");
                    MessageBox.Show("Please insert the Server / UserName & Password for first database (L.H.S) to connect.");
                    return string.Empty;
                }

                var ServerName = Server_textBox1.Text;
                var DatabaseName = DatabaseName_textBox1.Text;
                var UserName = Username_textBox1.Text;
                var Password = Password_textBox1.Text;
                //

                return "Data Source=" + ServerName +
                       ";Initial Catalog=" + DatabaseName +
                       ";Integrated Security=False;User ID=" + UserName +
                       ";Password=" + Password +
                       ";Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }

        private string BuildSecondConnectionString(bool IsDefault)
        {
            //defaultConnection = IsDefault;
            if (IsDefault)
                return DeafultConnectionString_textBox2.Text;
            else
            {
                if (string.IsNullOrEmpty(Server_textBox2.Text) ||
                string.IsNullOrEmpty(Server_textBox2.Text) ||
                string.IsNullOrEmpty(Password_textBox2.Text) ||
                string.IsNullOrEmpty(DatabaseName_textBox2.Text))
                {
                    //throw new Exception("Please insert the Server / UserName & Password to connect.");
                    MessageBox.Show("Please insert the Server / UserName & Password for second database (R.H.S) to connect.");
                    return string.Empty;
                }

                var ServerName = Server_textBox2.Text;
                var DatabaseName = DatabaseName_textBox2.Text;
                var UserName = Username_textBox2.Text;
                var Password = Password_textBox2.Text;
                //

                return "Data Source=" + ServerName +
                       ";Initial Catalog=" + DatabaseName +
                       ";Integrated Security=False;User ID=" + UserName +
                       ";Password=" + Password +
                       ";Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }

        #endregion
    }
}

public enum Connection : int
{
    FirstDB = 1,
    SecondDB = 2
}
