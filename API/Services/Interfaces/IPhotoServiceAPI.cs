using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IPhotoServiceAPI
    {
        Task<ImageUploadResult> AddPhotoAsyncAPI(IFormFile file);
        Task<DeletionResult> RemovePhotoAsyncAPI(string publicId);
    }
}
