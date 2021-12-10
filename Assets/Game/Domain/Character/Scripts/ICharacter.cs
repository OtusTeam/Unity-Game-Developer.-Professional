using System;
using UnityEngine;

namespace Prototype
{
    public interface ICharacter
    {
        event Action<float> OnSpeedChanged;

        event Action<int> OnHitPointsChanged;

        event Action<int> OnDamageChanged;

        float Speed { get; }
        
        int HitPoints { get; }
        
        int Damage { get; }
        
        Sprite Icon { get; }
        
        string Name { get; }
    }
}