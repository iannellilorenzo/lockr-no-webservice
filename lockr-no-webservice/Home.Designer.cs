namespace lockr_no_webservice
{
    partial class Home
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
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.btnUpdateAccount = new System.Windows.Forms.Button();
            this.btnDeleteAccount = new System.Windows.Forms.Button();
            this.linklblShowHidePassword = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linklblShowHideSecretKey = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnShowPasswords = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Location = new System.Drawing.Point(470, 15);
            this.dgvAccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.RowHeadersWidth = 51;
            this.dgvAccounts.Size = new System.Drawing.Size(1229, 360);
            this.dgvAccounts.TabIndex = 0;
            this.dgvAccounts.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccounts_CellDoubleClick);
            this.dgvAccounts.SelectionChanged += new System.EventHandler(this.dgvAccounts_SelectionChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(13, 35);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(265, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(13, 98);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(265, 22);
            this.txtEmail.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(13, 161);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(265, 22);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(13, 231);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(265, 22);
            this.txtDescription.TabIndex = 5;
            // 
            // txtSecretKey
            // 
            this.txtSecretKey.Location = new System.Drawing.Point(13, 298);
            this.txtSecretKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSecretKey.Name = "txtSecretKey";
            this.txtSecretKey.Size = new System.Drawing.Size(265, 22);
            this.txtSecretKey.TabIndex = 6;
            this.txtSecretKey.UseSystemPasswordChar = true;
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Location = new System.Drawing.Point(13, 347);
            this.btnAddAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(100, 28);
            this.btnAddAccount.TabIndex = 7;
            this.btnAddAccount.Text = "Add";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // btnUpdateAccount
            // 
            this.btnUpdateAccount.Location = new System.Drawing.Point(121, 347);
            this.btnUpdateAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdateAccount.Name = "btnUpdateAccount";
            this.btnUpdateAccount.Size = new System.Drawing.Size(100, 28);
            this.btnUpdateAccount.TabIndex = 8;
            this.btnUpdateAccount.Text = "Update";
            this.btnUpdateAccount.UseVisualStyleBackColor = true;
            this.btnUpdateAccount.Click += new System.EventHandler(this.btnUpdateAccount_Click);
            // 
            // btnDeleteAccount
            // 
            this.btnDeleteAccount.Location = new System.Drawing.Point(229, 347);
            this.btnDeleteAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteAccount.Name = "btnDeleteAccount";
            this.btnDeleteAccount.Size = new System.Drawing.Size(100, 28);
            this.btnDeleteAccount.TabIndex = 9;
            this.btnDeleteAccount.Text = "Delete";
            this.btnDeleteAccount.UseVisualStyleBackColor = true;
            this.btnDeleteAccount.Click += new System.EventHandler(this.btnDeleteAccount_Click);
            // 
            // linklblShowHidePassword
            // 
            this.linklblShowHidePassword.AutoSize = true;
            this.linklblShowHidePassword.Location = new System.Drawing.Point(286, 164);
            this.linklblShowHidePassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linklblShowHidePassword.Name = "linklblShowHidePassword";
            this.linklblShowHidePassword.Size = new System.Drawing.Size(40, 16);
            this.linklblShowHidePassword.TabIndex = 4;
            this.linklblShowHidePassword.TabStop = true;
            this.linklblShowHidePassword.Text = "Show";
            this.linklblShowHidePassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblShowHidePassword_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Description";
            // 
            // linklblShowHideSecretKey
            // 
            this.linklblShowHideSecretKey.AutoSize = true;
            this.linklblShowHideSecretKey.Location = new System.Drawing.Point(286, 304);
            this.linklblShowHideSecretKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linklblShowHideSecretKey.Name = "linklblShowHideSecretKey";
            this.linklblShowHideSecretKey.Size = new System.Drawing.Size(40, 16);
            this.linklblShowHideSecretKey.TabIndex = 14;
            this.linklblShowHideSecretKey.TabStop = true;
            this.linklblShowHideSecretKey.Text = "Show";
            this.linklblShowHideSecretKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblShowHideSecretKey_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Secret Key";
            // 
            // btnShowPasswords
            // 
            this.btnShowPasswords.Location = new System.Drawing.Point(336, 336);
            this.btnShowPasswords.Name = "btnShowPasswords";
            this.btnShowPasswords.Size = new System.Drawing.Size(91, 51);
            this.btnShowPasswords.TabIndex = 16;
            this.btnShowPasswords.Text = "Show all passwords";
            this.btnShowPasswords.UseVisualStyleBackColor = true;
            this.btnShowPasswords.Click += new System.EventHandler(this.btnShowPasswords_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1712, 394);
            this.Controls.Add(this.btnShowPasswords);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linklblShowHideSecretKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAccounts);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.linklblShowHidePassword);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtSecretKey);
            this.Controls.Add(this.btnAddAccount);
            this.Controls.Add(this.btnUpdateAccount);
            this.Controls.Add(this.btnDeleteAccount);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Home";
            this.Text = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvAccounts;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Button btnUpdateAccount;
        private System.Windows.Forms.Button btnDeleteAccount;
        private System.Windows.Forms.LinkLabel linklblShowHidePassword;

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linklblShowHideSecretKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnShowPasswords;
    }
}