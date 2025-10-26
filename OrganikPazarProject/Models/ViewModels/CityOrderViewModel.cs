namespace OrganikPazar.Models.ViewModels
{
    public class CityOrderViewModel
    {
        public string City { get; set; }
        public int OrderCount { get; set; }
        public decimal AvgPrice { get; set; }
        public string TopCategory { get; set; }
    }
}
