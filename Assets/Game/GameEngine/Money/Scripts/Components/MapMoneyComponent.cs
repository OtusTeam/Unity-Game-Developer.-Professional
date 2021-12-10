using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MapMoneyComponent : MapItemComponent
    {
        [SerializeField]
        private MoneyResourceInfo resourceInfo;

        protected override Color ProvideColor()
        {
            return Color.white;
        }

        protected override Sprite ProvideIcon()
        {
            return this.resourceInfo.minimapIcon;
        }
    }
}