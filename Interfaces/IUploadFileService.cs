namespace Tour_API.Interfaces
{
    public interface IUploadFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string blobName);
        Task<string> GetBlobUriWithSasToken(string blobsName);
    }
}
