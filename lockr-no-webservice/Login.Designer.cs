using System;
using System.Drawing;
using System.Windows.Forms;

namespace lockr_no_webservice
{
    partial class Login
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblLockr = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.linklblShowHidePassword = new System.Windows.Forms.LinkLabel();

            // Main Panel
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Padding = new Padding(40);

            // Logo Panel
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelLogo.Size = new System.Drawing.Size(400, 120);
            this.panelLogo.Dock = DockStyle.Top;

            // Logo Text
            this.lblLockr.Text = "LOCKR";
            this.lblLockr.Font = new System.Drawing.Font("Segoe UI", 32F, FontStyle.Bold);
            this.lblLockr.ForeColor = System.Drawing.Color.White;
            this.lblLockr.TextAlign = ContentAlignment.MiddleCenter;
            this.lblLockr.Dock = DockStyle.Fill;
            this.panelLogo.Controls.Add(this.lblLockr);

            // Welcome Text
            this.lblWelcome.Text = "Welcome Back";
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, FontStyle.Regular);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblWelcome.Location = new System.Drawing.Point(40, 140);
            this.lblWelcome.Size = new System.Drawing.Size(320, 40);

            // Username TextBox
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.Location = new System.Drawing.Point(40, 190);
            this.txtUsername.Size = new System.Drawing.Size(320, 30);
            this.txtUsername.PlaceholderText = "Username";
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;

            // Password TextBox
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.Location = new System.Drawing.Point(40, 250);
            this.txtPassword.Size = new System.Drawing.Size(320, 30);
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // Show/Hide Password Link
            this.linklblShowHidePassword.AutoSize = true;
            this.linklblShowHidePassword.Location = new System.Drawing.Point(320, 233);
            this.linklblShowHidePassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linklblShowHidePassword.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.linklblShowHidePassword.LinkBehavior = LinkBehavior.NeverUnderline;
            this.linklblShowHidePassword.Text = "Show";
            this.linklblShowHidePassword.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linklblShowHidePassword_LinkClicked);
            this.linklblShowHidePassword.BringToFront();

            // Login Button
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Size = new System.Drawing.Size(320, 40);
            this.btnLogin.Location = new System.Drawing.Point(40, 300);
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // Register Text
            this.label1.Text = "Don't have an account?";
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(40, 360);
            this.label1.AutoSize = true;

            // Register Button - Moving it more to the right
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnRegister.Size = new System.Drawing.Size(100, 30);
            this.btnRegister.Location = new System.Drawing.Point(200, 355);
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRegister.Text = "Register here";
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

            // Form Properties
            this.ClientSize = new System.Drawing.Size(400, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // Add Controls
            this.Controls.AddRange(new Control[] {
                this.panelLogo,
                this.lblWelcome,
                this.txtUsername,
                this.txtPassword,
                this.linklblShowHidePassword,
                this.btnLogin,
                this.label1,
                this.btnRegister
            });
        }

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblLockr;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel linklblShowHidePassword;
    }

    #endregion
}