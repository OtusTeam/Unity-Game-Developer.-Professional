using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class SizeComponent : MonoBehaviour
    {
        public int GetSizeX()
        {
            return this.sizeX;
        }

        public int GetSizeZ()
        {
            return this.sizeZ;
        }
        
        [SerializeField]
        private int sizeX;

        [SerializeField]
        private int sizeZ;
    }
}