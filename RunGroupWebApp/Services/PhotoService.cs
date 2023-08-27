using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroupWebApp.Helper;
using RunGroupWebApp.Interfaces;

namespace RunGroupWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var hesap = new Account (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret );
            _cloudinary = new Cloudinary (hesap);// for account creation
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var yuklemeSonucu = new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var yuklemeParametreleri = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Crop("fill").Gravity("face")
                };
                yuklemeSonucu = await _cloudinary.UploadAsync(yuklemeParametreleri);
            }
            return yuklemeSonucu;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var silmeParametreleri = new DeletionParams(publicId);
            var silmeSonucu = await _cloudinary.DestroyAsync(silmeParametreleri);
            return silmeSonucu;
        }
    }
}
