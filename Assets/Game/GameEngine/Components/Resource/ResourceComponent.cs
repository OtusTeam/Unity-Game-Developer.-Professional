using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class ResourceComponent : MonoBehaviour
    {
        public event Action<int> OnWoodChanged;

        public event Action<int> OnStoneChanged;

        public int Wood
        {
            get { return this.wood; }
        }

        public int Stone
        {
            get { return this.stone; }
        }

        [SerializeField]
        private int wood;

        [SerializeField]
        private int stone;

        public void ResetWood()
        {
            this.wood = 0;
            this.OnWoodChanged?.Invoke(0);
        }

        public void ResetStone()
        {
            this.stone = 0;
            this.OnStoneChanged?.Invoke(0);
        }
    }
}