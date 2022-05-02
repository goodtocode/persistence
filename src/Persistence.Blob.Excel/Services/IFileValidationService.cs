using System;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface IFileValidationService
    {
        bool IsValidExtension(Uri filePath);
    }
}