using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Wardrobe.Application.Image.Upload;

public class UploadImageService : IUploadImageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public UploadImageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> Upload(Stream file, string fileName, string? containerName = default)
    {
        var mainStorage = _blobServiceClient.GetBlobContainerClient(containerName ?? "wardrobe-app");
        await mainStorage.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

        var blobClient = mainStorage.GetBlobClient(fileName);
        await blobClient.UploadAsync(file, overwrite: true);
        
        return $"{_blobServiceClient.Uri.AbsoluteUri}/{containerName ?? "wardrobe-app"}/{fileName}";
    }
}

public interface IUploadImageService
{
    Task<string> Upload(Stream file, string fileName, string? containerName = default);
}