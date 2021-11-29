using UnityEngine;

namespace DynamicObjects
{
    public interface IMonoDynamicObject : IDynamicObject
    {
        GameObject GameObject { get; }
        
        Transform Transform { get; }
    }
}