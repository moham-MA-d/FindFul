using DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IEntityService<T> where T : class
    {
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task RemoveAsync(T entity);
        T GetById(int Id);
        Task<T> GetByIdAsync(int Id);

        Task SaveChangesAsync();
        void SaveChanges();
    }
}
