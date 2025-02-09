﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class BlindBox
{
    public int BlindBoxId { get; set; }

    public int PackageId { get; set; }

    public string BlindBoxName { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public double? Percent { get; set; }

    public string BlindBoxStatus { get; set; }

    public virtual ICollection<BlindBoxImage> BlindBoxImages { get; set; } = new List<BlindBoxImage>();

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Package Package { get; set; }
}