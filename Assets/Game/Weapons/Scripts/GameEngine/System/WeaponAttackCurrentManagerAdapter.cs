using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponAttackCurrentManagerAdapter : MonoBehaviour, IMethodDelegate
    {
        [Inject]
        private IDynamicObject entity;

        [Inject]
        private IWeaponCurrentManager weaponManager;
        
        private void Awake()
        {
            this.entity.AddMethod(ActionKey.ATTACK, this);
        }

        object IMethodDelegate.Invoke(object data)
        {
            if (this.weaponManager.TryGetWeapon(out var currentWeapon))
            {
                currentWeapon.TryInvokeMethod(ActionKey.ATTACK);
            }
            
            return null;
        }
    }
}