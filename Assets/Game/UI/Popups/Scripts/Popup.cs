using System.Collections;
using Popups;
using UnityEngine;
using UnityEngine.Events;

namespace Prototype.UI
{
    public class Popup : MonoBehaviour, IPopup
    {
        [SerializeField]
        private UnityEvent<object> onShow;

        [SerializeField]
        private UnityEvent onHide;

        private IPopup.Handler handler;

        void IPopup.Show(IPopup.Handler handler, object data)
        {
            this.handler = handler;
            this.StartCoroutine(this.ShowInNextFrame(data));
        }

        void IPopup.Hide()
        {
            this.OnHide();
            this.onHide?.Invoke();
        }

        private IEnumerator ShowInNextFrame(object data)
        {
            //Можно отыграть после метода Start (Опционально).
            yield return new WaitForEndOfFrame();
            this.OnShow();
            this.onShow?.Invoke(data);
        }

        //Unity Event
        public void Close()
        {
            if (this.handler != null)
            {
                this.handler.Close(this);
            }
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}