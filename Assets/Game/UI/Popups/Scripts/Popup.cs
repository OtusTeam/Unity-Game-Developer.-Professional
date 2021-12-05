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
            this.StartCoroutine(this.CloseNextFrame());
        }

        private IEnumerator ShowNextFrame(object data)
        {
            //Нужно отыграть после метода Start (Опционально).
            yield return new WaitForEndOfFrame();
            
            this.OnShow(data);
        }

        private IEnumerator CloseNextFrame()
        {
            //Нужно доиграть кадр (Опционалльно).
            yield return new WaitForEndOfFrame();
            
            if (this.handler != null)
            {
                this.handler.Close(this.GetType());
            }
        }
    }
}