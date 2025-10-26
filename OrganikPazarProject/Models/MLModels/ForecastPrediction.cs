using Microsoft.ML.Data;

namespace OrganikPazar.Models.MLModels
{
    public class ForecastPrediction
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
