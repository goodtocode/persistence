
namespace GoodToCode.Shared.Domain
{
    public interface IDomainObject
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}