
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Blob.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        const string _containerName = "container-azureblob";
        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;

        }
        public async Task<string> UploadAsync(Stream stream, string blobName, bool isOverWrite)
        {
            // Obtém o cliente do container
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Obtém a referência do blob
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            // Faz o upload do arquivo
            await blobClient.UploadAsync(stream, overwrite: isOverWrite);

            // Retorna a URL do blob
            return blobClient.Uri.ToString();
        }
    }
}
