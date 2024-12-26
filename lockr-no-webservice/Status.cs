using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace lockr_no_webservice
{
    /// <summary>
    /// Represents a status in the system.
    /// </summary>
    public class Status
    {
        // Private attributes
        private int _id;
        private string _statusName;

        // Properties with getter and setter
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string StatusName
        {
            get => _statusName;
            set
            {
                if (Regex.IsMatch(value, @"^[A-Za-z' -]{1,15}$"))
                {
                    _statusName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid status name format.");
                }
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Status: {StatusName}, Id: {Id}";
        }

        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A copy of the current object.</returns>
        public Status Copy()
        {
            return new Status
            {
                Id = this.Id,
                StatusName = this.StatusName
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

            Status status = (Status)obj;
            return Id == status.Id &&
                   StatusName == status.StatusName;
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
