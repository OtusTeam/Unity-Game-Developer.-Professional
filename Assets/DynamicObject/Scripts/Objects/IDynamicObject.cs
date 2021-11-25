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
        T CallMethod<T>(object key, Args args = null);

        void CallMethod(object key, Args args = null);
        
        bool ContainsMethod(object key);

        void AddMethod(object key, IDelegate method);

        void RemoveMethod(object key);
        
        //Events:
        void AddEventListener(object key, IDelegate callback);

        void RemoveEventListener(object key, IDelegate callback);

        void InvokeEvent(object key, Args args);
    }
}