using OrganikPazar.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganikPazar.Service.Interfaces
{
    public interface IForecastService
    {
        Task<List<string>> GetCitiesAsync();
        Task<List<OrderData>> GetCityOrdersAsync(string city);
        Task TrainAllCitiesAsync();
        Task TrainAndSaveCityForecastAsync(string city);
        Task<List<dynamic>> GetForecastsByCityAsync(string city);
    }
}
