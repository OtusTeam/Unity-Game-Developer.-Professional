using UnityEngine;

namespace Otus
{
    public sealed class WeaponChangeInputController : MonoBehaviour
    {
        [SerializeField]
        private WeaponConfig[] weaponConfigs;

        [Header("Inject")]
        [SerializeField]
        private WeaponManager weaponManager;
        
        private void Update()
        {
            this.ProcessInputActions();
        }

        private void ProcessInputActions()
        {
            for (int i = 0, count = this.weaponConfigs.Length; i < count; i++)
            {
                var config = this.weaponConfigs[i];
                if (Input.GetKeyDown(config.selectActionKey))
                {
                    this.weaponManager.ChangeCurrentWeapon(config.id);
                }
            }
        }
    }
}