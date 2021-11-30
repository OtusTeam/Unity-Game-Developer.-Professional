using System;
using System.Collections.Generic;

namespace Otus.GameEffects
{
    public interface IEntityEffectManager
    {
        event Action<IEffect> OnEffectAdded;
        
        event Action<IEffect> OnEffectRemoved;
        
        void AddEffect(IEffect effect);
        
        void RemoveEffect(IEffect effect);

        IEnumerable<IEffect> GetActiveEffects();
    }
}