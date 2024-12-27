﻿using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace lockr_no_webservice
{
    public partial class Login : Form
    {
        private DatabaseHelper dbHelper;
        public User CurrentUser { get; private set; }

        public Login()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        /// <summary>
        /// Handles the click event of the login button.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[A-Za-z0-9_.-]{1,30}$"))
            {
                MessageBox.Show("Invalid username format. Username must be between 1 and 30 characters. Only lowercase and uppercase letters, digits and special characters (`-`, `_`, `.`) are accepted", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
            {
                MessageBox.Show("Invalid password format. Password must be between 8 and 32 characters. At least one lowercase letter, one uppercase letter and special character (`@`, `$`, `!`, `%`, `?`, `&`)", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CurrentUser = new();
            CurrentUser.Username = username;

            // Verify user credentials
            if (CurrentUser.VerifyCredentials(password))
            {
                // If the credentials are valid, open the main form
                this.Hide();
                Home homeForm = new Home();
                homeForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event of the register button.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register registerForm = new Register();
            registerForm.FormClosed += (s, args) =>
            {
                if (registerForm.RegisteredUser != null)
                {
                    // Access the registered user object here
                    User registeredUser = registerForm.RegisteredUser;
                    MessageBox.Show($"We are happy you chose Lockr as your password manager, {registeredUser.Username}!\nPlease log in to start using Lockr.", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Show();
            };
            registerForm.Show();
        }
    }
}
