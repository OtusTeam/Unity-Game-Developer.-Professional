using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Foundation
{
    public sealed class FadeCanvasAnimator : AbstractService<ICanvasAnimator>, ICanvasAnimator
    {
        public float AppearDuration = 1.0f;
        public float DisappearDuration = 1.0f;

        public void AnimateAppear(Canvas canvas, CanvasGroup canvasGroup)
        {
            canvasGroup.DOShow(AppearDuration);
        }

        public void AnimateDisappear(Canvas canvas, CanvasGroup canvasGroup)
        {
            canvasGroup.DOHide(DisappearDuration);
        }
    }
}
