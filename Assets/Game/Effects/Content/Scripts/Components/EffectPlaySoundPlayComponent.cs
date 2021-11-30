using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectPlaySoundPlayComponent : EffectComponent
    {
        [Inject]
        private SoundManager soundManager;
        
        [SerializeField]
        private AudioClip sfx;

        public override void Activate(IDynamicObject target, IHandler handler)
        {
            this.soundManager.PlaySound(this.sfx);
        }

        public override void Deactivate()
        {
        }
    }
}