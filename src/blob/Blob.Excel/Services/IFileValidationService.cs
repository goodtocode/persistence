using System;

namespace GoodToCode.Blob.Excel
{
    public interface IFileValidationService
    {
        bool IsValidExtension(Uri filePath);
    }
}
