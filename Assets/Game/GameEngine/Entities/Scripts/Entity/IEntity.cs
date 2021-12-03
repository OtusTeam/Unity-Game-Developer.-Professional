using System.Collections.Generic;

namespace Prototype.GameEngine
{
    public interface IEntity
    {
        void AddComponent(object component);
        
        void RemoveComponent(object component);

        T GetComponent<T>();

        bool TryGetComponent<T>(out T component);
        
        IEnumerable<T> GetComponents<T>();
    }
}