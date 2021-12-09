using UnityEngine;

namespace Prototype
{
    public interface IGameResource
    {
        public string Title { get; }

        public Sprite Icon { get; }
        
        public string Value { get; }
    }
}