using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lockr_no_webservice
{
    /// <summary>
    /// Represents an account in the system.
    /// </summary>
    public class Account
    {
        // Private attributes
        private int _id;
        private string _username;
        private string _email;
        private string _password;
        private string _description;
        private string _userReference;

        // Properties with getter and setter
        /// <summary>
        /// Gets or sets the ID of the account.
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Gets or sets the username of the account.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                if (Regex.IsMatch(value, @"^[A-Za-z0-9_.-]{1,30}$"))
                {
                    _username = value;
                }
                else
                {
                    throw new ArgumentException("Invalid account name format.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the email of the account.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(value);
                    _email = mailAddress.Address;
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid email format.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the password of the account.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
                {
                    _password = value;
                }
                else
                {
                    throw new ArgumentException("Invalid password format.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the description of the account.
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Gets or sets the user reference of the account.
        /// </summary>
        public string UserReference
        {
            get => _userReference;
            set
            {
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(value);
                    _userReference = mailAddress.Address;
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid email format.");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        public Account()
        {
            Id = 0;
            Username = "defaultAccount";
            Email = "default@example.com";
            Password = "Default1@";
            Description = "Default description";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class with specified parameters.
        /// </summary>
        /// <param name="id">The ID of the account.</param>
        /// <param name="username">The username of the account.</param>
        /// <param name="email">The email of the account.</param>
        /// <param name="password">The password of the account.</param>
        /// <param name="description">The description of the account.</param>
        /// <param name="userReference">The user reference of the account.</param>
        public Account(int id, string username, string email, string password, string description, string userReference)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Description = description;
            UserReference = userReference;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class by copying another account.
        /// </summary>
        /// <param name="account">The account to copy.</param>
        public Account(Account account)
        {
            Id = account.Id;
            Username = account.Username;
            Email = account.Email;
            Password = account.Password;
            Description = account.Description;
            UserReference = account.UserReference;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Account: {Username}, Email: {Email}, Description: {Description}, UserReference: {UserReference}";
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A copy of the current object.</returns>
        public Account Copy()
        {
            return new Account
            {
                Id = this.Id,
                Username = this.Username,
                Email = this.Email,
                Password = this.Password,
                Description = this.Description,
                UserReference = this.UserReference
            };
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Account account = (Account)obj;
            return Id == account.Id &&
                   Username == account.Username &&
                   Email == account.Email &&
                   Password == account.Password &&
                   Description == account.Description &&
                   UserReference == account.UserReference;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}