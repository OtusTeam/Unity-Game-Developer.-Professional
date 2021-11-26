namespace DynamicObjects
{
    public interface IMethodDelegate
    {
        object Invoke(Args args = null);
    }
}