using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

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

        /// <summary>
        /// Gets or sets the email of the user.
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
        /// Gets or sets the username of the user.
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
                    throw new ArgumentException("Invalid username format.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the password hash of the user.
        /// </summary>
        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
                {
                    _passwordHash = Argon2idHash(value);
                }
                else
                {
                    throw new ArgumentException("Invalid password format.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the creation date of the user.
        /// </summary>
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        /// <summary>
        /// Gets or sets the last update date of the user.
        /// </summary>
        public DateTime UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }

        /// <summary>
        /// Gets or sets the secret key of the user.
        /// </summary>
        public string SecretKey
        {
            get => _secretKey;
            set
            {
                if (Regex.IsMatch(value, @"^\d{6}$"))
                {
                    _secretKey = Argon2idHash(value);
                }
                else
                {
                    throw new ArgumentException("Invalid secret key format.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the verification token of the user.
        /// </summary>
        public string VerificationToken
        {
            get => _verificationToken;
            set => _verificationToken = Argon2idHash(value);
        }

        /// <summary>
        /// Gets or sets the status ID of the user.
        /// </summary>
        public int StatusId
        {
            get => _statusId;
            set => _statusId = value;
        }

        /// <summary>
        /// Gets or sets the role ID of the user.
        /// </summary>
        public int RoleId
        {
            get => _roleId;
            set => _roleId = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with specified parameters.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="passwordHash">The password hash of the user.</param>
        /// <param name="phoneNumber">The phone number of the user.</param>
        /// <param name="createdAt">The creation date of the user.</param>
        /// <param name="updatedAt">The last update date of the user.</param>
        /// <param name="secretKey">The secret key of the user.</param>
        /// <param name="verificationToken">The verification token of the user.</param>
        /// <param name="statusId">The status ID of the user.</param>
        /// <param name="roleId">The role ID of the user.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class by copying another user.
        /// </summary>
        /// <param name="user">The user to copy.</param>
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
            return $"User: {Username}, Email: {Email}, Phone: {PhoneNumber}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A copy of the current object.</returns>
        public User Copy()
        {
            return new User(this);
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

        /// <summary>Generates salt for hashing passwords.</summary>
        /// <param name="length">Number of bytes used for length of salt</param>
        /// <returns>Salt for password</returns>
        public static byte[] GenerateSalt(int length = 16)
        {
            byte[] buffer = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buffer);
            }

            return buffer;
        }

        /// <summary>Hashes a password using the Argon2 hashing scheme.</summary>
        /// <param name="password">Plaintext password to be hashed</param>
        /// <param name="saltLength">Number of bytes used for length of salt</param>
        /// <param name="parallelism">Degree of parallelism (cores = value / 2)</param>
        /// <param name="iterations">Number of iterations</param>
        /// <param name="memorySize">Memory size in KB</param>
        /// <returns>Hashed password</returns>
        public string Argon2idHash(string password, int saltLength = 16, int parallelism = 1, int iterations = 4, int memorySize = 65536)
        {
            byte[] salt = GenerateSalt(saltLength);

            Argon2id argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = parallelism,
                Iterations = iterations,
                MemorySize = memorySize
            };

            byte[] hashed = argon2.GetBytes(32);
            string saltString = Convert.ToBase64String(salt);
            string hashedString = Convert.ToBase64String(hashed);
            return $"$argon2$sl={saltLength}$p={parallelism}$i={iterations}$m={memorySize}${saltString}${hashedString}";
        }

        /// <summary>Verifies a password with its hashed counterpart.</summary>
        /// <param name="plaintext">Plaintext password</param>
        public bool VerifyHash(string enteredPassword)
        {
            // Split the stored hash into parts
            var parts = PasswordHash.Split('$');

            // Extract the salt from the stored hash
            string saltBase64 = parts[6];
            byte[] salt = Convert.FromBase64String(saltBase64);

            // Extract the stored hash bytes
            string storedHashedBase64 = parts[7];
            byte[] storedHashBytes = Convert.FromBase64String(storedHashedBase64);

            // Extract the configuration parameters from the stored hash
            int saltLength = int.Parse(parts[1].Split('=')[1]);
            int parallelism = int.Parse(parts[2].Split('=')[1]);
            int iterations = int.Parse(parts[3].Split('=')[1]);
            int memorySize = int.Parse(parts[4].Split('=')[1]);

            // Create the Argon2id instance with the same parameters and the entered password
            Argon2id argon2 = new Argon2id(Encoding.UTF8.GetBytes(enteredPassword))
            {
                Salt = salt,
                DegreeOfParallelism = parallelism,
                Iterations = iterations,
                MemorySize = memorySize
            };

            // Calculate the hash of the entered password
            byte[] computedHash = argon2.GetBytes(storedHashBytes.Length);  // Use the same length as the stored hash

            // Compare the computed hash with the stored hash using SequenceEqual
            return computedHash.SequenceEqual(storedHashBytes);  // Returns true if the hashes are identical
        }
    }
}