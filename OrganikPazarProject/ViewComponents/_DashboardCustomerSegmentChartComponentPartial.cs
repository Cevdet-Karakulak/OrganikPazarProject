using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace OrganikPazar.ViewComponents.DashboardComponents
{
    public class _DashboardCustomerSegmentChartComponentPartial : ViewComponent
    {
        private readonly IConfiguration _config;

        public _DashboardCustomerSegmentChartComponentPartial(IConfiguration config)
        {
            _config = config;
        }

        public IViewComponentResult Invoke()
        {
            string connStr = _config.GetConnectionString("DefaultConnection");
            int gold = 0, silver = 0, bronze = 0;

            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"
                    WITH customer_stats AS (
                        SELECT 
                            c.customerid,
                            COALESCE(COUNT(o.orderid), 0) AS total_orders,
                            COALESCE(COUNT(o.orderid), 0) / 12.0 AS monthly_avg
                        FROM customer c
                        LEFT JOIN orders o 
                            ON o.customerid = c.customerid 
                           AND EXTRACT(YEAR FROM o.orderdate) = 2025
                        GROUP BY c.customerid
                    )
                    SELECT
                        SUM(CASE WHEN monthly_avg >= 5 THEN 1 ELSE 0 END) AS gold_customers,
                        SUM(CASE WHEN monthly_avg >= 2 AND monthly_avg < 5 THEN 1 ELSE 0 END) AS silver_customers,
                        SUM(CASE WHEN monthly_avg < 2 THEN 1 ELSE 0 END) AS bronze_customers
                    FROM customer_stats;
                ", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        gold = Convert.ToInt32(reader["gold_customers"]);
                        silver = Convert.ToInt32(reader["silver_customers"]);
                        bronze = Convert.ToInt32(reader["bronze_customers"]);
                    }
                }
            }

            var data = new { Gold = gold, Silver = silver, Bronze = bronze };
            return View(data);
        }
    }
}
