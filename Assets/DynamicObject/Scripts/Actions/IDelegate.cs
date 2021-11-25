namespace DynamicObjects
{
    public interface IDelegate
    {
        object Invoke(Args args = null);
    }
}