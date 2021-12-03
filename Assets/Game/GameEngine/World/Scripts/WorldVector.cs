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
    }
}