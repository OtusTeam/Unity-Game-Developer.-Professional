using System.Collections.Generic;

namespace Prototype.GameEngine
{
    public interface IEntity
    {
        void AddEntityComponent(object component);
        
        void RemoveEntityComponent(object component);

        T GetEntityComponent<T>();

        bool TryGetEntityComponent<T>(out T component);
        
        IEnumerable<T> GetEntityComponents<T>();
    }
}