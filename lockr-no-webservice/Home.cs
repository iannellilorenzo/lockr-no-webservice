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
using System.Text.RegularExpressions;
using Org.BouncyCastle.Utilities.Encoders;

namespace lockr_no_webservice
{
    public partial class Home : Form
    {
        private DatabaseHelper dbHelper;
        private List<Account> accounts;
        private User currentUser;
        private string plainSecretKey;

        public Home(User user)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            currentUser = user;
            dgvAccounts.AutoGenerateColumns = false; // Disable auto-generation of columns
            dgvAccounts.AllowUserToAddRows = false; // Disable the addition of new rows by the user
            InitializeDataGridViewColumns();
            RequestSecretKey();
            LoadAccounts();
        }

        /// <summary>
        /// Initializes the columns of the DataGridView.
        /// </summary>
        private void InitializeDataGridViewColumns()
        {
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Visible = false
            });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "Username",
                ReadOnly = true
            });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                ReadOnly = true
            });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Password",
                DataPropertyName = "Password",
                HeaderText = "Password",
                ReadOnly = false
            });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                DataPropertyName = "Description",
                HeaderText = "Description",
                ReadOnly = true
            });
        }

        /// <summary>
        /// Requests the secret key from the user and validates it.
        /// </summary>
        private void RequestSecretKey()
        {
            using (var form = new Form())
            {
                // Form properties
                form.Text = "Secret Key Required";
                form.Size = new Size(400, 200);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.BackColor = Color.White;

                // Label
                var label = new Label
                {
                    Text = "Enter Secret Key:",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(40, 30),
                    Size = new Size(320, 20)
                };

                // TextBox
                var textBox = new TextBox
                {
                    Font = new Font("Segoe UI", 12F),
                    Location = new Point(40, 60),
                    Size = new Size(320, 30),
                    UseSystemPasswordChar = true,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Button
                var buttonOk = new Button
                {
                    Text = "Confirm",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(40, 110),
                    Size = new Size(320, 40),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(52, 73, 94),
                    ForeColor = Color.White
                };

                buttonOk.Click += (sender, e) => { form.Close(); };
                buttonOk.DialogResult = DialogResult.OK;

                // Add controls
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk });
                form.AcceptButton = buttonOk;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    string enteredSecretKey = textBox.Text;
                    if (currentUser.VerifyHash(currentUser.SecretKey, enteredSecretKey))
                    {
                        plainSecretKey = enteredSecretKey;
                    }
                    else
                    {
                        MessageBox.Show("Invalid secret key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RequestSecretKey();
                    }
                }
                else
                {
                    MessageBox.Show("Secret key is required to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RequestSecretKey();
                }
            }
        }

        /// <summary>
        /// Loads the accounts from the database and binds them to the DataGridView.
        /// </summary>
        private void LoadAccounts()
        {
            string query = "SELECT * FROM accounts WHERE user_reference = @UserReference";
            var parameters = new Dictionary<string, object>
                {
                    { "@UserReference", currentUser.Email }
                };

            using (MySqlDataReader reader = dbHelper.ExecuteQuery(query, parameters))
            {
                accounts = new List<Account>();
                while (reader.Read())
                {
                    Account account = new Account(
                        Convert.ToInt32(reader["id"]),
                        reader["username"].ToString(),
                        reader["email"].ToString(),
                        reader["password"].ToString(),
                        reader["description"].ToString(),
                        reader["user_reference"].ToString(),
                        plainSecretKey
                    );
                    accounts.Add(account);
                }
            }

            // Bind the data to the existing columns
            dgvAccounts.DataSource = new BindingList<Account>(accounts);
        }

        /// <summary>
        /// Validates the user inputs.
        /// </summary>
        /// <returns>True if inputs are valid, otherwise false.</returns>
        private bool ValidateInputs()
        {
            if (!Regex.IsMatch(txtUsername.Text, @"^[A-Za-z0-9_.-]{1,30}$"))
            {
                MessageBox.Show("Invalid username format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(txtPassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
            {
                MessageBox.Show("Invalid password format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(txtSecretKey.Text, @"^\d{6}$"))
            {
                MessageBox.Show("Invalid secret key format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the click event of the Add Account button.
        /// </summary>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            Account account = new Account(0, txtUsername.Text, txtEmail.Text, txtPassword.Text, txtDescription.Text, currentUser.Email, txtSecretKey.Text);

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

        /// <summary>
        /// Handles the click event of the Update Account button.
        /// </summary>
        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);
                Account account = accounts.First(a => a.Id == selectedId);

                if (!string.IsNullOrEmpty(txtUsername.Text))
                {
                    account.Username = txtUsername.Text;
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    account.Email = txtEmail.Text;
                }

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    account.Password = Account.Encrypt(txtPassword.Text, plainSecretKey);
                }

                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    account.Description = txtDescription.Text;
                }

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

        /// <summary>
        /// Handles the click event of the Delete Account button.
        /// </summary>
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

        private bool IsDecrypted()
        {
            string pass = dgvAccounts.Rows[0].Cells["Password"].Value.ToString();
            bool decrypted = true;

            try
            {
                string noBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(pass));
                decrypted = false;
            }
            catch
            {
                decrypted = true;
            }

            return decrypted;
        }

        /// <summary>
        /// Handles the selection changed event of the DataGridView.
        /// </summary>
        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);
                Account account = accounts.First(a => a.Id == selectedId);

                txtUsername.Text = account.Username;
                txtEmail.Text = account.Email;

                if (IsDecrypted())
                {
                    txtPassword.Text = account.Password;
                }
                else
                {
                    txtPassword.Text = Account.Decrypt(account.Password, plainSecretKey);
                }

                txtDescription.Text = account.Description;
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the DataGridView to copy cell content.
        /// </summary>
        private void dgvAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Clipboard.SetText(cell.Value.ToString());
                MessageBox.Show("Cell content copied to clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Shows or hides the info in the specified field.
        /// </summary>
        /// <param name="field">Field that will be hid or shown.</param>
        /// <param name="showHide">LinkLabel to change text to.</param>
        private void ShowHideInfo(TextBox field, LinkLabel showHide)
        {
            if (field.UseSystemPasswordChar)
            {
                field.UseSystemPasswordChar = false;
                showHide.Text = "Hide";
            }
            else
            {
                field.UseSystemPasswordChar = true;
                showHide.Text = "Show";
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the Show/Hide Password link label.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void linklblShowHidePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowHideInfo(txtPassword, linklblShowHidePassword);
        }

        /// <summary>
        /// Handles the LinkClicked event of the Show/Hide Secret Key link label.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void linklblShowHideSecretKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowHideInfo(txtSecretKey, linklblShowHideSecretKey);
        }

        private void ShowEncryptedPasswords()
        {
            foreach (DataGridViewRow row in dgvAccounts.Rows)
            {
                if (row.Cells["Password"].Value != null)
                {
                    string plainPassword = row.Cells["Password"].Value.ToString();
                    string encryptedPassword = Account.Encrypt(plainPassword, plainSecretKey);
                    row.Cells["Password"].Value = encryptedPassword;
                }
            }
        }

        private void ShowDecryptedPasswords()
        {
            foreach (DataGridViewRow row in dgvAccounts.Rows)
            {
                if (row.Cells["Password"].Value != null)
                {
                    string encryptedPassword = row.Cells["Password"].Value.ToString();
                    string decryptedPassword = Account.Decrypt(encryptedPassword, plainSecretKey);
                    row.Cells["Password"].Value = decryptedPassword;
                }
            }
        }

        private void ShowHidePasswords()
        {
            bool decrypted = IsDecrypted();

            if (decrypted)
            {
                ShowEncryptedPasswords();
                btnShowPasswords.Text = "Show all passwords";
            }
            else
            {
                ShowDecryptedPasswords();
                btnShowPasswords.Text = "Hide all passwords";
            }
        }

        private void btnShowPasswords_Click(object sender, EventArgs e)
        {
            ShowHidePasswords();
        }
    }
}
