using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponsVisibilityController : MonoBehaviour
    {
        [Inject]
        private WeaponCurrentManager weaponCurrentManager;

        [Inject]
        private WeaponsPoolManager weaponPoolManager;

        [SerializeField]
        private Transform handTransform;

        [SerializeField]
        private Transform poolTransform;

        #region Lifecycle

        private void OnEnable()
        {
            this.weaponCurrentManager.OnWeaponSetuped += this.OnCurrentWeaponSetuped;
            this.weaponCurrentManager.OnWeaponChanged += this.OnCurrentWeaponChanged;
            this.weaponPoolManager.OnWeaponAdded += this.OnWeaponAdded;
        }

        private void OnDisable()
        {
            this.weaponCurrentManager.OnWeaponSetuped -= this.OnCurrentWeaponSetuped;
            this.weaponCurrentManager.OnWeaponChanged -= this.OnCurrentWeaponChanged;
            this.weaponPoolManager.OnWeaponAdded -= this.OnWeaponAdded;
        }

        #endregion

        #region Callbacks

        private void OnCurrentWeaponSetuped(MonoDynamicObject targetWeapon)
        {
            targetWeapon.InvokeMethod(ActionKey.SHOW);
            this.BindToTransform(targetWeapon, this.handTransform);
        }

        private void OnCurrentWeaponChanged(MonoDynamicObject previousWeapon, MonoDynamicObject nextWeapon)
        {
            previousWeapon.InvokeMethod(ActionKey.HIDE);
            this.BindToTransform(previousWeapon, this.poolTransform);

            nextWeapon.InvokeMethod(ActionKey.SHOW);
            this.BindToTransform(nextWeapon, this.handTransform);
        }

        private void OnWeaponAdded(Weapon weapon)
        {
            var weaponObject = weapon.DynamicObject;
            weaponObject.InvokeMethod(ActionKey.HIDE);
            this.BindToTransform(weaponObject, this.poolTransform);
        }

        #endregion

        private void BindToTransform(MonoDynamicObject weapon, Transform parent)
        {
            var weaponTransform = weapon.transform;
            weaponTransform.SetParent(parent);
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.eulerAngles = Vector3.zero;
        }
    }
}