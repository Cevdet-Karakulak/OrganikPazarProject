using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace OrganikPazar.ViewComponents.DashboardComponents
{
    public class _DashboardForecastChartComponentPartial : ViewComponent
    {
        private readonly IConfiguration _config;
        public _DashboardForecastChartComponentPartial(IConfiguration config)
        {
            _config = config;
        }

        public IViewComponentResult Invoke()
        {
            string connStr = _config.GetConnectionString("DefaultConnection");
            var data = new List<dynamic>();

            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"
                    SELECT city, forecastmonth, predictedorders
                    FROM orderforecast
                    WHERE EXTRACT(YEAR FROM forecastmonth) = 2026
                      AND city IN (
                          SELECT city
                          FROM orderforecast
                          WHERE EXTRACT(YEAR FROM forecastmonth) = 2026
                          GROUP BY city
                          ORDER BY SUM(predictedorders) DESC
                          LIMIT 5
                      )
                    ORDER BY city, forecastmonth;
                ", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(new
                        {
                            City = reader["city"].ToString(),
                            Month = ((DateTime)reader["forecastmonth"]).ToString("MMMM"),
                            PredictedOrders = Convert.ToInt32(reader["predictedorders"])
                        });
                    }
                }
            }

            return View(data);
        }
    }
}
