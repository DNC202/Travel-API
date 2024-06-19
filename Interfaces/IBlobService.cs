namespace Tour_API.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(IFormFile file, int id);
        Task DeleteFileAsync(string blobName);
        Task<string> GetContainerSasToken();
    }
}
