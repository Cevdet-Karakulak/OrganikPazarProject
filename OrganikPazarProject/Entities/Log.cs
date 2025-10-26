using System;

namespace OrganikPazar.Entities
{
    public partial class Log
    {
        public int Logid { get; set; }
        public string Username { get; set; } = null!;
        public string Actiontype { get; set; } = null!;
        public string Entity { get; set; } = null!;
        public string? ActionDetail { get; set; } 
        public DateTime? Actiondate { get; set; }
        public string? IpAddress { get; set; } 
    }
}
