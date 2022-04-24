using Core.IRepositories.Comments;
using Core.Models.Entities.Comments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Comments
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
