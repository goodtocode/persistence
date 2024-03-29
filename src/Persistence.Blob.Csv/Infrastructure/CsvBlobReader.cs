﻿using Azure.Storage.Blobs;
using CsvHelper;
using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Blob.Csv
{
    public class CsvBlobReader
    {
        public CsvBlobReader()
        {
        }

        public ISheetData ReadFile(Stream fileStream)
        {
            ISheetData sheet;
            IEnumerable<dynamic> records;

            using (var csv = new CsvReader(new StreamReader(fileStream), CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<dynamic>();
                sheet = records.ToSheetData();
            }            

            return sheet;
        }

        public ISheetData ReadFile(string file)
        {
            IEnumerable<dynamic> records;
            ISheetData sheet;
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<dynamic>();
                sheet = records.ToSheetData();
            }
            
            return sheet;
        }

        public async Task<ISheetData> ReadFileAsync(string blobConnectionString, string blobContainer, string blobFileName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainer);
            BlobClient blobClient = containerClient.GetBlobClient(blobFileName);
            ISheetData sheet = null;
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                sheet = new CsvBlobReader().ReadFile(response.Value.Content);
            }

            return sheet;
        }
    }
}




