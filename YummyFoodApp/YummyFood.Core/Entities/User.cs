﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace YummyFood.Core.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            PaymentDetails = new HashSet<PaymentDetail>();
            SignInLogs = new HashSet<SignInLog>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? Enabled { get; set; }
        public bool? Deleted { get; set; }
        public long? PhoneNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Password { get; set; }
        public int? SessionId { get; set; }
        public DateTime? LoginDate { get; set; }
        public string DateOfBirth { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<SignInLog> SignInLogs { get; set; }
    }
}