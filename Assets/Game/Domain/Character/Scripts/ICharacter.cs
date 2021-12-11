using System;
using UnityEngine;

namespace Prototype
{
    public interface ICharacter
    {
        event Action<int> OnHitPointsChanged;

        event Action<int> OnDamageChanged;

        event Action<int> OnMoneyChanged; 
        
        int HitPoints { get; }
        
        int Damage { get; }
        
        int Money { get; }
        
        Sprite Icon { get; }
        
        string Name { get; }
    }
}