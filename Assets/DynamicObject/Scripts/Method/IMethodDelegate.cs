namespace DynamicObjects
{
    public interface IMethodDelegate
    {
        object Invoke(IMethodArgs args = null);
    }
}