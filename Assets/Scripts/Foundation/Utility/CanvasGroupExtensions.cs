using UnityEngine;
using DG.Tweening;

namespace Foundation
{
    public static class CanvasGroupExtensions
    {
        public static Tweener DOAlpha(this CanvasGroup canvasGroup, float endValue, float time)
        {
            return DOTween.To(
                    () => canvasGroup.alpha,
                    (value) => canvasGroup.alpha = value,
                    endValue,
                    time)
                .SetOptions(false)
                .SetTarget(canvasGroup);
        }

        public static Tweener DOShow(this CanvasGroup canvasGroup, float time)
        {
            return canvasGroup.DOAlpha(1.0f, time);
        }

        public static Tweener DOHide(this CanvasGroup canvasGroup, float time)
        {
            return canvasGroup.DOAlpha(0.0f, time);
        }
    }
}
