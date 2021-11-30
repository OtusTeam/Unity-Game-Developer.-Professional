using System;
using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class EntityAttackWeaponAdapter : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [SerializeField]
        private Mode mode;

        [ShowIf("mode", Value = Mode.WEAPON)]
        [SerializeField]
        private MonoDynamicObject weapon;

        [ShowIf("mode", Value = Mode.WEAPON_MANAGER)]
        [SerializeField]
        private WeaponCurrentManager weaponManager;

        private void Awake()
        {
            if (this.mode == Mode.WEAPON)
            {
                this.entity.AddMethod(ActionKey.ATTACK, new MethodDelegate(this.AttackByWeapon));
                this.weapon.AddProperty(PropertyKey.PARENT, new PropertyProvider(this.entity));
            }
            else if (this.mode == Mode.WEAPON_MANAGER)
            {
                this.entity.AddMethod(ActionKey.ATTACK, new MethodDelegate(this.AttackByWeaponManager));
            }
        }

        private object AttackByWeapon(object data)
        {
            this.weapon.TryInvokeMethod(ActionKey.ATTACK);
            return null;
        }

        private object AttackByWeaponManager(object data)
        {
            if (this.weaponManager.TryGetWeapon(out var currentWeapon))
            {
                currentWeapon.TryInvokeMethod(ActionKey.ATTACK);
            }

            return null;
        }

        [Serializable]
        private enum Mode
        {
            WEAPON,
            WEAPON_MANAGER
        }
    }
}