using UnityEngine;

namespace Otus
{
    public sealed class Inventory : MonoBehaviour
    {
        [SerializeField]
        private int shellCount;

        public bool HasShells()
        {
            return this.shellCount > 0;
        }

        public void SpendShell()
        {
            if (this.shellCount > 0)
            {
                this.shellCount--;
            }
        }
    }
}