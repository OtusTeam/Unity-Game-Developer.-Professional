using UnityEngine;

namespace Otus.GameEffects
{
    [CreateAssetMenu(
        fileName = "EffectCountdownDamageConfig",
        menuName = "Effects/New EffectCountdownDamageConfig"
    )]
    public sealed class EffectDamageCountdownConfig : ScriptableObject
    {
        [SerializeField]
        public float duration;

        [SerializeField]
        public float period;

        [SerializeField]
        public int damage;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            this.period = Mathf.Max(this.period, 0.01f);
        }
#endif
    }
}