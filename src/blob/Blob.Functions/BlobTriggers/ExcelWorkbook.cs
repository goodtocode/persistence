using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Blob.Excel.Functions
{
    public class ExcelWorkbook
    {
        public const string FunctionName = "excel-load";
        private readonly IFileValidationService _fileValidator;
        private readonly ExcelService _fileReader;

        public ExcelWorkbook(IFileValidationService fileValidator,
                                                ExcelService fileService)
        {
            _fileValidator = fileValidator;
            _fileReader = fileService;
        }

        /// <summary>
        /// //public async Task Run([BlobTrigger("file-drop/{name}", Connection = "")] Stream excelFile, string name, Uri uri, ILogger log)
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="name"></param>
        /// <param name="fileUri"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName(FunctionName)]
        public async Task Run([BlobTrigger("file-drop/{name}", Connection = "")] Stream fileStream, string name, Uri fileUri, ILogger log)
        {
            log.LogInformation($"{FunctionName} triggered.");

            var isValidFileExtension = _fileValidator.IsValidExtension(fileUri);
            if (isValidFileExtension)
            {
                var contents = _fileReader.GetWorkbook(fileStream);
                if (contents != null)
                {
                    //await _dataService.AddAsync(contents);
                }
            }
            else
            {
                
            }
        }
    }
}
