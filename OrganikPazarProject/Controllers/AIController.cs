using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrganikPazar.Hubs;
using OrganikPazar.Service.Interfaces;
using System.Text.RegularExpressions;

namespace OrganikPazar.Controllers
{
    public class AIController : Controller
    {
        private readonly IHubContext<AIRecipeHub> _hubContext;
        private readonly IAIService _aiService;
        private readonly IProductSuggestionService _productSuggestion;

        public AIController(
            IHubContext<AIRecipeHub> hubContext,
            IAIService aiService,
            IProductSuggestionService productSuggestion)
        {
            _hubContext = hubContext;
            _aiService = aiService;
            _productSuggestion = productSuggestion;
        }

        [HttpGet]
        public IActionResult GetRecipe() => View();

        [HttpPost]
        public async Task<IActionResult> GetRecipe(string ingredients)
        {
            if (string.IsNullOrWhiteSpace(ingredients))
                return BadRequest("Malzeme listesi boş olamaz.");

            var recipeText = await _aiService.GetGeminiResponseAsync(ingredients);

            var materialMatches = Regex.Matches(recipeText, @"(?<=Malzemeler)(.*?)(?=\n\n|Hazırlanışı|Yapılışı|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            List<string> extractedWords = new();

            if (materialMatches.Count > 0)
            {
                var materialsBlock = materialMatches[0].Value;
                extractedWords = materialsBlock
                    .ToLower()
                    .Split(new[] { '\n', ',', '.', '(', ')', ':', '-', '*', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => w.Length > 2 && w.Length < 20)
                    .Distinct()
                    .ToList();
            }

            if (!extractedWords.Any())
            {
                extractedWords = ingredients
                    .ToLower()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 2)
                    .ToList();
            }

            var suggestionsHtml = @"
<div class='ai-products-section'>
    <h5>🌿 Organik Pazar’dan Ürün Önerileri</h5>
    <table class='table table-hover ai-product-table'>
        <thead>
            <tr>
                <th>Ürün</th>
                <th>Fiyat</th>
                <th class='text-end'>İşlemler</th>
            </tr>
        </thead>
        <tbody>";

            bool hasSuggestion = false;

            foreach (var word in extractedWords)
            {
                var product = _productSuggestion.GetProductSuggestion(word);
                if (product != null)
                {
                    hasSuggestion = true;
                    dynamic p = product;

                    suggestionsHtml += $@"
<tr>
    <td>
        <div class='d-flex align-items-center'>
            <img src='{p.Imageurl}' alt='{p.Productname}' class='product-thumb me-2' />
            <strong>{p.Productname}</strong>
        </div>
    </td>
    <td><span class='price-text'>{p.Unitprice:N2} ₺</span></td>
    <td class='text-end'>
        <a href='/Product/Detail/{p.Productid}' class='btn btn-outline-success btn-sm me-2'>🔍 Detaya Git</a>
        <a href='/Order/Create?productId={p.Productid}' class='btn btn-success btn-sm'>🛒 Sepete Ekle</a>
    </td>
</tr>";
                }
            }

            suggestionsHtml += "</tbody></table></div>";

            if (hasSuggestion)
                recipeText += $"\n\n{suggestionsHtml}";

            await _hubContext.Clients.All.SendAsync("ReceiveRecipe", recipeText);

            return Ok(new { status = "ok" });
        }
    }
}
