namespace DbClassGenerator
{
    partial class MainForm
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
            this.DataSourceLabel = new System.Windows.Forms.Label();
            this.DataSourceTextBox = new System.Windows.Forms.TextBox();
            this.UserIdCheckBox = new System.Windows.Forms.CheckBox();
            this.UserIdTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TablesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RootNameSpaceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DataSourceLabel
            // 
            this.DataSourceLabel.AutoSize = true;
            this.DataSourceLabel.Location = new System.Drawing.Point(13, 10);
            this.DataSourceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DataSourceLabel.Name = "DataSourceLabel";
            this.DataSourceLabel.Size = new System.Drawing.Size(72, 15);
            this.DataSourceLabel.TabIndex = 0;
            this.DataSourceLabel.Text = "Data Source";
            // 
            // DataSourceTextBox
            // 
            this.DataSourceTextBox.Location = new System.Drawing.Point(93, 7);
            this.DataSourceTextBox.Name = "DataSourceTextBox";
            this.DataSourceTextBox.Size = new System.Drawing.Size(185, 23);
            this.DataSourceTextBox.TabIndex = 1;
            // 
            // UserIdCheckBox
            // 
            this.UserIdCheckBox.AutoSize = true;
            this.UserIdCheckBox.Location = new System.Drawing.Point(282, 9);
            this.UserIdCheckBox.Name = "UserIdCheckBox";
            this.UserIdCheckBox.Size = new System.Drawing.Size(65, 19);
            this.UserIdCheckBox.TabIndex = 2;
            this.UserIdCheckBox.Text = "User ID";
            this.UserIdCheckBox.UseVisualStyleBackColor = true;
            this.UserIdCheckBox.CheckedChanged += new System.EventHandler(this.UserIdCheckBox_CheckedChanged);
            // 
            // UserIdTextBox
            // 
            this.UserIdTextBox.Enabled = false;
            this.UserIdTextBox.Location = new System.Drawing.Point(349, 7);
            this.UserIdTextBox.Name = "UserIdTextBox";
            this.UserIdTextBox.Size = new System.Drawing.Size(185, 23);
            this.UserIdTextBox.TabIndex = 3;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(541, 11);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(57, 15);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "Password";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Enabled = false;
            this.PasswordTextBox.Location = new System.Drawing.Point(605, 8);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(185, 23);
            this.PasswordTextBox.TabIndex = 5;
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(809, 7);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 6;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // DatabasesComboBox
            // 
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.Enabled = false;
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Location = new System.Drawing.Point(93, 51);
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.Size = new System.Drawing.Size(791, 23);
            this.DatabasesComboBox.TabIndex = 7;
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.DatabasesComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Data Source";
            // 
            // TablesCheckedListBox
            // 
            this.TablesCheckedListBox.FormattingEnabled = true;
            this.TablesCheckedListBox.Location = new System.Drawing.Point(12, 80);
            this.TablesCheckedListBox.Name = "TablesCheckedListBox";
            this.TablesCheckedListBox.Size = new System.Drawing.Size(872, 310);
            this.TablesCheckedListBox.TabIndex = 9;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(796, 396);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(88, 31);
            this.GenerateButton.TabIndex = 10;
            this.GenerateButton.Text = "Generate...";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Root Namespace";
            // 
            // RootNameSpaceTextBox
            // 
            this.RootNameSpaceTextBox.Location = new System.Drawing.Point(112, 401);
            this.RootNameSpaceTextBox.Name = "RootNameSpaceTextBox";
            this.RootNameSpaceTextBox.Size = new System.Drawing.Size(678, 23);
            this.RootNameSpaceTextBox.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 484);
            this.Controls.Add(this.RootNameSpaceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.TablesCheckedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatabasesComboBox);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserIdTextBox);
            this.Controls.Add(this.UserIdCheckBox);
            this.Controls.Add(this.DataSourceTextBox);
            this.Controls.Add(this.DataSourceLabel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database .NET Classes Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DataSourceLabel;
        private System.Windows.Forms.TextBox DataSourceTextBox;
        private System.Windows.Forms.CheckBox UserIdCheckBox;
        private System.Windows.Forms.TextBox UserIdTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox TablesCheckedListBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RootNameSpaceTextBox;
    }
}

