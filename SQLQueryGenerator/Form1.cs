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

namespace SQLQueryGenerator
{
    public partial class Form1 : Form
    {
        #region Public Methods

        public Form1()
        {
            InitializeComponent();
            Status.Text = "Disconnected!";
            DisableControls();
            LoadItemsForConnectionType();
        }

        #endregion

        #region Helper Methods

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection;
                var connectionString = BuildConnectionString();
                connection = new SqlConnection(connectionString);
                connection.Open();
                Status.Text = "Connected to " + textBox_Server.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SQLQueryToExecute.Text))
                throw new Exception("Please insert the query to execute.");
            //
            var connectionString = BuildConnectionString();
            //
            var sqlQueryToExecute = textBox_SQLQueryToExecute.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQueryToExecute, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        List<string> columnsList = new List<string>();

                        var dt = dataReader.GetSchemaTable();
                        foreach (DataRow column in dt.Rows)
                            columnsList.Add(column[0].ToString());
                        //

                        if(sqlQueryToExecute.ToUpper().Contains("SELECT * FROM"))
                        {
                            string query = string.Empty;

                            foreach (var item in columnsList)
                                if (item != "VersionNo")
                                    if (string.IsNullOrEmpty(query))
                                        query = item;
                                    else
                                        query = query + ", " + item;

                            sqlQueryToExecute = sqlQueryToExecute.Replace("*", query);
                        }


                        try
                        {
                            var dt2 = new DataTable();
                            dt2.Load(dataReader);
                            dataGridView1.DataSource = dt2;
                        }
                        catch(Exception ex)
                        {
                            throw new DataException("Check this Error : ", ex);
                        }
                    }
                }
            }
            //
        }

        private void button_DisconnectServer_Click(object sender, EventArgs e)
        {
            //connection.Close();
            Status.Text = "Disconnected!";
        }

        private string BuildConnectionString()
        {
            try
            {
                if (string.IsNullOrEmpty(textBox_Server.Text) ||
                string.IsNullOrEmpty(textBox_Username.Text) ||
                string.IsNullOrEmpty(textBox_Password.Text) ||
                string.IsNullOrEmpty(textBox_DataBaseName.Text))
                    return string.Format("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrialDB-3;Integrated Security=True");
                //throw new Exception("Please insert the Server / UserName & Password to connect.");
                else {
                    //
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
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label2.Text = folderBrowserDialog1.SelectedPath;
            }
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
        
        #endregion
    }
}
