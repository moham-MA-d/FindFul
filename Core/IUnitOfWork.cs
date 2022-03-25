using Core.IRepositories.User;
using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        Task<int> CommitAsync();

        int Commit();

        //IUserRepository Users { get; }
    }
}
