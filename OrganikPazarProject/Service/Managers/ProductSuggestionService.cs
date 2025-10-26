using OrganikPazar.Context;
using OrganikPazar.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace OrganikPazar.Service.Managers
{
    public class ProductSuggestionService : IProductSuggestionService
    {
        private readonly OrganikPazarContext _context;

        public ProductSuggestionService(OrganikPazarContext context)
        {
            _context = context;
        }

        public object? GetProductSuggestion(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return null;

            string normalizedKeyword = Normalize(keyword);

            var productEntity = _context.Products
                .Include(p => p.Category)
                .AsEnumerable()
                .FirstOrDefault(prod =>
                {
                    var normalizedName = Normalize(prod.Productname);
                    return normalizedName.Contains(normalizedKeyword)
                        || normalizedKeyword.Contains(normalizedName)
                        || normalizedName.StartsWith(normalizedKeyword)
                        || normalizedName.EndsWith(normalizedKeyword);
                });

            if (productEntity == null)
                return null;

            return new
            {
                productEntity.Productid,
                productEntity.Productname,
                Categoryname = productEntity.Category?.Categoryname ?? "Kategori Yok",
                productEntity.Unitprice,
                productEntity.Imageurl
            };
        }

        private string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            string text = input.ToLower(new CultureInfo("tr-TR"));
            text = text
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");

            return text.Trim();
        }
    }
}
