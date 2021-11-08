using UnityEngine;

namespace Otus
{
    public sealed class PlayerWeaponAttackController : MonoBehaviour
    {
        [Header("Inject")]
        [SerializeField]
        private WeaponManager weaponManager;
        
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                this.weaponManager.CurrentWeapon.Attack();
            }
        }
    }
}