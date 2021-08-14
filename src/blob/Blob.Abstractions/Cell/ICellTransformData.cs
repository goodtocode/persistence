namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface ICellTransformData : ICellData
    {
        string TransformName { get; }
        string TransformValue { get; }
    }
}
