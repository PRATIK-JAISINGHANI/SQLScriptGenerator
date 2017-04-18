namespace SQLQueryGenerator
{
    partial class DefaultConnectionString
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
            this.defaultCOnnection_Textbox = new System.Windows.Forms.TextBox();
            this.submit_button = new System.Windows.Forms.Button();
            this.DefaultConnection_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // defaultCOnnection_Textbox
            // 
            this.defaultCOnnection_Textbox.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultCOnnection_Textbox.Location = new System.Drawing.Point(29, 66);
            this.defaultCOnnection_Textbox.Name = "defaultCOnnection_Textbox";
            this.defaultCOnnection_Textbox.Size = new System.Drawing.Size(561, 23);
            this.defaultCOnnection_Textbox.TabIndex = 0;
            // 
            // submit_button
            // 
            this.submit_button.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit_button.Location = new System.Drawing.Point(473, 104);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(117, 41);
            this.submit_button.TabIndex = 1;
            this.submit_button.Text = "Submit";
            this.submit_button.UseVisualStyleBackColor = true;
            // 
            // DefaultConnection_label
            // 
            this.DefaultConnection_label.AutoSize = true;
            this.DefaultConnection_label.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefaultConnection_label.Location = new System.Drawing.Point(26, 30);
            this.DefaultConnection_label.Name = "DefaultConnection_label";
            this.DefaultConnection_label.Size = new System.Drawing.Size(330, 18);
            this.DefaultConnection_label.TabIndex = 2;
            this.DefaultConnection_label.Text = "Please mention your local server connection string :";
            // 
            // DefaultConnectionString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 165);
            this.Controls.Add(this.DefaultConnection_label);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.defaultCOnnection_Textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DefaultConnectionString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection String Required";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox defaultCOnnection_Textbox;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.Label DefaultConnection_label;
    }
}