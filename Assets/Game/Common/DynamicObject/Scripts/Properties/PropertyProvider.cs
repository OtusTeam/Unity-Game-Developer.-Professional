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
        private readonly Func<object> @delegate;

        public PropertyDelegateProvider(Func<object> @delegate)
        {
            this.@delegate = @delegate;
        }

        public object ProvideProperty()
        {
            return this.@delegate.Invoke();
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