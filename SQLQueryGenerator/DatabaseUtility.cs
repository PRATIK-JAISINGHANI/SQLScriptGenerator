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
    public partial class DatabaseUtility : Form
    {
        public DatabaseUtility()
        {
            InitializeComponent();
            //SetDefault();
        }

        private void scriptGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQLScriptGenerator scriptGenerator = new SQLScriptGenerator();
            scriptGenerator.WindowState = FormWindowState.Minimized;
            scriptGenerator.MdiParent = this;
            scriptGenerator.Show();
            scriptGenerator.WindowState = FormWindowState.Maximized;
        }


        //private void SetDefault()
        //{
        //    SQLScriptGenerator scriptGenerator = new SQLScriptGenerator();
        //    scriptGenerator.MdiParent = this;
        //    scriptGenerator.Show();
        //}

        private void databaseScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseScanner databaseScanner = new DatabaseScanner();
            databaseScanner.WindowState = FormWindowState.Minimized;
            databaseScanner.MdiParent = this;
            databaseScanner.Show();
            databaseScanner.WindowState = FormWindowState.Maximized;
        }
    }
}
