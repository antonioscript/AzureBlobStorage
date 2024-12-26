namespace Blob.Services
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream stream, string blobName, bool isOverWrite);

    }
}
