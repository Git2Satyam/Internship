﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace YummyFood.Core.Entities
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public Guid CartId { get; set; }
        public int ItemId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public bool? Enabled { get; set; }

        public virtual Cart Cart { get; set; }
    }
}