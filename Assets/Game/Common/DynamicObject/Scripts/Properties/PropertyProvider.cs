using System;

namespace DynamicObjects
{
    public sealed class PropertyProvider : IPropertyProvider
    {
        private readonly object value;

        public PropertyProvider(object value)
        {
            this.value = value;
        }

        public object ProvideProperty()
        {
            return this.value;
        }
    }

    public sealed class PropertyDelegateProvider : IPropertyProvider
    {
        private readonly Func<object> function;

        public PropertyDelegateProvider(Func<object> function)
        {
            this.function = function;
        }

        public object ProvideProperty()
        {
            return this.function.Invoke();
        }
    }

    public sealed class PropertyDecoratorProvider : IPropertyProvider
    {
        private readonly IPropertyProvider provider;

        public PropertyDecoratorProvider(IPropertyProvider provider)
        {
            this.provider = provider;
        }
        
        public object ProvideProperty()
        {
            return this.provider.ProvideProperty();
        }
    }
}