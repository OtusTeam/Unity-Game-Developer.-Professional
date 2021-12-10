using System.Collections;
using Prototype;
using UnityEngine;
using UnityEngine.Events;

namespace Prototype.Unity
{
    public class Popup : MonoBehaviour, IPopup
    {
        [SerializeField]
        private UnityEvent<UIArguments> onShow;

        [SerializeField]
        private UnityEvent onHide;

        private IPopup.Handler handler;

        void IPopup.Show(IPopup.Handler handler, UIArguments args)
        {
            this.handler = handler;
            this.StartCoroutine(this.ShowInNextFrame(args));
        }

        void IPopup.Hide()
        {
            this.OnHide();
            this.onHide?.Invoke();
        }

        private IEnumerator ShowInNextFrame(UIArguments arguments)
        {
            //Можно отыграть после метода Start (Опционально).
            yield return new WaitForEndOfFrame();
            this.OnShow(arguments);
            this.onShow?.Invoke(arguments);
        }

        //Unity Event
        public void Close()
        {
            if (this.handler != null)
            {
                this.handler.Close(this);
            }
        }

        protected virtual void OnShow(UIArguments args)
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}