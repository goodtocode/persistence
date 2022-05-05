using System;
using System.IO;

namespace GoodToCode.Persistence.Blob.Excel
{
    public class FileValidationService : IFileValidationService
    {
        public bool IsValidExtension(Uri filePath)
        {
            string fileExtension = Path.GetExtension(filePath.ToString());

            if (fileExtension.Equals($".{SupportedFileTypes.XLSX}", StringComparison.InvariantCultureIgnoreCase)
                ||
                fileExtension.Equals($".{SupportedFileTypes.XLS}", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
