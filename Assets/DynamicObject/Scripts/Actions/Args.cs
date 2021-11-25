using System.Collections.Generic;

namespace DynamicObjects
{
    public sealed class Args
    {
        private readonly Dictionary<object, object> parameterMap;

        public Args(params KeyValuePair<object, object>[] parameters)
        {
            this.parameterMap = new Dictionary<object, object>();
        }

        public T GetParameter<T>(object key)
        {
            return (T) this.parameterMap[key];
        }
    }
}