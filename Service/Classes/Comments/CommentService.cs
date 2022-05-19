using Core;
using Core.IRepositories;
using Core.IServices;
using Core.IServices.Comments;
using Core.Models.Entities.Comments;

namespace Service.Classes.Comments
{
    public class CommentService : EntityService<Comment>, ICommentService, IEntityService<Comment>
    {
        public CommentService(IUnitOfWork unitOfWork, IGenericRepository<Comment> repository) : base(unitOfWork, repository)
        {
        }
    }
}
