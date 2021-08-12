using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net.Http;

namespace GoodToCode.Infrastructure.Covid19
{
    public class AzureBlob
    {
        private string _containerName;
        private string _blobName;
        private string _connectionString;
        public AzureBlob(string containerName, string blobName) : this(containerName, blobName, "DefaultEndpointsProtocol=https;AccountName=GoodToCodecovid19;AccountKey=l6hLtpU4PfEtwbc5bfCBym01IxhnY8qJDxTRL48tRsEjCR0vrPoswpIMRHpLAfhbCzxU6DhOlq3bDcuIP8sMpA==;EndpointSuffix=core.windows.net")
        { }

        public AzureBlob(string containerName, string blobName, string connectionString) { _containerName = containerName; _blobName = blobName; _connectionString = connectionString; }

        public async Task<string> GetBlobContents()
        {
            if (string.IsNullOrWhiteSpace(_containerName) || string.IsNullOrWhiteSpace(_blobName))
                throw new ArgumentException($"{nameof(_containerName)} and {nameof(_blobName)} can not be null or empty.");

            BlobContainerClient container = new BlobContainerClient(_connectionString, _containerName);
            BlobClient blob = container.GetBlobClient(_blobName);
            BlobDownloadInfo download = await blob.DownloadAsync();
            StreamReader reader = new StreamReader(download.Content);
            var returnData = reader.ReadToEnd();
            return returnData;
        }

        public async Task<bool> SaveBlob(byte[] bytes)
        {
            if (string.IsNullOrWhiteSpace(_containerName) || string.IsNullOrWhiteSpace(_blobName))
                throw new ArgumentException($"{nameof(_containerName)} and {nameof(_blobName)} can not be null or empty.");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(_blobName);
            var deleted = await blockBlob.DeleteIfExistsAsync();
            await blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

            return true;
        }
    }
}
