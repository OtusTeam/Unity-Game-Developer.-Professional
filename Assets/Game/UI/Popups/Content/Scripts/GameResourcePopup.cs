using System;
using Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.UI
{
    public sealed class GameResourcePopup : Popup
    {
        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private Text titleText;

        [SerializeField]
        private Text valueText;

        protected override void OnShow(IPopupArgs args)
        {
            if (!(args is GameResourcePopupArgs resourceArgs))
            {
                throw new Exception($"Expected args of type {nameof(GameResourcePopupArgs)}");
            }
            
            this.Setup(resourceArgs.GameResource);
        }
        
        private void Setup(IGameResource gameResource)
        {
            this.iconImage.sprite = gameResource.Icon;
            this.titleText.text = gameResource.Title.ToUpper();
            this.valueText.text = gameResource.Value;
        }
    }
}