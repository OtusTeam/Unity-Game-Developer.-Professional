using UnityEngine;

namespace Prototype.GameEngine
{
    [CreateAssetMenu(
        fileName = "UnitInfo",
        menuName = "GameEngine/Units/New UnitInfo"
    )]
    public sealed class UnitInfo : ScriptableObject
    {
        [SerializeField]
        public Sprite portrait;

        [SerializeField]
        public Sprite mapIcon;
        
        [SerializeField]
        public new string name;
    }
}