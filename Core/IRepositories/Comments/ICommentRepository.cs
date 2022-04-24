using Core.Models.Entities.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories.Comments
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
    }
}
