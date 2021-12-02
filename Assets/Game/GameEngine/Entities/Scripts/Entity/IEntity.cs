using System.Collections.Generic;
using GameElements;

namespace GameEngine
{
    public interface IEntity
    {
        IGameSystem CurrentGameSystem { get; }
        
        void AddComponent<T>(T component);

        void RemoveComponent<T>();

        T GetComponent<T>();

        bool TryGetComponent<T>(out T component);
        
        IEnumerable<T> GetComponents<T>();

        void AddFlag(FlagType flag);

        void RemoveFlag(FlagType flag);

        bool ContainsFlag(FlagType flag);

        void BindContext(IGameSystem gameSystem);

        void ResetContext();
    }
}