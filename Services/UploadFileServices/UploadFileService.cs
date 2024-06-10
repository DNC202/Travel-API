using Azure.Storage.Blobs;
using Tour_API.Interfaces;

namespace Tour_API.Services.UploadFileServices
{
    public class UploadFileService : IUploadFileService
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        public UploadFileService(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No file uploaded.";

            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;
            await blobContainerClient.CreateIfNotExistsAsync();
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);
            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteAsync();
            }
            await blobContainerClient.UploadBlobAsync(file.FileName, stream);
            return blobContainerClient.Uri.ToString() + "/" + file.FileName;
        }

        public async Task DeleteFileAsync(string blobName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerCLient = blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerCLient.GetBlobClient(blobName).DeleteAsync();
        }
    }
}
