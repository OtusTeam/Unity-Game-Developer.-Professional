using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoneyResourceComponent : MonoBehaviour
    {
        public int Money
        {
            get { return this.money; }
        }
        
        [SerializeField]
        private int money;

        [SerializeField]
        private GameObject root;

        public void Collect(out int money)
        {
            money = this.money;
            this.root.SetActive(false);
        }
    }
}