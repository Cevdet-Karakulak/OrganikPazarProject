namespace OrganikPazar.Models.MLModels
{
    public class ForecastResult
    {
        public string City { get; set; } = string.Empty;
        public int Month { get; set; }
        public int PredictedCount { get; set; }
    }
}
