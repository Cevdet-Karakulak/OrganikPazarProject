using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganikPazar.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<List<T>> GetPagedAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        IQueryable<T> GetQueryable();


    }
}
