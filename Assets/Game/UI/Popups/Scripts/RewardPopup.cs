using Prototype.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Prototype.GameInterface
{
    public sealed class RewardPopup : Popup
    {
        [FormerlySerializedAs("valueText")]
        [SerializeField]
        private Text rewardText;
        
        [SerializeField]
        private Image iconImage;

        protected override void OnShow(UIArguments args)
        {
            var reward = args.Get<IReward>(UIArgumentName.REWARD);
            this.rewardText.text = reward.Text.ToUpper();
            this.iconImage.sprite = reward.Icon;
        }
    }
}