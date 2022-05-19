using Core.IRepositories.Comments;
using Core.Models.Entities.Comments;
using Microsoft.Extensions.Logging;

namespace Data.Repositories.Comments
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
