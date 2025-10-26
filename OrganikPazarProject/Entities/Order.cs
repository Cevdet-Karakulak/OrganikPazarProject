using System;
using System.Collections.Generic;

namespace OrganikPazar.Entities;

public partial class Order
{
    public int Orderid { get; set; }

    public int? Customerid { get; set; }

    public int? Productid { get; set; }

    public int Quantity { get; set; }

    public decimal Totalprice { get; set; }

    public DateTime? Orderdate { get; set; }

    public string? City { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
