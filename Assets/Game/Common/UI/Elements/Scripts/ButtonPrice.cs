using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public sealed class ButtonPrice : MonoBehaviour
    {
        public event Action OnClicked;
        
        [SerializeField]
        private State state;
        
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

        public void SetState(State state)
        {
            this.state = state;
            if (state == State.ENABLE)
            {
                this.button.interactable = true;
                this.backgroundImage.sprite = this.backgroundEnabledSprite;
            }
            else
            {
                this.button.interactable = false;
                this.backgroundImage.sprite = this.backgroundDisabledSprite;
            }
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
        
        public enum State
        {
            ENABLE,
            DISABLE
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            try
            {
                this.SetState(this.state);
            }
            catch (Exception)
            {
                // ignored
            }
        }
#endif
    }
}