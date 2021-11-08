using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Otus
{
    //Дробовик
    public sealed class MeleeZoneWeaponController : WeaponComponent
    {
        public override event Action<IWeapon> OnAttack;
        
        [SerializeField]
        private int damage;
        
        [Space]
        [SerializeField]
        private bool isActive;

        [SerializeField]
        private ObservableCollider observableCollider;

        private HashSet<Eneny> damagedEnemies;

        public override void Attack()
        {
            if (this.isActive)
            {
                this.StartCoroutine(this.AttackRoutine());
            }
        }

        private IEnumerator AttackRoutine()
        {
            this.observableCollider.SetActive(true);
            this.OnAttack?.Invoke(this);
            yield return new WaitForFixedUpdate();
            this.observableCollider.SetActive(false);
            this.damagedEnemies.Clear();
        }

        public override bool CanAttack()
        {
            return this.isActive;
        }

        public override void SetActive(bool isActive)
        {
            this.isActive = isActive;
            this.observableCollider.SetActive(isActive);
        }

        private void Awake()
        {
            this.observableCollider.SetActive(false);
            this.damagedEnemies = new HashSet<Eneny>();
        }

        private void OnEnable()
        {
            this.observableCollider.OnTriggerEntered += this.OnTriggerEntered;
        }
        
        private void OnDisable()
        {
            this.observableCollider.OnTriggerEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            if (collider.TryGetComponent(out Eneny eneny) && this.damagedEnemies.Add(eneny))
            {
                eneny.TakeDamage(this.damage);
            }
        }
    }
}