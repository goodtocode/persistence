using System;

namespace GoodToCode.Persistence.Blob.Excel
{
    public interface IFileValidationService
    {
        bool IsValidExtension(Uri filePath);
    }
}