using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lockr_no_webservice
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        // Private attributes
        private string _email;
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _passwordHash;
        private string _phoneNumber;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private string _secretKey;
        private string _verificationToken;
        private int _statusId;
        private int _roleId;

        // Properties with getter and setter
        public string Email
        {
            get => _email;
            set
            {
                try
                {
                    var mailAddress = new MailAddress(value);
                    _email = mailAddress.Address;
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid email format.");
                }
            }
        }

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
                    throw new ArgumentException("Invalid username format.");
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (Regex.IsMatch(value, @"^[A-Za-z' -]{1,35}$"))
                {
                    _firstName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid first name format.");
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (Regex.IsMatch(value, @"^[A-Za-z' -]{1,35}$"))
                {
                    _lastName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid last name format.");
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

        public string SecretKey
        {
            get => _secretKey;
            set => _secretKey = value;
        }

        public string VerificationToken
        {
            get => _verificationToken;
            set => _verificationToken = value;
        }

        public int StatusId
        {
            get => _statusId;
            set => _statusId = value;
        }

        public int RoleId
        {
            get => _roleId;
            set => _roleId = value;
        }

        public User()
        {
            Email = "default@example.com";
            Username = "defaultUser";
            FirstName = "Default";
            LastName = "User";
            PasswordHash = "Default1@";
            PhoneNumber = "+0000000000";
            CreatedAt = DateTime.MinValue;
            UpdatedAt = DateTime.MinValue;
            SecretKey = "123456";
            VerificationToken = "defaultToken";
            StatusId = 0;
            RoleId = 0;
        }

        public User(string email, string username, string firstName, string lastName, string passwordHash, string phoneNumber, DateTime createdAt, DateTime updatedAt, string secretKey, string verificationToken, int statusId, int roleId)
        {
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            SecretKey = secretKey;
            VerificationToken = verificationToken;
            StatusId = statusId;
            RoleId = roleId;
        }

        public User(User user)
        {
            Email = user.Email;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PasswordHash = user.PasswordHash;
            PhoneNumber = user.PhoneNumber;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
            SecretKey = user.SecretKey;
            VerificationToken = user.VerificationToken;
            StatusId = user.StatusId;
            RoleId = user.RoleId;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"User: {Username}, Email: {Email}, Name: {FirstName} {LastName}, Phone: {PhoneNumber}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, StatusId: {StatusId}, RoleId: {RoleId}";
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A copy of the current object.</returns>
        public User Copy()
        {
            return new User
            {
                Email = this.Email,
                Username = this.Username,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PasswordHash = this.PasswordHash,
                PhoneNumber = this.PhoneNumber,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt,
                SecretKey = this.SecretKey,
                VerificationToken = this.VerificationToken,
                StatusId = this.StatusId,
                RoleId = this.RoleId
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

            User user = (User)obj;
            return Email == user.Email &&
                   Username == user.Username &&
                   FirstName == user.FirstName &&
                   LastName == user.LastName &&
                   PasswordHash == user.PasswordHash &&
                   PhoneNumber == user.PhoneNumber &&
                   CreatedAt == user.CreatedAt &&
                   UpdatedAt == user.UpdatedAt &&
                   SecretKey == user.SecretKey &&
                   VerificationToken == user.VerificationToken &&
                   StatusId == user.StatusId &&
                   RoleId == user.RoleId;
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