using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerWeaponChangeController : MonoBehaviour
    {
        [SerializeField]
        private Parameters parameters;
        
        [Inject]
        private IGameManager gameManager;
        
        [Inject]
        private WeaponCurrentManager weaponManager;

        [Inject]
        private WeaponsPoolManager weaponsPoolManager;

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
            this.SetInitialWeapon();
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
            var allWeapons = this.weaponsPoolManager.GetAllWeapons();

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

        private void SetInitialWeapon()
        {
            var weaponId = this.parameters.initialWeapon.id;
            var weapon = this.weaponsPoolManager.GetWeapon(weaponId);
            this.weaponManager.SetupWeapon(weapon.DynamicObject);
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public WeaponConfig initialWeapon;
        }
    }
}