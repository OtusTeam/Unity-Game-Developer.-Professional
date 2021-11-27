namespace DynamicObjects
{
    public interface IDynamicObject
    {
        //Properties:
        T GetProperty<T>(object key);

        bool ContainsProperty(object key);

        void AddProperty(object key, IPropertyProvider provider);

        void RemoveProperty(object key);

        //Methods:
        T InvokeMethod<T>(object key, object data = null);

        void InvokeMethod(object key, object data = null);
        
        bool ContainsMethod(object key);

        void AddMethod(object key, IMethodDelegate method);

        void RemoveMethod(object key);
        
        //Events:
        void AddEventListener(object key, IMethodDelegate callback);

        void RemoveEventListener(object key, IMethodDelegate callback);

        void InvokeEvent(object key, object data = null);
    }
}