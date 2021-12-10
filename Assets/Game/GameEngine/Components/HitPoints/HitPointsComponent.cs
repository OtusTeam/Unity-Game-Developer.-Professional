using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<int> OnHitPointsChanged; 
        
        public int HitPoints
        {
            get { return this.hitPoints; }
        }

        [SerializeField]
        private int hitPoints;

        public void IncrementHitPoints(int hitPoints)
        {
            this.hitPoints += hitPoints;
            this.OnHitPointsChanged?.Invoke(this.hitPoints);
        }

        public void DecrementHitPoints(int hitPoints)
        {
            this.hitPoints = Math.Max(this.hitPoints - hitPoints, 0);
            this.OnHitPointsChanged?.Invoke(this.hitPoints);
        }
    }
}