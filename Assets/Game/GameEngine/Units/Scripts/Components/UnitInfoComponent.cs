using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class UnitInfoComponent : MonoBehaviour
    {
        public UnitInfo Info { get; private set; }

        [SerializeField]
        private UnitInfo info;
    }
}