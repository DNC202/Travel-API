using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel;
using System.Web;
using Tour_API.Interfaces;

namespace Tour_API.Services.UploadFileServices
{
    public class BlobService : IBlobService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public BlobService(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }
        public async Task<string> UploadFileAsync(IFormFile file, int id)
        {
            if (file == null || file.Length == 0)
                return "No file uploaded.";

            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            await blobContainerClient.CreateIfNotExistsAsync();
            /*if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);*/

            var blobName = $"{id}{Path.GetExtension(file.FileName)}";
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
            await blobContainerClient.UploadBlobAsync(blobName, stream);
            await blobClient.SetAccessTierAsync(AccessTier.Cool);
            return blobName;
        }

        public async Task DeleteFileAsync(string blobName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerCLient = blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerCLient.GetBlobClient(blobName).DeleteIfExistsAsync();
        }

        public async Task<string> GetContainerSasToken()
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);


            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                StartsOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddMinutes(10),
                Resource = "c"
            };
            blobSasBuilder.SetPermissions(BlobAccountSasPermissions.Read);
            var sasUri = blobContainerClient.GenerateSasUri(blobSasBuilder);
            return sasUri.ToString();
        }
    }
}
