using System.Collections;
using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectCountdownComponent : EffectCoroutineComponent
    {
        [SerializeField]
        private float duration;

        protected override IEnumerator ProcessEffect(IDynamicObject target)
        {
            yield return new WaitForSeconds(this.duration);
        }
    }
}