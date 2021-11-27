namespace DynamicObjects
{
    public sealed class MethodDelegate : IMethodDelegate
    {
        public delegate object Function(object data = null);
        
        private readonly Function function;

        public MethodDelegate(Function function)
        {
            this.function = function;
        }

        public object Invoke(object data = null)
        {
            return this.function.Invoke(data);
        }
    }
}