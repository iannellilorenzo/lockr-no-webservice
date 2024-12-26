﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lockr_no_webservice
{
    public partial class Register : Form
    {
        private DatabaseHelper dbHelper;

        public Register()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string secretKey = txtSecretKey.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                email = mailAddress.Address;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[A-Za-z0-9_.-]{1,30}$"))
            {
                MessageBox.Show("Invalid username format. Username must be between 1 and 30 characters. Only lowercase and uppercase letters, digits and special characters (`-`, `_`, `.`) are accepted", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
            {
                MessageBox.Show("Invalid password format. Password must be between 8 and 32 characters. At least one lowercase letter, one uppercase letter and special character (`@`, `$`, `!`, `%`, `?`, `&`)", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Confirm Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Phone Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+?(\d{1,3})?[-.\s]?(\(?\d{1,4}\)?)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}$"))
            {
                MessageBox.Show("Invalid phone number format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                MessageBox.Show("Secret Key is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(secretKey, @"^\d{6}$"))
            {
                MessageBox.Show("Invalid secret key format. Secret key must be a 6-digits PIN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hash the password and generate salt
            var user = new User();
            string hashedPassword = user.Argon2idHash(password);
            string hashedSecretKey = user.Argon2idHash(secretKey);

            // Insert the new user into the database
            string query = "INSERT INTO users (email, username, first_name, last_name, phone_number, password_hash, created_at, updated_at, secret_key, status_id, role_id) VALUES (@Email, @Username, @FirstName, @LastName, @PhoneNumber, @PasswordHash, @CreatedAt, @UpdatedAt, @SecretKey, @StatusId, @RoleId)";
            var parameters = new Dictionary<string, object>
            {
                { "@Email", email },
                { "@Username", username },
                { "@FirstName", firstName },
                { "@LastName", lastName },
                { "@PhoneNumber", phoneNumber },
                { "@PasswordHash", hashedPassword },
                { "@CreatedAt", DateTime.Now },
                { "@UpdatedAt", DateTime.Now },
                { "@SecretKey", hashedSecretKey },
                { "@StatusId", 1 }, // Assuming 1 is the default status ID
                { "@RoleId", 1 } // Assuming 1 is the default role ID
            };

            try
            {
                dbHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Registration successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}