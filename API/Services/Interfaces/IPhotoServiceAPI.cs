using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IPhotoServiceApi
    {
        Task<ImageUploadResult> AddPhotoAsyncApi(IFormFile file);
        Task<DeletionResult> RemovePhotoAsyncApi(string publicId);
    }
}
