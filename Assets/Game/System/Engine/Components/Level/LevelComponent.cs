using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class LevelComponent : MonoBehaviour
    {
        public int Level
        {
            get { return this.level; }
        }

        [SerializeField]
        private int level;
    }
}