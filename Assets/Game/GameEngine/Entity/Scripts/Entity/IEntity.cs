using System.Collections.Generic;
using GameElements;

namespace GameEngine
{
    public interface IEntity
    {
        void AddComponent<T>(T component);

        void RemoveComponent<T>();

        T GetComponent<T>();

        bool TryGetComponent<T>(out T component);
        
        IEnumerable<T> GetComponents<T>();

        void AddTag(TagType tag);

        void RemoveTag(TagType tag);

        bool ContainsTag(TagType tag);

        IGameSystem GetContext();

        void BindContext(IGameSystem gameSystem);

        void ResetContext();
    }
}