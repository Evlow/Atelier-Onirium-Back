using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.Business.Services
{
    public class ImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IOptions<CloudinarySettings> config, ILogger<ImageService> logger)
        {
            if (config == null || string.IsNullOrEmpty(config.Value.CloudName))
            {
                throw new ArgumentException("Cloudinary configuration is invalid or missing.");
            }

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Cloudinary Configuration: CloudName={CloudName}, ApiKey={ApiKey}",
                config.Value.CloudName, config.Value.ApiKey);

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        // Méthode pour ajouter des images
        public async Task<List<ImageUploadResult>> AddImagesAsync(List<IFormFile> files)
        {
            var uploadResults = new List<ImageUploadResult>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    uploadResults.Add(uploadResult);
                }
            }

            return uploadResults;
        }

        // Méthode pour ajouter une image principale
        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    // Optionnel : vous pouvez définir un tag ou une transformation spécifique ici pour l'image principale.
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult;
            }

            return null;
        }

        // Méthode pour supprimer des images en utilisant les publicIds
        public async Task<List<DeletionResult>> DeleteImagesAsync(List<string> publicIds)
        {
            var deletionResults = new List<DeletionResult>();

            foreach (var publicId in publicIds)
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);
                deletionResults.Add(result);
            }

            return deletionResults;
        }

        // Supprimer une image principale
        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }

        // Supprimer une image de la liste des images supplémentaires
        public async Task<DeletionResult> DeleteAdditionalImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
