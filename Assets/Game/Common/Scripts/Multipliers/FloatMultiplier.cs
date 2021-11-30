using System;
using System.Collections.Generic;
using UnityEngine;

namespace Otus
{
    [Serializable]
    public sealed class FloatMultiplier : IMultiplier<float>
    {
        [SerializeField]
        private float value;

        public float GetValue()
        {
            return this.value;
        }
    }
    
    public sealed class FloatMultiplierGroup : MultiplierGroup<float>
    {
        protected override float EvaluateValue(List<IMultiplier<float>> multipliers)
        {
            var result = 1.0f;
            for (int i = 0, count = multipliers.Count; i < count; i++)
            {
                var multiplier = multipliers[i];
                result *= multiplier.GetValue();
            }

            return result;
        }
    }
}