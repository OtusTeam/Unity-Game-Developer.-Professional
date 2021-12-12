using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public sealed class ButtonPrice : MonoBehaviour
    {
        public event Action OnClicked;

        [SerializeField]
        private bool isEnabled;
        
        [Header("UI")]
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Sprite backgroundEnabledSprite;

        [SerializeField]
        private Sprite backgroundDisabledSprite;
        
        [SerializeField]
        private Text titleText;

        [SerializeField]
        private Text priceText;

        [SerializeField]
        private Image iconImage;

        public void SetEnable(bool isEnabled)
        {
            this.isEnabled = isEnabled;

            this.button.interactable = isEnabled;
            this.backgroundImage.sprite = isEnabled
                ? this.backgroundEnabledSprite
                : this.backgroundDisabledSprite;
        }
        
        public void SetTitle(string title)
        {
            this.titleText.text = title;
        }

        public void SetPrice(string price)
        {
            this.priceText.text = price;
        }

        public void SetIcon(Sprite icon)
        {
            this.iconImage.sprite = icon;
        }

        #region Callbacks

        private void OnEnable()
        {
            this.button.onClick.AddListener(this.OnButtonClicked);
        }

        private void OnDisable()
        {
            this.button.onClick.RemoveListener(this.OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            this.OnClicked?.Invoke();
        }

        #endregion
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            try
            {
                this.SetEnable(this.isEnabled);
            }
            catch (Exception)
            {
                // ignored
            }
        }
#endif
    }
}