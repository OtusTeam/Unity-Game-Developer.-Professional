using System;
using GameElements;
using Popups;
using Prototype.UI;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class TradePoint : MonoBehaviour, IGameContextElement
    {
        public event Action<TradePoint, IEntity> OnEntityEntered;

        public int WoodPrice
        {
            get { return this.woodPrice; }
        }

        public int StonePrice
        {
            get { return this.stonePrice; }
        }

        public IGameSystem GameSystem { get; set; }

        [SerializeField]
        private int woodPrice;

        [SerializeField]
        private int stonePrice;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEntity entity))
            {
                this.OnEntityEntered?.Invoke(this, entity);
            }
        }
    }
}