using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    [Serializable]
    public struct WorldVector
    {
        [SerializeField]
        public float x;
        
        [SerializeField]
        public float z;
        
        public WorldVector(float x, float z)
        {
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(z)}: {z}";
        }
    }
}