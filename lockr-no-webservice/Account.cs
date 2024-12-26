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
        private string _accountName;
        private string _email;
        private string _passwordHash;
        private string _phoneNumber;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        // Properties with getter and setter
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string AccountName
        {
            get => _accountName;
            set
            {
                if (Regex.IsMatch(value, @"^[A-Za-z0-9_.-]{1,30}$"))
                {
                    _accountName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid account name format.");
                }
            }
        }

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

        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
                {
                    _passwordHash = value;
                }
                else
                {
                    throw new ArgumentException("Invalid password format.");
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (Regex.IsMatch(value, @"^\+?(\d{1,3})?[-.\s]?(\(?\d{1,4}\)?)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}$"))
                {
                    _phoneNumber = value;
                }
                else
                {
                    throw new ArgumentException("Invalid phone number format.");
                }
            }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public DateTime UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }

        public Account()
        {
            Id = 0;
            AccountName = "defaultAccount";
            Email = "default@example.com";
            PasswordHash = "Default1@";
            PhoneNumber = "+0000000000";
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
        }

        public Account(int id, string accountName, string email, string passwordHash, string phoneNumber, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            AccountName = accountName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Account(Account account)
        {
            Id = account.Id;
            AccountName = account.AccountName;
            Email = account.Email;
            PasswordHash = account.PasswordHash;
            PhoneNumber = account.PhoneNumber;
            CreatedAt = account.CreatedAt;
            UpdatedAt = account.UpdatedAt;
        }



        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Account: {AccountName}, Email: {Email}, Phone: {PhoneNumber}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
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
                AccountName = this.AccountName,
                Email = this.Email,
                PasswordHash = this.PasswordHash,
                PhoneNumber = this.PhoneNumber,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
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
                   AccountName == account.AccountName &&
                   Email == account.Email &&
                   PasswordHash == account.PasswordHash &&
                   PhoneNumber == account.PhoneNumber &&
                   CreatedAt == account.CreatedAt &&
                   UpdatedAt == account.UpdatedAt;
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
