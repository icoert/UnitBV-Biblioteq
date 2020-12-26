﻿using System.Text.RegularExpressions;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }

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

        public bool HasValidPhoneNumber()
        {
            return Regex.Match(this.PhoneNumber, @"^(\+[0-9]{11})$").Success;
        }
    }
}
