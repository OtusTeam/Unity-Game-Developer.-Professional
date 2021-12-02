using System.Collections.Generic;
using GameElements;

namespace Prototype.GameEngine
{
    public interface IEntity
    {
        IGameSystem CurrentGameSystem { get; }

        void AddComponent(object component);
        
        void RemoveComponent(object component);

        T GetComponent<T>();

        bool TryGetComponent<T>(out T component);
        
        IEnumerable<T> GetComponents<T>();

        void AddFlag(EntityFlag entityFlag);

        void RemoveFlag(EntityFlag entityFlag);

        bool ContainsFlag(EntityFlag entityFlag);

        void BindContext(IGameSystem gameSystem);

        void ResetContext();
    }
}