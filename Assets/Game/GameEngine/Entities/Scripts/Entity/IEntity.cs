using System.Collections.Generic;

namespace Prototype.GameEngine
{
    public interface IEntity
    {
        int Id { get; } //В зависимости от тз игры

        void SetupId(int id); //В зависимости от тз игры
        
        void AddEntityComponent(object component);
        
        void RemoveEntityComponent(object component);

        T GetEntityComponent<T>();

        bool TryGetEntityComponent<T>(out T component);
        
        IEnumerable<T> GetEntityComponents<T>();
    }
}