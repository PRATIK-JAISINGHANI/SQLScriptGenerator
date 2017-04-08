namespace SQLQueryGenerator
{
    partial class Form1
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
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.usermasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usermasterTableAdapter = new SQLQueryGenerator._TrialDB_3DataSetTableAdapters.UsermasterTableAdapter();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label_connectionType = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.usermasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Server
            // 
            this.textBox_Server.Location = new System.Drawing.Point(149, 48);
            this.textBox_Server.Name = "textBox_Server";
            this.textBox_Server.Size = new System.Drawing.Size(484, 20);
            this.textBox_Server.TabIndex = 0;
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(23, 51);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(38, 13);
            this.Server.TabIndex = 1;
            this.Server.Text = "Server";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(21, 80);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(55, 13);
            this.Username.TabIndex = 2;
            this.Username.Text = "Username";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(21, 110);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(53, 13);
            this.Password.TabIndex = 3;
            this.Password.Text = "Password";
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(149, 77);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(240, 20);
            this.textBox_Username.TabIndex = 4;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(149, 107);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(240, 20);
            this.textBox_Password.TabIndex = 5;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(433, 110);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(0, 13);
            this.Status.TabIndex = 6;
            // 
            // button_ConnectServer
            // 
            this.button_ConnectServer.Location = new System.Drawing.Point(436, 133);
            this.button_ConnectServer.Name = "button_ConnectServer";
            this.button_ConnectServer.Size = new System.Drawing.Size(197, 22);
            this.button_ConnectServer.TabIndex = 11;
            this.button_ConnectServer.Text = "Connect Server";
            this.button_ConnectServer.UseVisualStyleBackColor = true;
            this.button_ConnectServer.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataBases
            // 
            this.DataBases.AutoSize = true;
            this.DataBases.Location = new System.Drawing.Point(21, 138);
            this.DataBases.Name = "DataBases";
            this.DataBases.Size = new System.Drawing.Size(85, 13);
            this.DataBases.TabIndex = 9;
            this.DataBases.Text = "DataBase Name";
            // 
            // textBox_DataBaseName
            // 
            this.textBox_DataBaseName.Location = new System.Drawing.Point(149, 135);
            this.textBox_DataBaseName.Name = "textBox_DataBaseName";
            this.textBox_DataBaseName.Size = new System.Drawing.Size(240, 20);
            this.textBox_DataBaseName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "SQL Query To Execute";
            // 
            // textBox_SQLQueryToExecute
            // 
            this.textBox_SQLQueryToExecute.Location = new System.Drawing.Point(149, 187);
            this.textBox_SQLQueryToExecute.Multiline = true;
            this.textBox_SQLQueryToExecute.Name = "textBox_SQLQueryToExecute";
            this.textBox_SQLQueryToExecute.Size = new System.Drawing.Size(730, 129);
            this.textBox_SQLQueryToExecute.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Execute Query";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button_DisconnectServer
            // 
            this.button_DisconnectServer.Location = new System.Drawing.Point(683, 133);
            this.button_DisconnectServer.Name = "button_DisconnectServer";
            this.button_DisconnectServer.Size = new System.Drawing.Size(196, 22);
            this.button_DisconnectServer.TabIndex = 14;
            this.button_DisconnectServer.Text = "Disconnect Server";
            this.button_DisconnectServer.UseVisualStyleBackColor = true;
            this.button_DisconnectServer.Click += new System.EventHandler(this.button_DisconnectServer_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(296, 335);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.dataGridView1.Location = new System.Drawing.Point(149, 376);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(730, 150);
            this.dataGridView1.TabIndex = 17;
            // 
            // label_connectionType
            // 
            this.label_connectionType.AutoSize = true;
            this.label_connectionType.Location = new System.Drawing.Point(23, 23);
            this.label_connectionType.Name = "label_connectionType";
            this.label_connectionType.Size = new System.Drawing.Size(88, 13);
            this.label_connectionType.TabIndex = 18;
            this.label_connectionType.Text = "Connection Type";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Connection"});
            this.comboBox1.Location = new System.Drawing.Point(149, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(484, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(911, 587);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label_connectionType);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
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
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Script Generator";
            ((System.ComponentModel.ISupportInitialize)(this.usermasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource usermasterBindingSource;
        private _TrialDB_3DataSetTableAdapters.UsermasterTableAdapter usermasterTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label_connectionType;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

