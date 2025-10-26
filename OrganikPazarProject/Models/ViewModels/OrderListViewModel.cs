namespace OrganikPazar.Models.ViewModels
{
    public class OrderListViewModel
    {
        public int Orderid { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Totalprice { get; set; }
        public DateTime? Orderdate { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
    }
}