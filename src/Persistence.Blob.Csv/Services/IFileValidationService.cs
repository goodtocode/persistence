using System;

namespace GoodToCode.Persistence.Blob.Csv
{
    public interface IFileValidationService
    {
        bool IsValidExtension(Uri filePath);
    }
}