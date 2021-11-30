using UnityEngine;

namespace Otus.GameEffects
{
    [CreateAssetMenu(
        fileName = "EffectAsset",
        menuName = "Effects/New EffectAsset"
    )]
    public sealed class EffectAsset : ScriptableObject
    {
        [SerializeField]
        public Effect prefab;
    }
}