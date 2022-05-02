using System;

namespace GoodToCode.Shared.Blob.Csv
{
    public interface IFileValidationService
    {
        bool IsValidExtension(Uri filePath);
    }
}