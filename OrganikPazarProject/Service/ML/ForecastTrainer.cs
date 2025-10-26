using Microsoft.ML;
using Microsoft.ML.Data;
using OrganikPazar.Models.MLModels;
using System.Collections.Generic;
using System.Linq;

namespace OrganikPazar.Service.ML
{
    public class ForecastTrainer
    {
        private readonly MLContext _mlContext;

        public ForecastTrainer()
        {
            _mlContext = new MLContext();
        }

        public List<ForecastResult> TrainAndPredict(string city, List<OrderData> data)
        {
            var results = new List<ForecastResult>();

            if (data == null || data.Count < 5)
                return results;

            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms
                .Concatenate("Features", nameof(OrderData.Month), nameof(OrderData.Year))
                .Append(_mlContext.Regression.Trainers.FastTree(
                    labelColumnName: nameof(OrderData.OrderCount),
                    numberOfLeaves: 10,
                    numberOfTrees: 50,
                    minimumExampleCountPerLeaf: 2
                ));

            var model = pipeline.Fit(trainingData);

            var futureData = Enumerable.Range(1, 3)
                .Select(month => new OrderData { City = city, Year = 2026, Month = month })
                .ToList();

            var futureView = _mlContext.Data.LoadFromEnumerable(futureData);
            var predictions = model.Transform(futureView);
            var predictedValues = _mlContext.Data.CreateEnumerable<ForecastPrediction>(predictions, false).ToList();

            for (int i = 0; i < futureData.Count; i++)
            {
                results.Add(new ForecastResult
                {
                    City = city,
                    Month = (int)futureData[i].Month,
                    PredictedCount = (int)predictedValues[i].Score
                });
            }

            return results;
        }
    }
}
