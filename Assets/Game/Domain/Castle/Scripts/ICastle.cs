using System;
using UnityEngine;

namespace Prototype
{
    public interface ICastle
    {
        event Action<int> OnHitPointsChanged;

        event Action<int> OnDamageChanged;

        event Action<int> OnLevelChanged; 
        
        int HitPoints { get; }
        
        int Damage { get; }
        
        int Level { get; }
        
        Sprite Icon { get; }
        
        string Name { get; }
    }
}