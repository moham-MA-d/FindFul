using API.Helpers;
using API.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace API.Services.Classes
{
    public class PhotoServiceAPI : IPhotoServiceAPI
    {
        private readonly Cloudinary _cloudinary;
        public PhotoServiceAPI(IOptions<CloudinarySetting> setting)
        {
            var account = new Account(
                    setting.Value.CloudName,
                    setting.Value.ApiKey,
                    setting.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        
        }
        public async Task<ImageUploadResult> AddPhotoAsyncAPI(IFormFile file)
        {
            ImageUploadResult uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> RemovePhotoAsyncAPI(string publicId)
        {
           var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
