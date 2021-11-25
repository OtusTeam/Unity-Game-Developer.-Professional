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
        T CallMethod<T>(object key, IMethodArgs args = null);

        void CallMethod(object key, IMethodArgs args = null);
        
        bool ContainsMethod(object key);

        void AddMethod(object key, IMethodDelegate method);

        void RemoveMethod(object key);
        
        //Events:
        void AddEventListener(object key, IMethodDelegate callback);

        void RemoveEventListener(object key, IMethodDelegate callback);

        void InvokeEvent(object key, IMethodArgs args);
    }
}