namespace Otus
{
    public interface IPropertyProvider
    {
        T Get<T>(PropertyId id);

        bool TryGet<T>(PropertyId id, out T property);
    }
}