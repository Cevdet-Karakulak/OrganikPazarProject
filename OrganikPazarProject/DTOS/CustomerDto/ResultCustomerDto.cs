namespace OrganikPazar.Dtos.CustomerDtos
{
    public class ResultCustomerDto
    {
        public int Customerid { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public DateTime? Registerdate { get; set; }
    }
}
