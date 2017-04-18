using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace SQLQueryGenerator
{
    public partial class SQLScriptGenerator : Form
    {
        #region Constants

        public DataTable shownData;
        public static string INSERT_QUERY = "Insert into TABLE_NAME (COLUMN_NAMES) values (VALUES)";
        public static List<String> numericDataTypes = new List<string>() { "SYSTEM.INT16", "SYSTEM.INT32", "SYSTEM.INT64", "SYSTEM.BOOLEAN" };
        public string tableName = string.Empty;
        public string INSERT_QUERY_BAKED = string.Empty;
        public string selectedPath = string.Empty;
        public bool defaultConnection;
        public string defaultConnectionString = string.Empty;
        #endregion

        #region Public Methods

        public SQLScriptGenerator()
        {
            InitializeComponent();
            ConnectionStatus_toolStripStatusLabel1.Text = "Connection Status : Disconnected";
            FileStatus_toolStripStatusLabel2.Text = "|  File Status : Pending .....!";
            DisableControls();
            LoadItemsForConnectionType();
        }

        #endregion

        #region Helper Methods

        private void button1_Click(object sender, EventArgs e)
        {
            var connectionString = string.Empty;
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select connection type.", "Alert", MessageBoxButtons.OK);
                return;
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                Form defaultConnection = new DefaultConnectionString();
                defaultConnection.ShowDialog();
                

                connectionString = BuildConnectionString(true);
            }
            else
                connectionString = BuildConnectionString(false);

            SqlConnection connection;
            connection = new SqlConnection(connectionString);
            connection.Open();
            ConnectionStatus_toolStripStatusLabel1.Text = "Connection Status : Connected." + textBox_Server.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            INSERT_QUERY_BAKED = string.Empty;
            if (string.IsNullOrEmpty(textBox_SQLQueryToExecute.Text))
                throw new Exception("Please insert the query to execute.");
            //
            var connectionString = BuildConnectionString(defaultConnection);
            //
            var sqlQueryToExecute = textBox_SQLQueryToExecute.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQueryToExecute, connection))
                {
                    SetTableName(sqlQueryToExecute);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        List<string> columnsList = new List<string>();
                        //
                        var dt = dataReader.GetSchemaTable();

                        foreach (DataRow column in dt.Rows)
                            columnsList.Add(column[0].ToString());
                        //
                        if (columnsList.Contains("VersionNo"))
                            columnsList.Remove("VersionNo");
                        //
                        if(!IncludeID_CheckBox.Checked && columnsList.Contains("Id"))
                            columnsList.Remove("Id");

                        //
                        string query = string.Empty;
                        //
                        if (sqlQueryToExecute.ToUpper().Contains("SELECT * FROM"))
                            foreach (var item in columnsList)
                                    if (string.IsNullOrEmpty(query))
                                        query = item;
                                    else
                                        query = query + ", " + item;

                        sqlQueryToExecute = sqlQueryToExecute.Replace("*", query);

                        dataReader.Close();

                        using (SqlCommand commandFinal = new SqlCommand(sqlQueryToExecute, connection))
                        {
                            commandFinal.CommandType = CommandType.Text;
                            using (SqlDataReader dataReaderFinal = commandFinal.ExecuteReader())
                            {
                                try
                                {
                                    var dt2 = new DataTable();
                                    dt2.Load(dataReaderFinal);
                                    shownData = dt2;
                                    dataGridView1.DataSource = dt2;
                                }
                                catch (Exception ex)
                                {
                                    throw new DataException("Check this Error : ", ex);
                                }

                            }
                        }
                    }
                }
            }
            //
        }

        private void button_DisconnectServer_Click(object sender, EventArgs e)
        {
            //connection.Close();
            textBox_SQLQueryToExecute.Text = string.Empty;
            dataGridView1.DataSource = new DataTable();

            ConnectionStatus_toolStripStatusLabel1.Text = "Connection Status : Disconnected.";
FileStatus_toolStripStatusLabel2.Text = "|  File Status : Pending .....!";
        }

        private string BuildConnectionString(bool IsDefault)
        {
            defaultConnection = IsDefault;
            try
            {
                if (defaultConnection)
                    return string.Format("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrialDB-3;Integrated Security=True");
                else
                {
                    if (string.IsNullOrEmpty(textBox_Server.Text) ||
                    string.IsNullOrEmpty(textBox_Username.Text) ||
                    string.IsNullOrEmpty(textBox_Password.Text) ||
                    string.IsNullOrEmpty(textBox_DataBaseName.Text))
                        throw new Exception("Please insert the Server / UserName & Password to connect.");

                    var ServerName = textBox_Server.Text;
                    var DatabaseName = textBox_DataBaseName.Text;
                    var UserName = textBox_Username.Text;
                    var Password = textBox_Password.Text;
                        //

                    return "Data Source=" + ServerName +
                           ";Initial Catalog=" + DatabaseName +
                           ";Integrated Security=False;User ID=" + UserName +
                           ";Password=" + Password +
                           ";Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("Connection Was not Established", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (shownData.Rows.Count == 0)
                throw new Exception("No data present in grid to generate scripts");

            if (string.IsNullOrEmpty(selectedPath))
            {
                MessageBox.Show("Please select a path to save file.", "Alert", MessageBoxButtons.OK);
                return;
            }

            //
            var INSERT_QUERY_RAW = INSERT_QUERY;
            List<string> columns = new List<string>();
            Dictionary<string, string> columnWithDataTypes = new Dictionary<string, string>();
            //
            foreach (DataColumn column in shownData.Columns)
            {
                columns.Add(column.ColumnName);
                columnWithDataTypes.Add(column.ColumnName, column.DataType.ToString());
            }
            //
            string columnNames = string.Empty;
            string columnValues = string.Empty;
            //
            foreach (DataColumn column in shownData.Columns)
                if (string.IsNullOrEmpty(columnNames))
                    columnNames = column.ColumnName;
                else
                    columnNames = columnNames + ", " + column.ColumnName;

            INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("TABLE_NAME", tableName);
            INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("COLUMN_NAMES", columnNames);

            if (string.IsNullOrEmpty(INSERT_QUERY_BAKED))
                INSERT_QUERY_BAKED = INSERT_QUERY_RAW;

            foreach (DataRow dr in shownData.Rows)
            {
                columnValues = string.Empty;
                foreach (var item in columns)
                {
                    if (string.IsNullOrEmpty(columnValues))
                    {
                        if (numericDataTypes.Contains(columnWithDataTypes[item.ToString()].ToUpper()))
                            columnValues = dr[item.ToString()].ToString();
                        else
                            columnValues = string.Concat("'", dr[item.ToString()].ToString(), "'");
                    }
                    else
                    {
                        if (numericDataTypes.Contains(columnWithDataTypes[item.ToString()].ToUpper()))
                            columnValues = columnValues + ", " + dr[item.ToString()].ToString();
                        else
                            columnValues = columnValues + ", " + string.Concat("'", dr[item.ToString()].ToString(), "'");
                    }
                }

                if (columnValues.Contains("True,"))
                    columnValues = columnValues.Replace("True,", "1,");

                if (columnValues.ToUpper().Contains("False,"))
                    columnValues = columnValues.Replace("False,", "0,");

                if (columnValues.ToUpper().Contains("''"))
                    columnValues = columnValues.Replace("''", "NULL");

                if (INSERT_QUERY_RAW.Contains("VALUES"))
                    INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("VALUES", columnValues);
                else
                    INSERT_QUERY_RAW = INSERT_QUERY_RAW + "\n"+"\n" + INSERT_QUERY_BAKED.Replace("VALUES", columnValues);
            }

            using (FileStream fs = File.Create(string.Concat(selectedPath,"\\",tableName,"-",DateTime.Now.ToString("dd-MM-yyyy"),".txt")))
            {
                byte[] text = new UTF8Encoding(true).GetBytes(INSERT_QUERY_RAW);
                fs.Write(text, 0, text.Length);
            }
            //MessageBox.Show(INSERT_QUERY_RAW.ToString());
            MessageBox.Show("Your file has been saved at : " + selectedPath + "\n\n File Name : " + string.Concat(tableName, "-", DateTime.Now.ToString("dd-MM-yyyy"), ".txt"), "File Saved.", MessageBoxButtons.OK);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                DisableControls();
            else
                EnableControls();
        }

        private void LoadItemsForConnectionType()
        {
            comboBox1.Items.Add(new Dictionary<Int32, string>() { {1,"Windows Authentication" }, { 2, "SQL Connection"} });
        }

        private void DisableControls()
        {
            textBox_Server.Enabled = false;
            textBox_Username.Enabled = false;
            textBox_Password.Enabled = false;
            textBox_DataBaseName.Enabled = false;
        }

        private void EnableControls()
        {
            textBox_Server.Enabled = true;
            textBox_Username.Enabled = true;
            textBox_Password.Enabled = true;
            textBox_DataBaseName.Enabled = true;
        }

        private string GetSQLQueryToExecute(SqlDataReader dataReader, string sqlQuery)
        {
            return sqlQuery;
        }

        private void SetTableName(string sqlQuery)
        {
            sqlQuery = sqlQuery.Remove(0, sqlQuery.ToUpper().IndexOf("FROM") - 1);
            sqlQuery = sqlQuery.ToUpper().Replace("FROM", "");
            sqlQuery = sqlQuery.TrimStart();
            if(sqlQuery.Contains(" "))
                sqlQuery = sqlQuery.Remove(sqlQuery.IndexOf(" "));
            var space = " ";
            foreach (char c in sqlQuery)
                sqlQuery = sqlQuery.Replace(space, "");

            tableName = sqlQuery;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            FileStatus_toolStripStatusLabel2.Text = "|  File will be saved at : "+ fbd.SelectedPath;
            selectedPath = fbd.SelectedPath;
        }
        #endregion
    }
}
