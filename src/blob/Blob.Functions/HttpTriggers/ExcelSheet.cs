using GoodToCode.Blob.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GoodToCode.Blob.Excel
{
    public class ExcelSheet
    {
        private IConfiguration configuration { set; get; }
        private ILogger log { get; set; }
        public const string FunctionName = "ExcelSheet";

        private readonly IFileValidationService _fileValidator;
        private readonly ExcelService _fileReader;

        public ExcelSheet(ILogger<Healthcheck> logger, IConfiguration config, IFileValidationService fileValidator,
                                                ExcelService fileService)
        {
            log = logger;
            configuration = config;
            _fileValidator = fileValidator;
            _fileReader = fileService;
        }

        [FunctionName(FunctionName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FunctionHealth))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", "put", Route = null)] HttpRequest req)
        {
            log.LogInformation($"{FunctionName} triggered.");

            var contents = _fileReader.GetFileContent(req.Body);
            if (contents != null)
            {

                
            }

            return new OkObjectResult(new { });
        }
    }
}





//namespace GoodToCode.Blob.Excel.Functions
//{
//    public class ExcelReader
//    {
//        public const string FunctionName = "blob-excel-reader";
//        private readonly IFileValidationService _fileValidator;
//        private readonly ExcelFileService _fileReader;

//        public ExcelReader(IFileValidationService fileValidator,
//                                                ExcelFileService fileService)
//        {
//            _fileValidator = fileValidator;
//            _fileReader = fileService;
//        }

//        /// <summary>
//        /// //public async Task Run([BlobTrigger("file-drop/{name}", Connection = "")] Stream excelFile, string name, Uri uri, ILogger log)
//        /// </summary>
//        /// <param name="excelFile"></param>
//        /// <param name="name"></param>
//        /// <param name="uri"></param>
//        /// <param name="log"></param>
//        /// <returns></returns>
//        [FunctionName(FunctionName)]
//        public async Task Run([BlobTrigger("file-drop/{name}", Connection = "")] Stream excelFile, string name, Uri uri, ILogger log)
//        {
//            log.LogInformation($"{FunctionName} triggered.");

//            var isValidFileExtension = _fileValidator.IsValidExtension(uri);
//            if (isValidFileExtension)
//            {
//                var contents = _fileReader.GetFileContent(excelFile);
//                if (contents != null)
//                {
//                    //await _dataService.AddAsync(contents);
//                }
//            }
//            else
//            {
                
//            }
//        }
//    }
//}
