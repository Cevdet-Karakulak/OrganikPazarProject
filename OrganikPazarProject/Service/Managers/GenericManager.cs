using OrganikPazar.DAL.Interfaces;
using OrganikPazar.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganikPazar.Services.Managers
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericDal<T> _dal;

        public GenericManager(IGenericDal<T> dal)
        {
            _dal = dal;
        }

        public Task<List<T>> GetAllAsync() => _dal.GetAllAsync();
        public Task<T> GetByIdAsync(int id) => _dal.GetByIdAsync(id);
        public Task InsertAsync(T entity) => _dal.InsertAsync(entity);
        public Task UpdateAsync(T entity) => _dal.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _dal.DeleteAsync(id);
        public Task<List<T>> GetPagedAsync(int pageNumber, int pageSize) => _dal.GetPagedAsync(pageNumber, pageSize);
        public Task<int> GetTotalCountAsync() => _dal.GetTotalCountAsync();
        public IQueryable<T> GetQueryable() => _dal.GetQueryable();

    }
}
