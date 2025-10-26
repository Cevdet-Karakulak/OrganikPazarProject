using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace OrganikPazar.ViewComponents.DashboardComponents
{
    public class _DashboardForecastComponentPartial : ViewComponent
    {
        private readonly IConfiguration _config;
        public _DashboardForecastComponentPartial(IConfiguration config)
        {
            _config = config;
        }

        public IViewComponentResult Invoke()
        {
            string connStr = _config.GetConnectionString("DefaultConnection");
            var forecasts = new List<dynamic>();

            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"
                    SELECT city, forecastmonth, predictedorders, modelversion, createdat
                    FROM orderforecast
                    WHERE EXTRACT(YEAR FROM forecastmonth) = 2026
                      AND city IN (
                          SELECT city
                          FROM orderforecast
                          WHERE EXTRACT(YEAR FROM forecastmonth) = 2026
                          GROUP BY city
                          ORDER BY SUM(predictedorders) DESC
                          LIMIT 3
                      )
                    ORDER BY city, forecastmonth;
                ", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        forecasts.Add(new
                        {
                            City = reader["city"].ToString(),
                            ForecastMonth = Convert.ToDateTime(reader["forecastmonth"]),
                            PredictedOrders = Convert.ToInt32(reader["predictedorders"]),
                            ModelVersion = reader["modelversion"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["createdat"])
                        });
                    }
                }
            }

            return View(forecasts);
        }
    }
}
