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
            scriptGenerator.MdiParent = this;
            scriptGenerator.Show();
        }

        private void SetDefault()
        {
            SQLScriptGenerator scriptGenerator = new SQLScriptGenerator();
            scriptGenerator.MdiParent = this;
            scriptGenerator.Show();
        }
    }
}
