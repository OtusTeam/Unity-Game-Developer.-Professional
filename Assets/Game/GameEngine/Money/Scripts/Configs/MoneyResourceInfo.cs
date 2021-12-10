using UnityEngine;

namespace Prototype.GameEngine
{
    [CreateAssetMenu(
        fileName = "MoneyResourceInfo",
        menuName = "GameEngine/Money/New MoneyResourceInfo"
    )]
    public sealed class MoneyResourceInfo : ScriptableObject
    {
        [SerializeField]
        public Sprite icon;

        [SerializeField]
        public string title;
    }
}