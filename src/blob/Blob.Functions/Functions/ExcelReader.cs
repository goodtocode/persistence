using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GoodToCode.Blob.Excel.Functions
{
    public class ExcelReader
    {
        public const string FunctionName = "excel-read";
        private readonly IFileValidationService _fileValidator;
        private readonly ExcelFileService _fileReader;

        public ExcelReader(IFileValidationService fileValidator,
                                                ExcelFileService fileService)
        {
            _fileValidator = fileValidator;
            _fileReader = fileService;
        }

        /// <summary>
        /// //public async Task Run([BlobTrigger("file-drop/{name}", Connection = "")] Stream excelFile, string name, Uri uri, ILogger log)
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName(FunctionName)]
        public async Task Run([BlobTrigger("file-drop/{name}", Connection = "")] Stream excelFile, string name, Uri uri, ILogger log)
        {
            log.LogInformation($"{FunctionName} triggered.");

            var isValidFileExtension = _fileValidator.IsValidExtension(uri);
            if (isValidFileExtension)
            {
                var contents = _fileReader.GetFileContent(excelFile);
                if (contents != null)
                {
                    await _dataService.AddAsync(contents);
                }
            }
            else
            {
                
            }
        }
    }
}
