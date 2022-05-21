using System;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IEntityService<T> where T : class
    {
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task RemoveAsync(T entity);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        Task SaveChangesAsync();
        void SaveChanges();
    }
}
