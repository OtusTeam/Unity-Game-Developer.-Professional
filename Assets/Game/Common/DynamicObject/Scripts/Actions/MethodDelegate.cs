using System;

namespace DynamicObjects
{
    public sealed class MethodDelegate : IMethodDelegate
    {
        private readonly Func<Args, object> function;

        public MethodDelegate(Func<Args, object> function)
        {
            this.function = function;
        }

        public object Invoke(Args args = null)
        {
            return this.function.Invoke(args);
        }
    }
}