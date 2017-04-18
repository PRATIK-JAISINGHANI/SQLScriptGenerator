namespace SQLQueryGenerator
{
    partial class SQLScriptGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox_Server = new System.Windows.Forms.TextBox();
            this.Server = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.Status = new System.Windows.Forms.Label();
            this.button_ConnectServer = new System.Windows.Forms.Button();
            this.DataBases = new System.Windows.Forms.Label();
            this.textBox_DataBaseName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_SQLQueryToExecute = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_DisconnectServer = new System.Windows.Forms.Button();
            this.button_GenerateScript = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.usermasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usermasterTableAdapter = new SQLQueryGenerator._TrialDB_3DataSetTableAdapters.UsermasterTableAdapter();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label_connectionType = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ConnectionStatus_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileStatus_toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.IncludeID_CheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.usermasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Server
            // 
            this.textBox_Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Server.Location = new System.Drawing.Point(169, 47);
            this.textBox_Server.Name = "textBox_Server";
            this.textBox_Server.Size = new System.Drawing.Size(484, 22);
            this.textBox_Server.TabIndex = 2;
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Server.Location = new System.Drawing.Point(12, 48);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(48, 18);
            this.Server.TabIndex = 1;
            this.Server.Text = "Server";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(12, 77);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(69, 18);
            this.Username.TabIndex = 2;
            this.Username.Text = "Username";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.Location = new System.Drawing.Point(12, 107);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(65, 18);
            this.Password.TabIndex = 3;
            this.Password.Text = "Password";
            // 
            // textBox_Username
            // 
            this.textBox_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Username.Location = new System.Drawing.Point(169, 76);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(240, 22);
            this.textBox_Username.TabIndex = 3;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Password.Location = new System.Drawing.Point(169, 106);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.PasswordChar = '*';
            this.textBox_Password.Size = new System.Drawing.Size(240, 22);
            this.textBox_Password.TabIndex = 4;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(455, 82);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(0, 13);
            this.Status.TabIndex = 6;
            // 
            // button_ConnectServer
            // 
            this.button_ConnectServer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_ConnectServer.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ConnectServer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_ConnectServer.Location = new System.Drawing.Point(468, 119);
            this.button_ConnectServer.Name = "button_ConnectServer";
            this.button_ConnectServer.Size = new System.Drawing.Size(197, 35);
            this.button_ConnectServer.TabIndex = 6;
            this.button_ConnectServer.Text = "Connect Server";
            this.button_ConnectServer.UseVisualStyleBackColor = false;
            this.button_ConnectServer.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataBases
            // 
            this.DataBases.AutoSize = true;
            this.DataBases.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataBases.Location = new System.Drawing.Point(12, 135);
            this.DataBases.Name = "DataBases";
            this.DataBases.Size = new System.Drawing.Size(101, 18);
            this.DataBases.TabIndex = 9;
            this.DataBases.Text = "DataBase Name";
            // 
            // textBox_DataBaseName
            // 
            this.textBox_DataBaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DataBaseName.Location = new System.Drawing.Point(169, 134);
            this.textBox_DataBaseName.Name = "textBox_DataBaseName";
            this.textBox_DataBaseName.Size = new System.Drawing.Size(240, 22);
            this.textBox_DataBaseName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "SQL Query To Execute";
            // 
            // textBox_SQLQueryToExecute
            // 
            this.textBox_SQLQueryToExecute.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SQLQueryToExecute.Location = new System.Drawing.Point(169, 186);
            this.textBox_SQLQueryToExecute.Multiline = true;
            this.textBox_SQLQueryToExecute.Name = "textBox_SQLQueryToExecute";
            this.textBox_SQLQueryToExecute.Size = new System.Drawing.Size(730, 129);
            this.textBox_SQLQueryToExecute.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(917, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 48);
            this.button1.TabIndex = 8;
            this.button1.Text = "Execute Query";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button_DisconnectServer
            // 
            this.button_DisconnectServer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_DisconnectServer.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_DisconnectServer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_DisconnectServer.Location = new System.Drawing.Point(703, 119);
            this.button_DisconnectServer.Name = "button_DisconnectServer";
            this.button_DisconnectServer.Size = new System.Drawing.Size(196, 35);
            this.button_DisconnectServer.TabIndex = 11;
            this.button_DisconnectServer.Text = "Disconnect Server";
            this.button_DisconnectServer.UseVisualStyleBackColor = false;
            this.button_DisconnectServer.Click += new System.EventHandler(this.button_DisconnectServer_Click);
            // 
            // button_GenerateScript
            // 
            this.button_GenerateScript.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_GenerateScript.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_GenerateScript.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_GenerateScript.Location = new System.Drawing.Point(917, 426);
            this.button_GenerateScript.Name = "button_GenerateScript";
            this.button_GenerateScript.Size = new System.Drawing.Size(104, 48);
            this.button_GenerateScript.TabIndex = 10;
            this.button_GenerateScript.Text = "Generate Script";
            this.button_GenerateScript.UseVisualStyleBackColor = false;
            this.button_GenerateScript.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 16;
            // 
            // usermasterTableAdapter
            // 
            this.usermasterTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(169, 344);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(730, 150);
            this.dataGridView1.TabIndex = 17;
            // 
            // label_connectionType
            // 
            this.label_connectionType.AutoSize = true;
            this.label_connectionType.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_connectionType.Location = new System.Drawing.Point(12, 21);
            this.label_connectionType.Name = "label_connectionType";
            this.label_connectionType.Size = new System.Drawing.Size(110, 18);
            this.label_connectionType.TabIndex = 18;
            this.label_connectionType.Text = "Connection Type";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Connection"});
            this.comboBox1.Location = new System.Drawing.Point(169, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(484, 24);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionStatus_toolStripStatusLabel1,
            this.FileStatus_toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 523);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1038, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.ConnectionStatus_toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.ConnectionStatus_toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.ConnectionStatus_toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.FileStatus_toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.FileStatus_toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.FileStatus_toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(917, 361);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 49);
            this.button2.TabIndex = 9;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // checkBox1
            // 
            this.IncludeID_CheckBox.AutoSize = true;
            this.IncludeID_CheckBox.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncludeID_CheckBox.Location = new System.Drawing.Point(15, 361);
            this.IncludeID_CheckBox.Name = "checkBox1";
            this.IncludeID_CheckBox.Size = new System.Drawing.Size(151, 22);
            this.IncludeID_CheckBox.TabIndex = 21;
            this.IncludeID_CheckBox.Text = "Include \"ID\" Column";
            this.IncludeID_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1038, 545);
            this.Controls.Add(this.IncludeID_CheckBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label_connectionType);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_GenerateScript);
            this.Controls.Add(this.button_DisconnectServer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_SQLQueryToExecute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_DataBaseName);
            this.Controls.Add(this.DataBases);
            this.Controls.Add(this.button_ConnectServer);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.textBox_Server);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Script Generator";
            this.TransparencyKey = System.Drawing.Color.Silver;
            ((System.ComponentModel.ISupportInitialize)(this.usermasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Server;
        private System.Windows.Forms.Label Server;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Button button_ConnectServer;
        private System.Windows.Forms.Label DataBases;
        private System.Windows.Forms.TextBox textBox_DataBaseName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_SQLQueryToExecute;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_DisconnectServer;
        private System.Windows.Forms.Button button_GenerateScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource usermasterBindingSource;
        private _TrialDB_3DataSetTableAdapters.UsermasterTableAdapter usermasterTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label_connectionType;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionStatus_toolStripStatusLabel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripStatusLabel FileStatus_toolStripStatusLabel2;
        private System.Windows.Forms.CheckBox IncludeID_CheckBox;
    }
}

