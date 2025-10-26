using System;
using System.Collections.Generic;

namespace OrganikPazar.Entities;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public string? Description { get; set; }

    public string? Imageurl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
