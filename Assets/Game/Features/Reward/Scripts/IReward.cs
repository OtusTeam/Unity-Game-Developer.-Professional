using UnityEngine;

namespace Prototype
{
    public interface IReward
    {
        public Sprite Icon { get; }

        public string Text { get; }
    }
}