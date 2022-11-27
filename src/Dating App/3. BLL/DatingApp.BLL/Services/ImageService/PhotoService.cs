using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.BLL.Services.CloudinaryService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DatingApp.BLL.Services.ImageService
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> configuration)
        {
            var account = new Account
            (
                configuration.Value.CloudName,
                configuration.Value.ApiKey,
                configuration.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                        .Height(300).Width(300)
                            .Crop("fill").Gravity("face"),
                    Folder = "dating-app"
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
