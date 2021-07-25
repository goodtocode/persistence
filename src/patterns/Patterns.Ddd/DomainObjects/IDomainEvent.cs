
namespace GoodToCode.Shared.Domain
{
    public interface IDomainEvent<T>
    {
        T Item { get; }
    }
}   