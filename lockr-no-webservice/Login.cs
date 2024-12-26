using Google.Protobuf.WellKnownTypes;
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
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }

            // Verify user credentials
            if (IsValidUser(email, password))
            {
                // If the credentials are valid, open the main form
                this.Hide();
                Home homeForm = new Home();
                homeForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid email or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Verifies the user's credentials by querying the database.
        /// </summary>
        /// <param name="email">The email entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        private bool IsValidUser(string email, string password)
        {
            string query = "SELECT * FROM users WHERE email = @Email";
            var parameters = new Dictionary<string, object>
            {
                { "@Email", email }
            };

            using (MySqlDataReader reader = dbHelper.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    // Assuming you have a method to verify the hashed password
                    string storedPasswordHash = reader["PasswordHash"].ToString();
                    string salt = reader["Salt"].ToString();

                    if (VerifyHash(password, storedPasswordHash, salt))
                    {
                        // Construct the user object from the query result
                        User user = new User
                        {
                            Email = reader["Email"].ToString(),
                            Username = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            PasswordHash = storedPasswordHash,
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]),
                            SecretKey = reader["SecretKey"].ToString(),
                            VerificationToken = reader["VerificationToken"].ToString(),
                            StatusId = Convert.ToInt32(reader["StatusId"]),
                            RoleId = Convert.ToInt32(reader["RoleId"])
                        };

                        // Store the user object as needed
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Verifies the hashed password.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <param name="storedPasswordHash">The stored hashed password.</param>
        /// <param name="salt">The salt used for hashing.</param>
        /// <returns>True if the password is correct, otherwise false.</returns>
        private bool VerifyHash(string password, string storedPasswordHash, string salt)
        {
            var argon2 = new Konscious.Security.Cryptography.Argon2id(System.Text.Encoding.UTF8.GetBytes(password));
            argon2.Salt = Convert.FromBase64String(salt);
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.MemorySize = 1024 * 1024; // 1 GB
            argon2.Iterations = 4;

            string hash = Convert.ToBase64String(argon2.GetBytes(16));
            return hash == storedPasswordHash;
        }

        /// <summary>
        /// Handles the click event of the register button.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
        }
    }
}
