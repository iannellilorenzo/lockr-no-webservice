using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockr_no_webservice
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a role in the system.
    /// </summary>
    public class Role
    {
        // Private attributes
        private int _id;
        private string _roleName;

        // Properties with getter and setter
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string RoleName
        {
            get => _roleName;
            set
            {
                if (Regex.IsMatch(value, @"^[A-Za-z' -]{1,15}$"))
                {
                    _roleName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid role name format.");
                }
            }
        }

        public Role()
        {
            Id = 2;
            RoleName = "user";
        }

        public Role(int id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }

        public Role(Role role)
        {
            Id = role.Id;
            RoleName = role.RoleName;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Role: {RoleName}, Id: {Id}";
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A copy of the current object.</returns>
        public Role Copy()
        {
            return new Role
            {
                Id = this.Id,
                RoleName = this.RoleName
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

            Role role = (Role)obj;
            return Id == role.Id &&
                   RoleName == role.RoleName;
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
