using System;
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

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Hash the password and generate salt
            var user = new User();
            string hashedPassword = user.Argon2idHash(password);

            // Insert the new user into the database
            string query = "INSERT INTO users (email, username, first_name, last_name, phone_number, password_hash, created_at, updated_at, secret_key, status_id, role_id) VALUES (@Email, @Username, @PasswordHash, @CreatedAt, @UpdatedAt)";
            var parameters = new Dictionary<string, object>
            {
                { "@Email", email },
                { "@Username", username },
                { "@PasswordHash", hashedPassword },
                { "@CreatedAt", DateTime.Now },
                { "@UpdatedAt", DateTime.Now }
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
