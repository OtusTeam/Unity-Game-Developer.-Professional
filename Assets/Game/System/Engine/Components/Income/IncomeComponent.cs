using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class IncomeComponent : MonoBehaviour
    {
        public int Income
        {
            get { return this.income; }
        }

        [SerializeField]
        private int income;
    }
}