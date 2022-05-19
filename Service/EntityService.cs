using Core;
using Core.IRepositories;
using System;
using System.Threading.Tasks;
using Core.IServices;

namespace Service
{
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;

        protected EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Update(entity);
            SaveChanges();
        }
        
        public async Task RemoveAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Remove(entity);
            await SaveChangesAsync();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
