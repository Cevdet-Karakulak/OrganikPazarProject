using System;

namespace OrganikPazar.Service.Interfaces
{
    public interface IProductSuggestionService
    {
        object? GetProductSuggestion(string keyword);
    }
}
