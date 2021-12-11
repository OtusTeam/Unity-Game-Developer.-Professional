using System;
using UnityEngine;

namespace Prototype
{
    public interface ICharacter
    {
        event Action<int> OnHitPointsChanged;

        event Action<int> OnDamageChanged;
        
        int HitPoints { get; }
        
        int Damage { get; }
        
        Sprite Icon { get; }
        
        string Name { get; }
    }
}