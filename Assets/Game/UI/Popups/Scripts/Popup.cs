using System.Collections;
using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public class Popup : MonoBehaviour, IPopup
    {
        private IPopup.Handler handler;
        
        void IPopup.Show(IPopup.Handler handler, object data)
        {
            this.handler = handler;
            this.StartCoroutine(this.ShowNextFrame(data));
        }

        protected virtual void OnShow(object data)
        {
        }

        void IPopup.Hide()
        {
            this.OnHide();
        }

        protected virtual void OnHide()
        {
        }

        protected void Close()
        {
            if (this.handler != null)
            {
                this.handler.Close(this);
            }
        }

        private IEnumerator ShowNextFrame(object data)
        {
            //Можно отыграть после метода Start (Опционально).
            yield return new WaitForEndOfFrame();
            
            this.OnShow(data);
        }
    }
}