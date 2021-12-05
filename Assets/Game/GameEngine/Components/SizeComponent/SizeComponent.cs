using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class SizeComponent : MonoBehaviour
    {
        public WorldVector GetSize()
        {
            return this.size;
        }
        
        [SerializeField]
        private WorldVector size;
    }
}