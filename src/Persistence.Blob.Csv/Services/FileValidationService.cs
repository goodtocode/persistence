using System;
using System.IO;

namespace GoodToCode.Persistence.Blob.Csv
{
    public class FileValidationService : IFileValidationService
    {
        public bool IsValidExtension(Uri filePath)
        {
            string fileExtension = Path.GetExtension(filePath.ToString());

            if (fileExtension.Equals($".{SupportedFileTypes.CSV}", StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }
    }
}
