// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="User.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.DomainModel
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>The firstname.</value>
        [Required]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>The lastname.</value>
        [Required]
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        public UserType UserType { get; set; }

        /// <summary>
        /// Determines whether [has valid email].
        /// </summary>
        /// <returns><c>true</c> if [has valid email]; otherwise, <c>false</c>.</returns>
        public bool HasValidEmail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(this.Email);
                return addr.Address == this.Email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has valid phone number].
        /// </summary>
        /// <returns><c>true</c> if [has valid phone number]; otherwise, <c>false</c>.</returns>
        public bool HasValidPhoneNumber()
        {
            return Regex.Match(this.PhoneNumber, @"^(\+[0-9]{10})$").Success;
        }

        /// <summary>
        /// Determines whether [has valid address].
        /// </summary>
        /// <returns><c>true</c> if [has valid address]; otherwise, <c>false</c>.</returns>
        public bool HasValidAddress()
        {
            return Regex.Match(this.Address, @"\s*\S+(?:\s+\S+){3}").Success; // Street OneStreet, Number 999
        }
    }
}
