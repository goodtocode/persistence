using GoodToCode.Shared.Blob.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Blob.Excel
{
    public class ExcelCell
    {
        private IConfiguration configuration { set; get; }
        private ILogger log { get; set; }
        public const string FunctionName = "ExcelCell";

        private readonly IFileValidationService _fileValidator;
        private readonly ExcelService _fileReader;

        public ExcelCell(ILogger<ExcelCell> logger, IConfiguration config, IFileValidationService fileValidator, ExcelService fileService)
        {
            log = logger;
            configuration = config;
            _fileValidator = fileValidator;
            _fileReader = fileService;
        }

        [FunctionName(FunctionName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FunctionHealth))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", "put", Route = null)] HttpRequest req)
        {
            log.LogInformation($"{FunctionName} triggered.");

            ISheet contents = _fileReader.GetSheet(req.Body, 0);
            if (contents == null)
                return new BadRequestResult();

            return new OkObjectResult(contents);
        }
    }
}