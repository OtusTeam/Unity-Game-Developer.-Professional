using System;
using System.Collections.Generic;

namespace Otus.GameEffects
{
    public interface IEffectEntityManager
    {
        event Action<IEffect> OnEffectAdded;

        event Action<IEffect> OnEffectRemoved;
        
        void AddEffect(IEffect effect);

        IEnumerable<IEffect> GetEffects();

        void RemoveEffect(IEffect effect);
    }
}