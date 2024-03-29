﻿using Azure.Storage.Blobs;
using NPOI.SS.UserModel;
using System.IO;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Blob.Excel
{
    public class NpoiBlobReader
    {
        public NpoiBlobReader()
        {

        }

        public IWorkbook ReadFile(Stream fileStream)
        {
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            return workbook;
        }

        public IWorkbook ReadFile(string file)
        {
            IWorkbook workbook = WorkbookFactory.Create(file);
            return workbook;
        }

        public async Task<IWorkbook> ReadFileAsync(string blobConnectionString, string blobContainer, string blobFileName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainer);
            BlobClient blobClient = containerClient.GetBlobClient(blobFileName);
            IWorkbook returnData = null;
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                returnData = new NpoiBlobReader().ReadFile(response.Value.Content);
            }

            return returnData;
        }
    }
}




