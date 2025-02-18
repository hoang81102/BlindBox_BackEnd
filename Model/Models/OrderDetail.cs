﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int BlindBoxId { get; set; }

    public int? PackageId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual BlindBox BlindBox { get; set; }

    public virtual Order Order { get; set; }

    public virtual Package Package { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}