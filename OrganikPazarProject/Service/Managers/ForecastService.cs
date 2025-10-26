using Npgsql;
using OrganikPazar.Models.MLModels;
using OrganikPazar.Service.Interfaces;
using OrganikPazar.Service.ML;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganikPazar.Service.Managers
{
    public class ForecastService : IForecastService
    {
        private readonly IConfiguration _config;

        public ForecastService(IConfiguration config)
        {
            _config = config;
        }

        private string ConnectionString => _config.GetConnectionString("DefaultConnection");

        public async Task<List<string>> GetCitiesAsync()
        {
            var cities = new List<string>();
            await using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();
            const string q = "SELECT DISTINCT city FROM orders WHERE city IS NOT NULL ORDER BY city;";
            await using var cmd = new NpgsqlCommand(q, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                cities.Add(reader.GetString(0));

            return cities;
        }

        public async Task<List<OrderData>> GetCityOrdersAsync(string city)
        {
            var list = new List<OrderData>();
            await using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();

            const string query = @"
                SELECT EXTRACT(YEAR FROM orderdate) AS year,
                       EXTRACT(MONTH FROM orderdate) AS month,
                       COUNT(*) AS ordercount
                FROM orders
                WHERE city = @city
                GROUP BY year, month
                ORDER BY year, month;";

            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@city", city);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new OrderData
                {
                    City = city,
                    Year = Convert.ToSingle(reader["year"]),
                    Month = Convert.ToSingle(reader["month"]),
                    OrderCount = Convert.ToSingle(reader["ordercount"])
                });
            }
            return list;
        }

        public async Task TrainAllCitiesAsync()
        {
            var cities = await GetCitiesAsync();
            foreach (var city in cities)
                await TrainAndSaveCityForecastAsync(city);
        }

        public async Task TrainAndSaveCityForecastAsync(string city)
        {
            var trainer = new ForecastTrainer();
            var data = await GetCityOrdersAsync(city);
            var results = trainer.TrainAndPredict(city, data);

            foreach (var result in results)
            {
                await SaveForecastAsync(result.City, new DateTime(2026, result.Month, 1), result.PredictedCount);
            }
        }

        private async Task SaveForecastAsync(string city, DateTime month, int predictedOrders)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();

            const string insert = @"
                INSERT INTO orderforecast (city, forecastmonth, predictedorders, modelversion, createdat)
                VALUES (@city, @month, @predicted, 'v1.0', NOW());";

            await using var cmd = new NpgsqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@predicted", predictedOrders);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<dynamic>> GetForecastsByCityAsync(string city)
        {
            var list = new List<dynamic>();
            await using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();

            const string q = @"
                SELECT city, forecastmonth, predictedorders, modelversion, createdat
                FROM orderforecast
                WHERE city = @city
                ORDER BY createdat DESC
                LIMIT 3;";

            await using var cmd = new NpgsqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@city", city);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new
                {
                    City = reader["city"].ToString(),
                    ForecastMonth = Convert.ToDateTime(reader["forecastmonth"]),
                    PredictedOrders = Convert.ToInt32(reader["predictedorders"]),
                    ModelVersion = reader["modelversion"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["createdat"])
                });
            }
            return list;
        }
    }
}
