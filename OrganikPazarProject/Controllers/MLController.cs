using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Service.Interfaces;

namespace OrganikPazar.Controllers
{
    public class MLController : Controller
    {
        private readonly IForecastService _forecastService;

        public MLController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<IActionResult> TrainForecast()
        {
            var cities = await _forecastService.GetCitiesAsync();
            var data = new List<dynamic>();
            foreach (var city in cities)
            {
                var forecasts = await _forecastService.GetForecastsByCityAsync(city);
                data.AddRange(forecasts);
            }

            return View(data);
        }

        public async Task<IActionResult> RunTrain()
        {
            await _forecastService.TrainAllCitiesAsync();
            TempData["Success"] = "✅ ML.NET modeli tüm şehirler için eğitildi ve sonuçlar kaydedildi!";
            return RedirectToAction(nameof(TrainForecast));
        }

        [HttpGet]
        public async Task<IActionResult> CustomForecast()
        {
            ViewBag.Cities = await _forecastService.GetCitiesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RunCustomForecast(string SelectedCity)
        {
            if (string.IsNullOrEmpty(SelectedCity))
            {
                TempData["Error"] = "Lütfen bir şehir seçiniz.";
                return RedirectToAction(nameof(CustomForecast));
            }

            await _forecastService.TrainAndSaveCityForecastAsync(SelectedCity);
            var list = await _forecastService.GetForecastsByCityAsync(SelectedCity);

            if (!list.Any())
            {
                TempData["Error"] = $"{SelectedCity} için tahmin sonucu bulunamadı.";
                return RedirectToAction(nameof(CustomForecast));
            }

            TempData["Success"] = $"{SelectedCity} için tahmin başarıyla oluşturuldu!";
            ViewBag.Cities = new List<string> { SelectedCity };
            return View("CustomForecast", list);
        }
    }
}
