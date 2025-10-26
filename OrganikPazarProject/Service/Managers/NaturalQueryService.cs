using Microsoft.Extensions.Configuration;
using Npgsql;
using OrganikPazar.Service.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrganikPazar.Service.Managers
{
    public class NaturalQueryService : INaturalQueryService
    {
        private readonly string _connectionString;

        public NaturalQueryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> ProcessAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return null;

            query = query.ToLower();

            var pattern1 = @"(?:(?<year>\d{4})\s*(yılı|senesi|yılında))?.*?(ilk\s*3\s*ay(lık)?|tahmin|satış tahmini|öngörü).*?(?<city>adana|istanbul|ankara|izmir|bursa|antalya|konya|gaziantep|samsun|trabzon|kayseri)";
            var pattern2 = @"(?<city>adana|istanbul|ankara|izmir|bursa|antalya|konya|gaziantep|samsun|trabzon|kayseri).*?(?:(?<year>\d{4})\s*(yılı|senesi|yılında))?.*?(ilk\s*3\s*ay(lık)?|tahmin|satış tahmini|öngörü)";

            var match = Regex.Match(query, pattern1, RegexOptions.IgnoreCase);
            if (!match.Success)
                match = Regex.Match(query, pattern2, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                var city = match.Groups["city"].Value;
                var year = match.Groups["year"].Success ? match.Groups["year"].Value : "2026";
                return await GetForecastForCity(city, year);
            }

            return null;
        }

        private async Task<string> GetForecastForCity(string city, string year)
        {
            try
            {
                var sb = new StringBuilder();
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                var startDate = new DateTime(int.Parse(year), 1, 1);
                var endDate = new DateTime(int.Parse(year), 4, 1);

                using var cmd = new NpgsqlCommand(@"
            SELECT DATE_TRUNC('month', forecastmonth AT TIME ZONE 'UTC') AS forecastmonth,
                   AVG(predictedorders)::int AS predictedorders
            FROM orderforecast
            WHERE LOWER(city) = LOWER(@city)
              AND (forecastmonth AT TIME ZONE 'UTC') >= @startDate
              AND (forecastmonth AT TIME ZONE 'UTC') < @endDate
            GROUP BY DATE_TRUNC('month', forecastmonth AT TIME ZONE 'UTC')
            ORDER BY forecastmonth ASC;", conn);

                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                using var reader = await cmd.ExecuteReaderAsync();
                int count = 0;

                while (await reader.ReadAsync())
                {
                    count++;
                    var month = Convert.ToDateTime(reader["forecastmonth"])
                        .ToString("MMMM yyyy", new CultureInfo("tr-TR"));
                    var predicted = reader["predictedorders"].ToString();
                    sb.AppendLine($"📅 {month} → Tahmini Sipariş: **{predicted}**");
                }

                if (count == 0)
                    return $"📊 {city} için {year} yılına ait tahmin verisi bulunamadı.";

                return $"📈 {city} ili için {year} yılı ilk 3 aylık satış tahminleri:\n\n{sb}";
            }
            catch (Exception ex)
            {
                return $"⚠️ Sistem hatası: {ex.Message}";
            }
        }


    }

}
