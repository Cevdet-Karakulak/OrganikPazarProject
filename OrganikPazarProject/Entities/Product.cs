using System;
using System.Collections.Generic;

namespace OrganikPazar.Entities;

public partial class Product
{
    public int Productid { get; set; }

    public int? Categoryid { get; set; }

    public string Productname { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Unitprice { get; set; }

    public int? Stock { get; set; }

    public string? Imageurl { get; set; }

    public bool? Isfeatured { get; set; }

    public decimal? Rating { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
