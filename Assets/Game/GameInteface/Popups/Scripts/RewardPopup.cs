using Prototype.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.GameInterface
{
    public sealed class RewardPopup : Popup
    {
        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private Text valueText;

        protected override void OnShow(UIArguments args)
        {
            var gameResource = args.Get<IReward>(UIArgumentName.REWARD);
            this.Setup(gameResource);
        }
        
        private void Setup(IReward reward)
        {
            this.iconImage.sprite = reward.Icon;
            this.valueText.text = reward.Text.ToUpper();
        }
    }
}