using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerWeaponChangeController : MonoBehaviour
    {
        [Inject]
        private IGameManager gameManager;
        
        [Inject]
        private IWeaponCurrentManager weaponManager;

        [Inject]
        private IWeaponsPool weaponsPool;

        private readonly List<Weapon> processingWeapons;

        private bool isEnabled;

        #region Lifecycle

        public PlayerWeaponChangeController()
        {
            this.processingWeapons = new List<Weapon>();
        }

        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
            this.gameManager.OnFinishGame += this.OnFinishGame;
        }
        
        private void OnStartGame()
        {
            this.isEnabled = true;
        }

        private void Update()
        {
            if (this.isEnabled)
            {
                this.ProcessInputActions();
            }
        }

        private void OnFinishGame()
        {
            this.isEnabled = false;
        }

        private void OnDisable()
        {
            this.gameManager.OnStartGame -= this.OnStartGame;
            this.gameManager.OnFinishGame -= this.OnFinishGame;
        }

        #endregion


        private void ProcessInputActions()
        {
            var allWeapons = this.weaponsPool.GetAllWeapons();

            this.processingWeapons.Clear();
            this.processingWeapons.AddRange(allWeapons);
            for (int i = 0, count = this.processingWeapons.Count; i < count; i++)
            {
                var weapon = this.processingWeapons[i];
                if (Input.GetKeyDown(weapon.Config.selectActionKey))
                {
                    this.ChangeWeapon(weapon);
                }
            }
        }

        private void ChangeWeapon(Weapon weapon)
        {
            this.weaponManager.ChangeWeapon(weapon.DynamicObject);
        }
    }
}