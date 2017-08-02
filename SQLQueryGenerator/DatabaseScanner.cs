using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLQueryGenerator
{
    public partial class DatabaseScanner : Form
    {

        #region Declaration 

        public string FirstConnectionString { get; set; }
        public string SecondConnectionString { get; set; }

        #endregion

        #region Constructor 

        public DatabaseScanner()
        {
            InitializeComponent();
            SetDefaults();
        }

        #endregion
        
        #region Helper Methods

        private void SetDefaults()
        {
            FirstDBConnectionStatus.Text = "Disconnected";
            SecondDBConnectionStatus.Text = "Disconnected";
            tablename_textbox.Enabled = false;

            DefaultConnectionString_textBox1.Enabled = false;
            Server_textBox1.Enabled = false;
            Username_textBox1.Enabled = false;
            Password_textBox1.Enabled = false;
            DatabaseName_textBox1.Enabled = false;
            //
            DeafultConnectionString_textBox2.Enabled = false;
            Server_textBox2.Enabled = false;
            Username_textBox2.Enabled = false;
            Password_textBox2.Enabled = false;
            DatabaseName_textBox2.Enabled = false;

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
                        else if (ConnectionType_ComboBox1.SelectedIndex == 1)
                            result = BuildFirstConnectionString(false);
                        else
                        {
                            MessageBox.Show("Please select proper connection type for First DB", "ALERT", MessageBoxButtons.OK);
                            return result;
                        }
                    }
                    break;
                case Connection.SecondDB:
                    {
                        if (ConnectionType_ComboBox2.SelectedIndex == 0)
                            result = BuildSecondConnectionString(true);
                        else if (ConnectionType_ComboBox2.SelectedIndex == 1)
                            result = BuildSecondConnectionString(false);
                        else
                        {
                            MessageBox.Show("Please select proper connection type for Second DB", "ALERT", MessageBoxButtons.OK);
                            return result;
                        }
                    }
                    break;
            }
            return result;
        }

        private string BuildFirstConnectionString(bool IsDefault)
        {
            //defaultConnection = IsDefault;
            if (IsDefault)
                return DefaultConnectionString_textBox1.Text;
            else
            {
                if (string.IsNullOrEmpty(Server_textBox1.Text) ||
                string.IsNullOrEmpty(Username_textBox1.Text) ||
                string.IsNullOrEmpty(Password_textBox1.Text) ||
                string.IsNullOrEmpty(DatabaseName_textBox1.Text))
                {
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
            if (IsDefault)
                return DeafultConnectionString_textBox2.Text;
            else
            {
                if (string.IsNullOrEmpty(Server_textBox2.Text) ||
                string.IsNullOrEmpty(Server_textBox2.Text) ||
                string.IsNullOrEmpty(Password_textBox2.Text) ||
                string.IsNullOrEmpty(DatabaseName_textBox2.Text))
                {
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

        private void CompareDatabases()
        {
            if (string.IsNullOrEmpty(FirstConnectionString))
            {
                MessageBox.Show("Connection String for first DB was not found, Please connect fist DB then try again.", "ALERT", MessageBoxButtons.OK);
                return;
            }
            //
            if (string.IsNullOrEmpty(SecondConnectionString))
            {
                MessageBox.Show("Connection String for Second DB was not found, Please connect second DB then try again.", "ALERT", MessageBoxButtons.OK);
                return;
            }
            //
            CompareDatabaseInternal();
        }

        private void CompareDatabaseInternal()
        {
            var finalLog = string.Empty;
            var _informationSchemaQuery = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
            //
            DataTable firstDBTable = new DataTable("firstDBTable");
            firstDBTable = GetData(FirstConnectionString, _informationSchemaQuery);
            //
            DataTable secondDBTable = new DataTable("secondDBTable");
            secondDBTable = GetData(SecondConnectionString, _informationSchemaQuery);
            //
            finalLog = CheckTables(firstDBTable, secondDBTable);

            finalLog = finalLog + CheckColumns(firstDBTable, secondDBTable);

            if(!string.IsNullOrEmpty(finalLog))
            {
                using (FileStream fs = File.Create(string.Concat(filePathLabel.Text, "\\", "DB Comparison Log File -", DateTime.Now.ToString("dd-MM-yyyy"), ".txt")))
                {
                    byte[] text = new UTF8Encoding(true).GetBytes(finalLog);
                    fs.Write(text, 0, text.Length);
                }
                MessageBox.Show("Your DB Comparison Log File has been saved on path : "+filePathLabel.Text, "File Saved", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Both the DBs Match Properly", "COMPARISON RESULT", MessageBoxButtons.OK);
            }
        }

        private void EnableFirstDBConnectionControls(bool IsDefaultConnection)
        {
            if (IsDefaultConnection)
            {
                DefaultConnectionString_textBox1.Enabled = true;
                Server_textBox1.Enabled = false;
                Username_textBox1.Enabled = false;
                Password_textBox1.Enabled = false;
                DatabaseName_textBox1.Enabled = false;
            }
            else
            {
                DefaultConnectionString_textBox1.Enabled = false;
                Server_textBox1.Enabled = true;
                Username_textBox1.Enabled = true;
                Password_textBox1.Enabled = true;
                DatabaseName_textBox1.Enabled = true;

            }
        }

        private void EnableSecondDBConnectionControls(bool IsDefaultConnection)
        {
            if (IsDefaultConnection)
            {
                DeafultConnectionString_textBox2.Enabled = true;
                Server_textBox2.Enabled = false;
                Username_textBox2.Enabled = false;
                Password_textBox2.Enabled = false;
                DatabaseName_textBox2.Enabled = false;
            }
            else
            {
                DeafultConnectionString_textBox2.Enabled = false;
                Server_textBox2.Enabled = true;
                Username_textBox2.Enabled = true;
                Password_textBox2.Enabled = true;
                DatabaseName_textBox2.Enabled = true;
            }
        }

        private string CheckTables(DataTable firstDBTable, DataTable secondDBTable)
        {
            var remainingTables = new List<string>();
            var messageToShow = string.Empty;
            //
            remainingTables = firstDBTable.DefaultView.ToTable(true, "TABLE_NAME").AsEnumerable().Select(p => p.Field<string>("TABLE_NAME")).Except(secondDBTable.DefaultView.ToTable(true, "TABLE_NAME").AsEnumerable().Select(p => p.Field<string>("TABLE_NAME"))).ToList<String>();
            if (remainingTables.Count > 0)
                messageToShow = string.Format("Tables Not Found in Second DB : {0}", string.Join(", \n", remainingTables));
            //
            remainingTables = secondDBTable.DefaultView.ToTable(true, "TABLE_NAME").AsEnumerable().Select(p => p.Field<string>("TABLE_NAME")).Except(firstDBTable.DefaultView.ToTable(true, "TABLE_NAME").AsEnumerable().Select(p => p.Field<string>("TABLE_NAME"))).ToList<string>();
            if (remainingTables.Count > 0 && !string.IsNullOrEmpty(messageToShow))
                messageToShow = string.Format("{0} \n \n Tables Not Found in First DB : {1}", messageToShow, string.Join(", \n", remainingTables));

            return messageToShow;
            //if (!string.IsNullOrEmpty(messageToShow))
            //{
            //    MessageBox.Show(messageToShow, "ALERT", MessageBoxButtons.OK);
            //    return;
            //}
        }

        private string CheckColumns(DataTable firstDBTable, DataTable secondDBTable)
        {
            var resultMessage = string.Empty;
            var commonTables = firstDBTable.DefaultView.ToTable(true, "TABLE_NAME").AsEnumerable().Select(p => p.Field<string>("TABLE_NAME")).Intersect(secondDBTable.DefaultView.ToTable(true, "TABLE_NAME").AsEnumerable().Select(p => p.Field<string>("TABLE_NAME"))).ToList<string>();
            if (commonTables.Count == 0)
            {
                resultMessage = "Not a single table match was found, Perhaps similar DBs are not being compared. Please select similar DBs and try it again.";
                //MessageBox.Show("Not a single table match was found, Perhaps similar DBs are not being compared. Please select similar DBs and try it again.", "CAUTION", MessageBoxButtons.OK);
                //return;
            }
            //
            foreach (var item in commonTables)
            {
                var _queryToGetColumns = string.Format("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'", item);
                string[] columnList = { "TABLE_CATALOG", "TABLE_NAME", "COLUMN_NAME", "COLUMN_DEFAULT", "IS_NULLABLE", "DATA_TYPE", "CHARACTER_MAXIMUM_LENGTH" };
                //
                DataTable firstDBColumns = GetData(FirstConnectionString, _queryToGetColumns);
                firstDBColumns.TableName = item;
                DataTable secondDBColumns = GetData(SecondConnectionString, _queryToGetColumns);
                secondDBColumns.TableName = item;
                //
                resultMessage = resultMessage + "\n \n "+item+" \n \n" + CompareColumns(firstDBColumns.DefaultView.ToTable(true, columnList), secondDBColumns.DefaultView.ToTable(true, columnList));
            }
            //MessageBox.Show(finalLog, "FINAL LOG", MessageBoxButtons.OK);
            return resultMessage;
        }

        private string CompareColumns(DataTable firstTable, DataTable secondTable)
        {
            var finalErrorMessage = string.Empty;

            // Check Columns in Second DB Table From First DB Table
            var columnsNotPresentInSecondDB = firstTable.AsEnumerable().Select(p => p.Field<string>("COLUMN_NAME")).Except(secondTable.AsEnumerable().Select(p => p.Field<string>("COLUMN_NAME"))).ToList<string>();
            if (columnsNotPresentInSecondDB.Count > 0)
                finalErrorMessage = finalErrorMessage + string.Format("\n \n Columns not found in Database : {0} , Table : {1} , Columns : {2}", secondTable.Rows[0]["TABLE_CATALOG"], secondTable.Rows[0]["TABLE_NAME"], string.Join(", \n", columnsNotPresentInSecondDB));

            // Check Columns in First DB Table From Second DB Table
            var columnsNotPresentInFirstDB = secondTable.AsEnumerable().Select(p => p.Field<string>("COLUMN_NAME")).Except(firstTable.AsEnumerable().Select(p => p.Field<string>("COLUMN_NAME"))).ToList<string>();
            if (columnsNotPresentInFirstDB.Count > 0)
                finalErrorMessage = finalErrorMessage + string.Format("\n \n Columns not found in Database : {0} , Table : {1} , Columns : {2}", firstTable.Rows[0]["TABLE_CATALOG"], firstTable.Rows[0]["TABLE_NAME"], string.Join(", \n", columnsNotPresentInFirstDB));

            // Now get common columns from Table
            var commonColumns = firstTable.AsEnumerable().Select(p => p.Field<string>("COLUMN_NAME")).Intersect(secondTable.AsEnumerable().Select(p => p.Field<string>("COLUMN_NAME"))).ToList<string>();

            if (commonColumns.Count == 0)
            {
                finalErrorMessage = finalErrorMessage + string.Format("Table : {0} - Not a single column found matching, Perhaps only Table Name is same. Please try Again.", firstTable.AsEnumerable().Select(p => p.Field<String>("TABLE_NAME").First()));
            }
            //
            foreach(var columnName in commonColumns)
            {
                var dataRow1 = firstTable.AsEnumerable().Where(p => p.Field<string>("COLUMN_NAME") == columnName).First();
                var dataRow2 = secondTable.AsEnumerable().Where(p => p.Field<string>("COLUMN_NAME") == columnName).First();

                foreach (var internalColumn in dataRow1.Table.Columns)
                {
                    if (dataRow1[internalColumn.ToString()] != dataRow2[internalColumn.ToString()])
                        finalErrorMessage = finalErrorMessage + string.Format("\n \n In Table : {0}, {1} Didn't Match For Column : {2} ", firstTable.Rows[0]["TABLE_NAME"], internalColumn, columnName);
                }
            }
            return finalErrorMessage;
        }

        private DataTable GetData(string connectionString, string queryToExecute)
        {
            DataTable result = new DataTable();
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(queryToExecute))
            {
                MessageBox.Show("Please Check Connection String OR Query To Execute, Then Try Again.", "ERROR", MessageBoxButtons.OK);
                return null;
            }
            //
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryToExecute, connection))
                {
                    command.CommandType = CommandType.Text;
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK);
                        return null;
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }
            return result;
        }

        private bool ValidateBeforeCompare()
        {
            if (string.IsNullOrEmpty(FirstConnectionString))
            {
                MessageBox.Show("Please Check Connection For First DB", "ERROR", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrEmpty(SecondConnectionString))
            {
                MessageBox.Show("Please Check Connection For First DB", "ERROR", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrEmpty(filePathLabel.Text))
            {
                MessageBox.Show("Please select path to save a log file", "Select Path", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        #endregion

        #region Event Methods

        private void CompareTable_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CompareTable_RadioButton.Checked == true)
                tablename_textbox.Enabled = true;
            else
                tablename_textbox.Enabled = false;
        }

        private void Compare_button_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeCompare())
                return;
            //
            if (CompareDatabase_radiobutton.Checked)
                CompareDatabases();

            if (CompareTable_RadioButton.Checked)
            {
                MessageBox.Show("Compare Table is not implemented yet, please try later. Thank You.", "ALERT", MessageBoxButtons.OK);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FirstConnectionString = GetConnectionString(Connection.FirstDB);
            if (string.IsNullOrEmpty(FirstConnectionString))
                return;
            //
            using (SqlConnection connection = new SqlConnection(FirstConnectionString))
            {
                try
                {
                    connection.Open();
                    FirstDBConnectionStatus.Text = "Connected";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SecondConnectionString = GetConnectionString(Connection.SecondDB);
            using (SqlConnection connection = new SqlConnection(SecondConnectionString))
            {
                connection.Open();
                SecondDBConnectionStatus.Text = "Connected";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FirstConnectionString = string.Empty;
            FirstDBConnectionStatus.Text = "Disconnected";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SecondConnectionString = string.Empty;
            SecondDBConnectionStatus.Text = "Disconnected";
        }

        private void ConnectionType_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConnectionType_ComboBox1.SelectedIndex == 0)
                EnableFirstDBConnectionControls(true);

            if (ConnectionType_ComboBox1.SelectedIndex == 1)
                EnableFirstDBConnectionControls(false);
        }

        private void ConnectionType_ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConnectionType_ComboBox2.SelectedIndex == 0)
                EnableSecondDBConnectionControls(true);

            if (ConnectionType_ComboBox2.SelectedIndex == 1)
                EnableSecondDBConnectionControls(false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            filePathLabel.Text =  fbd.SelectedPath;
        }

        #endregion
      
    }
}

public enum Connection : int
{
    FirstDB = 1,
    SecondDB = 2
}
