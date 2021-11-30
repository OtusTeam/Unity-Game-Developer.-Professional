using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponAttackComponentAdapter : MonoBehaviour, IMethodDelegate
    {
        [Inject]
        private IDynamicObject entity;

        [SerializeField]
        private WeaponAttackComponent weaponComponent;

        private void Awake()
        {
            this.entity.AddMethod(ActionKey.ATTACK, this);
        }

        object IMethodDelegate.Invoke(object data)
        {
            this.weaponComponent.Attack();
            return null;
        }
    }
}