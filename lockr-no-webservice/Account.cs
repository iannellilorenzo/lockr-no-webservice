using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using OpenSSL.Core;
using OpenSSL.Crypto;

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
                // Regex to match the encrypted password format (Base64 string)
                if (Regex.IsMatch(value, @"^[A-Za-z0-9+/=]{24,}$"))
                {
                    _password = value;
                }
                else if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$"))
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
            Password = "VGhpcyBpcyBhIGxvbmcgc3RyaW5nIGZvciB0ZXN0aW5n"; // This is a default base64 encoded string "This is a long string for testing"
            Description = "Default description";
            UserReference = "default@example.com";
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
        /// <param name="secretKey">The secret key of the user.</param>
        public Account(int id, string username, string email, string password, string description, string userReference, string secretKey)
        {
            Id = id;
            Username = username;
            Email = email;

            if (Regex.IsMatch(password, @"^[A-Za-z0-9+/=]{24,}$"))
            {
                Password = password;
            }
            else
            {
                Password = Encrypt(password, secretKey);
            }

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

        /// <summary>
        /// Derives a 256-bit AES key from a 6-digit secretKey using PBKDF2 with a random salt.
        /// </summary>
        /// <param name="secretKey">The 6-digit secretKey.</param>
        /// <param name="salt">The generated random salt.</param>
        /// <returns>A 32-byte AES key.</returns>
        public static byte[] DeriveKeyFromPin(string secretKey, out byte[] salt)
        {
            salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            using (var deriveBytes = new Rfc2898DeriveBytes(secretKey, salt, 10000))
            {
                return deriveBytes.GetBytes(32); // 32 bytes for AES-256
            }
        }

        /// <summary>
        /// Generates a random IV for AES encryption.
        /// </summary>
        /// <returns>A random 16-byte IV.</returns>
        public static byte[] GenerateRandomIV()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        /// <summary>
        /// Encrypts a plaintext string using AES with a random IV and a key derived from a 6-digit secretKey.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <param name="secretKey">The 6-digit secret key.</param>
        /// <returns>The encrypted string, base64 encoded, with IV and salt prefixed.</returns>
        public static string Encrypt(string plainText, string secretKey)
        {
            byte[] salt;
            byte[] key = DeriveKeyFromPin(secretKey, out salt);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] iv = aes.IV;
                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(salt, 0, salt.Length); // Prefix salt to the encrypted data
                        ms.Write(iv, 0, iv.Length);    // Prefix IV to the encrypted data
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts an encrypted string using AES with IV and salt prefixed, and a key derived from a 6-digit secretKey.
        /// </summary>
        /// <param name="cipherText">The base64 encoded encrypted text.</param>
        /// <param name="secretKey">The 6-digit secret key.</param>
        /// <returns>The decrypted plaintext string.</returns>
        public static string Decrypt(string cipherText, string secretKey)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            Array.Copy(buffer, 0, salt, 0, salt.Length);
            Array.Copy(buffer, salt.Length, iv, 0, iv.Length);

            byte[] key;
            using (var deriveBytes = new Rfc2898DeriveBytes(secretKey, salt, 10000))
            {
                key = deriveBytes.GetBytes(32);
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream(buffer, salt.Length + iv.Length, buffer.Length - (salt.Length + iv.Length)))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}