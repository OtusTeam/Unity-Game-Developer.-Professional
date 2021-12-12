using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class UnitInfoComponent : MonoBehaviour
    {
        public UnitInfo Info
        {
            get { return this.info; }
        }

        public UnitType Type
        {
            get { return this.type; }
        }

        [SerializeField]
        private UnitInfo info;

        [SerializeField]
        private UnitType type;
    }
}