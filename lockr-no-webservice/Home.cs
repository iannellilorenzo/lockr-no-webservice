using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace lockr_no_webservice
{
    public partial class Home : Form
    {
        private DatabaseHelper dbHelper;
        private List<Account> accounts;

        public Home()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            string query = "SELECT * FROM accounts";
            using (MySqlDataReader reader = dbHelper.ExecuteQuery(query, new Dictionary<string, object>()))
            {
                accounts = new List<Account>();
                while (reader.Read())
                {
                    Account account = new Account
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Username = reader["username"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Description = reader["description"].ToString(),
                        UserReference = reader["user_reference"].ToString()
                    };
                    accounts.Add(account);
                }
            }
            dgvAccounts.DataSource = accounts;
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            Account account = new Account
            {
                Username = txtUsername.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Description = txtDescription.Text,
                UserReference = txtUserReference.Text
            };

            string query = "INSERT INTO accounts (username, email, password, description, user_reference) VALUES (@Username, @Email, @Password, @Description, @UserReference)";
            var parameters = new Dictionary<string, object>
                {
                    { "@Username", account.Username },
                    { "@Email", account.Email },
                    { "@Password", account.Password },
                    { "@Description", account.Description },
                    { "@UserReference", account.UserReference }
                };

            try
            {
                dbHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Account added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);
                Account account = accounts.First(a => a.Id == selectedId);

                account.Username = txtUsername.Text;
                account.Email = txtEmail.Text;
                account.Password = txtPassword.Text;
                account.Description = txtDescription.Text;
                account.UserReference = txtUserReference.Text;

                string query = "UPDATE accounts SET username = @Username, email = @Email, password = @Password, description = @Description, user_reference = @UserReference WHERE id = @Id";
                var parameters = new Dictionary<string, object>
                    {
                        { "@Username", account.Username },
                        { "@Email", account.Email },
                        { "@Password", account.Password },
                        { "@Description", account.Description },
                        { "@UserReference", account.UserReference },
                        { "@Id", account.Id }
                    };

                try
                {
                    dbHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccounts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an account to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);

                string query = "DELETE FROM accounts WHERE id = @Id";
                var parameters = new Dictionary<string, object>
                    {
                        { "@Id", selectedId }
                    };

                try
                {
                    dbHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Account deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccounts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);
                Account account = accounts.First(a => a.Id == selectedId);

                txtUsername.Text = account.Username;
                txtEmail.Text = account.Email;
                txtPassword.Text = account.Password;
                txtDescription.Text = account.Description;
                txtUserReference.Text = account.UserReference;
            }
        }

        private void linklblShowHidePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                linklblShowHidePassword.Text = "Hide";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                linklblShowHidePassword.Text = "Show";
            }
        }
    }
}
