using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganikPazar.DAL.Interfaces
{
    public interface IGenericDal<T> where T : class
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
