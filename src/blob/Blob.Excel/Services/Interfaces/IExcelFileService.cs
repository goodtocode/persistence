using System.IO;

namespace GoodToCode.Blob.Excel
{
    public interface IExcelFileService
    {
        ExcelFileRawDataModel GetFileContent(Stream fileStream);
    }
}
