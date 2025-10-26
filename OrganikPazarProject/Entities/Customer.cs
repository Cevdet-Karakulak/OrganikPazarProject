using System;
using System.Collections.Generic;

namespace OrganikPazar.Entities;

public partial class Customer
{
    public int Customerid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public DateTime? Registerdate { get; set; }
    public decimal? CustomerBalance { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
