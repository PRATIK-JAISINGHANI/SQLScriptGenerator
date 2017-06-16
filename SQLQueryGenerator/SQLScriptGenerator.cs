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
        string filesGenerated = string.Empty;
        public DataSet shownTables = new DataSet();
        public static string INSERT_QUERY = "If Not Exists (Select 1 From TABLE_NAME Where WHERE_CLAUSE) \nBEGIN \nInsert into TABLE_NAME (COLUMN_NAMES) \nvalues (VALUES) \nEND";
        public static List<String> numericDataTypes = new List<string>() { "SYSTEM.INT16", "SYSTEM.INT32", "SYSTEM.INT64" };
        public static List<String> dateTimeDataTypes = new List<string>() { "System.DateTime" };
        public string tableName = string.Empty;
        public string INSERT_QUERY_BAKED = string.Empty;
        public string selectedPath = string.Empty;
        public bool defaultConnection;
        public string defaultConnectionString = string.Empty;

        public bool IsConnectionEstablished { get; set; }
        #endregion

        #region Constructor

        public SQLScriptGenerator()
        {
            SetDefaults();
        }

        #endregion

        #region Helper Methods

        private void SetDefaults()
        {
            InitializeComponent();
            ConnectionStatus_toolStripStatusLabel1.Text = "Connection Status : Disconnected";
            FileStatus_toolStripStatusLabel2.Text = "|  File Status : Pending .....!";
            DisableControls();
            //LoadItemsForConnectionType();
        }

        private void button_ConnectServer_Click(object sender, EventArgs e)
        {
            var connectionString = string.Empty;
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select connection type.", "Alert", MessageBoxButtons.OK);
                return;
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(defaultConnection_textBox.Text))
                {
                    MessageBox.Show("Please provide default connection string to establish a connection.", "Alert", MessageBoxButtons.OK);
                    return;
                }
                connectionString = BuildConnectionString(true);
            }
            else
                connectionString = BuildConnectionString(false);

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    SqlConnection connection;
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    ConnectionStatus_toolStripStatusLabel1.Text = "Connection Status : Connected." + textBox_Server.Text;
                    IsConnectionEstablished = true;
                }
                catch (Exception ex)
                {
                    IsConnectionEstablished = false;
                    MessageBox.Show("Connection was not established.", "Alert", MessageBoxButtons.OK);
                }
            }
        }

        private void ExecuteQuery_button_Click(object sender, EventArgs e)
        {
            if (!IsConnectionEstablished)
            {
                MessageBox.Show("Please check your connection before executing a query.", "Alert!", MessageBoxButtons.OK);
                return;
            }
            //
            ClearDataSet();
            INSERT_QUERY_BAKED = string.Empty;
            if (string.IsNullOrEmpty(textBox_SQLQueryToExecute.Text))
            {
                //throw new Exception("Please insert the query to execute.");
                MessageBox.Show("Please insert the query to execute.", "Error Found", MessageBoxButtons.OK);
                return;
            }
            //
            var connectionString = BuildConnectionString(defaultConnection);
            //
            var sqlQueryToExecute = textBox_SQLQueryToExecute.Text.Split('|').ToList<string>();

            foreach (var sqlItem in sqlQueryToExecute)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlItem, connection))
                    {
                        SetTableName(sqlItem);
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            List<string> columnsList = new List<string>();
                            //
                            var dt = dataReader.GetSchemaTable();

                            foreach (DataRow column in dt.Rows)
                                if(column["DataType"].ToString() != "System.Byte[]")
                                    columnsList.Add(column[0].ToString());
                            //
                            //if (columnsList.Contains("VersionNo"))
                            //    columnsList.Remove("VersionNo");
                            //
                            if (!IncludeID_CheckBox.Checked && columnsList.Contains("Id"))
                                columnsList.Remove("Id");
                            //
                            if (!IncludeID_CheckBox.Checked && columnsList.Contains("ID"))
                                columnsList.Remove("ID");

                            //
                            string query = string.Empty;
                            string innerQuery = sqlItem;
                            //
                            if (innerQuery.ToUpper().Contains("SELECT * FROM"))
                            {
                                foreach (var item in columnsList)
                                    if (string.IsNullOrEmpty(query))
                                        query = item;
                                    else
                                        query = query + "," + item;

                                innerQuery = innerQuery.Replace("*", query);
                            }

                            dataReader.Close();
                            //
                            using (SqlCommand commandFinal = new SqlCommand(innerQuery, connection))
                            {
                                commandFinal.CommandType = CommandType.Text;
                                using (SqlDataReader dataReaderFinal = commandFinal.ExecuteReader())
                                {
                                    try
                                    {
                                        var dt2 = new DataTable(tableName);
                                        dt2.Load(dataReaderFinal);
                                        shownTables.Tables.Add(dt2);
                                        if (sqlQueryToExecute.Count == 1)
                                            dataGridView1.DataSource = dt2;
                                        else
                                            dataGridView1.DataSource = null;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(Convert.ToString(ex), "Error Found", MessageBoxButtons.OK);
                                        return;
                                    }
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
            DisconnectServer();
        }

        private string BuildConnectionString(bool IsDefault)
        {
            defaultConnection = IsDefault;
            if (defaultConnection)
                return defaultConnection_textBox.Text;//string.Format("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrialDB-3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            else
            {
                if (string.IsNullOrEmpty(textBox_Server.Text) ||
                string.IsNullOrEmpty(textBox_Username.Text) ||
                string.IsNullOrEmpty(textBox_Password.Text) ||
                string.IsNullOrEmpty(textBox_DataBaseName.Text))
                {
                    //throw new Exception("Please insert the Server / UserName & Password to connect.");
                    MessageBox.Show("Please insert the Server / UserName & Password to connect.");
                    return string.Empty;
                }

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

        private void GenerateScript_button_Click(object sender, EventArgs e)
        {
            if (shownTables.Tables.Count == 0)
            {
                MessageBox.Show("No Tables found to generate scripts", "Alert!", MessageBoxButtons.OK);
                return;
            }
            //
            if (string.IsNullOrEmpty(selectedPath))
            {
                MessageBox.Show("Please select a path to save file.", "Alert", MessageBoxButtons.OK);
                return;
            }
            //
            foreach (DataTable table in shownTables.Tables)
            {
                
                var INSERT_QUERY_RAW = INSERT_QUERY;
                var columns = new List<string>();
                var columnWithDataTypes = new Dictionary<string, string>();
                //
                // NOTE : Here All the columns are added in 2 list i.e columns and columnWithDataTypes.
                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    columnWithDataTypes.Add(column.ColumnName, column.DataType.ToString());
                }
                //
                string columnNames = string.Empty;
                string columnValues = string.Empty;
                //
                foreach (DataColumn column in table.Columns)
                    if (string.IsNullOrEmpty(columnNames))
                        columnNames = column.ColumnName;
                    else
                        columnNames = columnNames + "," + column.ColumnName;


                INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("TABLE_NAME", table.TableName);
                INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("COLUMN_NAMES", columnNames);

                if (string.IsNullOrEmpty(INSERT_QUERY_BAKED))
                    INSERT_QUERY_BAKED = INSERT_QUERY_RAW;

                foreach (DataRow dr in table.Rows)
                {
                    columnValues = string.Empty;
                    var columnValuesList = new List<string>();
                    foreach (var item in columns)
                    {
                        if (string.IsNullOrEmpty(columnValues))
                        {
                            if (numericDataTypes.Contains(columnWithDataTypes[item.ToString()].ToUpper()))
                            {
                                columnValues = dr[item.ToString()].ToString();
                                columnValuesList.Add(dr[item.ToString()].ToString());
                            }
                            else
                            {
                                if (dateTimeDataTypes.Contains(columnWithDataTypes[item.ToString()]) && !string.IsNullOrEmpty(dr[item.ToString()].ToString()))
                                {
                                    columnValues = string.Concat("'", Convert.ToDateTime(dr[item.ToString()]).ToString("yyyy-MM-dd HH:mm:ss"), "'");
                                    columnValuesList.Add(string.Concat("'", Convert.ToDateTime(dr[item.ToString()]).ToString("yyyy-MM-dd HH:mm:ss"), "'"));
                                }
                                else
                                {
                                    columnValues = string.Concat("'", dr[item.ToString()].ToString(), "'");
                                    columnValuesList.Add(string.Concat("'", dr[item.ToString()].ToString(), "'"));
                                }
                            }
                        }
                        else
                        {
                            if (numericDataTypes.Contains(columnWithDataTypes[item.ToString()].ToUpper()))
                            {
                                columnValues = columnValues + "," + dr[item.ToString()].ToString();
                                columnValuesList.Add(dr[item.ToString()].ToString());
                            }
                            else
                            {
                                if (dateTimeDataTypes.Contains(columnWithDataTypes[item.ToString()]) && !string.IsNullOrEmpty(dr[item.ToString()].ToString()))
                                {
                                    columnValues = columnValues + "," + string.Concat("'", Convert.ToDateTime(dr[item.ToString()]).ToString("yyyy-MM-dd HH:mm:ss"), "'");
                                    columnValuesList.Add(string.Concat("'", Convert.ToDateTime(dr[item.ToString()]).ToString("yyyy-MM-dd HH:mm:ss"), "'"));
                                }
                                else
                                {
                                    columnValues = columnValues + "," + string.Concat("'", dr[item.ToString()].ToString(), "'");
                                    columnValuesList.Add(string.Concat("'", dr[item.ToString()].ToString(), "'"));
                                }
                            }
                        }
                    }

                    if (columnValues.Contains("'True'"))
                        columnValues = columnValues.Replace("'True'", "1");

                    if (columnValues.Contains("'False'"))
                        columnValues = columnValues.Replace("'False'", "0");

                    var t = new List<int>();
                    var f = new List<int>();

                    foreach (var item in columnValuesList)
                    {
                        if (item.ToString() == "'True'")
                            t.Add(columnValuesList.IndexOf(item.ToString()));
                        if (item.ToString() == "'False'")
                            f.Add(columnValuesList.IndexOf(item.ToString()));
                    }

                    foreach (var item in t)
                        columnValuesList[item] = "1";


                    foreach (var item in f)
                        columnValuesList[item] = "0";

                    if (columnValues.Contains("''"))
                        columnValues = columnValues.Replace("''", "NULL");

                    if (columnValues.ToUpper().Contains(",,"))
                        columnValues = columnValues.Replace(",,", ",NULL,");

                    if (INSERT_QUERY_RAW.Contains("VALUES"))
                        INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("VALUES", columnValues);
                    else
                        INSERT_QUERY_RAW = INSERT_QUERY_RAW + "\n" + "\n" + INSERT_QUERY_BAKED.Replace("VALUES", columnValues);

                    if (INSERT_QUERY_RAW.Contains("WHERE_CLAUSE"))
                        INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("WHERE_CLAUSE", GenerateWhereClause(columnWithDataTypes, columns, columnValuesList));
                }

                using (FileStream fs = File.Create(string.Concat(selectedPath, "\\", table.TableName, "-", DateTime.Now.ToString("dd-MM-yyyy"), ".txt")))
                {
                    byte[] text = new UTF8Encoding(true).GetBytes(INSERT_QUERY_RAW);
                    fs.Write(text, 0, text.Length);
                }

                if (string.IsNullOrEmpty(filesGenerated))
                    filesGenerated = string.Concat(table.TableName, "-", DateTime.Now.ToString("dd-MM-yyyy"), ".txt");
                else
                    filesGenerated = filesGenerated + "\n" + string.Concat(table.TableName, "-", DateTime.Now.ToString("dd-MM-yyyy"), ".txt");
            }
            MessageBox.Show(string.Concat("Your file has been saved at : ", selectedPath, "\n\n File Names : ", filesGenerated, " are File Saved."), "Files Saved.", MessageBoxButtons.OK);
            filesGenerated = string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                DisableControls();
                defaultConnection_textBox.Enabled = true;
            }

            else
            {
                EnableControls();
                defaultConnection_textBox.Enabled = false;
            }
        }

        private void DisableControls()
        {
            textBox_Server.Enabled = false;
            textBox_Username.Enabled = false;
            textBox_Password.Enabled = false;
            textBox_DataBaseName.Enabled = false;
            defaultConnection_textBox.Enabled = false;
            //
            defaultConnection_textBox.Text = string.Empty;
            textBox_Server.Text = string.Empty;
            textBox_Username.Text = string.Empty;
            textBox_Password.Text = string.Empty;
            textBox_DataBaseName.Text = string.Empty;
        }

        private void EnableControls()
        {
            textBox_Server.Enabled = true;
            textBox_Username.Enabled = true;
            textBox_Password.Enabled = true;
            textBox_DataBaseName.Enabled = true;
        }

        private void SetTableName(string sqlQuery)
        {
            sqlQuery = sqlQuery.Remove(0, sqlQuery.ToUpper().IndexOf("FROM") - 1);
            sqlQuery = sqlQuery.ToUpper().Replace("FROM", "");
            sqlQuery = sqlQuery.TrimStart();
            if (sqlQuery.Contains(" "))
                sqlQuery = sqlQuery.Remove(sqlQuery.IndexOf(" "));
            var space = " ";
            foreach (char c in sqlQuery)
                sqlQuery = sqlQuery.Replace(space, "");

            tableName = sqlQuery;
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            FileStatus_toolStripStatusLabel2.Text = "|  File will be saved at : " + fbd.SelectedPath;
            selectedPath = fbd.SelectedPath;
        }

        private string GenerateWhereClause(Dictionary<string, string> columnsWithDataTypes, List<string> columnNamesList, List<string> columnValuesList)
        {
            var whereClause = string.Empty;
            if (columnNamesList.Count == columnValuesList.Count)
            {

                for (int i = 0; i < columnValuesList.Count; i++)
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        if (!dateTimeDataTypes.Contains(columnsWithDataTypes[columnNamesList[i]]))
                        {
                            if (columnValuesList[i] != "NULL")
                                whereClause = columnNamesList[i] + " = " + columnValuesList[i];
                            else
                                whereClause = columnNamesList[i] + " IS " + columnValuesList[i];
                        }
                    }
                    else
                    {
                        if (!dateTimeDataTypes.Contains(columnsWithDataTypes[columnNamesList[i]]))
                        {
                            if (columnValuesList[i] != "NULL")
                                whereClause = whereClause + " And " + columnNamesList[i] + " = " + columnValuesList[i];
                            else
                                whereClause = whereClause + " And " + columnNamesList[i] + " IS " + columnValuesList[i];
                        }
                    }
                }
            }
            return whereClause;
        }

        private void ClearDataSet()
        {
            while (shownTables.Tables.Count > 0)
            {
                var tableName = shownTables.Tables[0].TableName;
                shownTables.Tables.Remove(tableName);
            }
        }

        private void DisconnectServer()
        {
            comboBox1.ResetText();
            DisableControls();
            textBox_SQLQueryToExecute.Text = string.Empty;
            ClearDataSet();
            ConnectionStatus_toolStripStatusLabel1.Text = "Connection Status : Disconnected.";
            FileStatus_toolStripStatusLabel2.Text = "|  File Status : Pending .....!";
            IncludeID_CheckBox.Checked = false;
            IsConnectionEstablished = false;
        }
        #endregion

        private void SQLScriptGenerator_Load(object sender, EventArgs e)
        {

        }
    }
}
