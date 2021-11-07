using System;
using System.Collections.Generic;
using UnityEngine;

namespace Otus
{
    public sealed class MeleeAnimatorWeaponComponent : Weapon
    {
        public override event Action<IWeapon> OnAttack;

        [SerializeField]
        private int damage;

        [Space]
        [SerializeField]
        private bool isActive;
        
        [SerializeField]
        private ObservableCollider observableCollider;

        [Header("Animation")]
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimationEventReceiver animationEventReceiver;

        [SerializeField]
        private string animationName = "Attack";

        [SerializeField]
        private string beginAnimationEvent = "BeginAttack";

        [SerializeField]
        private string endAnimationEvent = "EndAttack";

        private HashSet<Eneny> damagedEnemies;

        public override void Attack()
        {
            if (this.isActive)
            {
                this.animator.Play(this.animationName, -1, 0);
            }
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
            this.animationEventReceiver.OnAnimationEvent += this.OnAnimationEvent;
        }

        private void OnDisable()
        {
            this.animationEventReceiver.OnAnimationEvent -= this.OnAnimationEvent;
        }

        private void OnAnimationEvent(string message)
        {
            if (message == this.beginAnimationEvent)
            {
                this.damagedEnemies.Clear();
                this.observableCollider.SetActive(true);
                this.OnAttack?.Invoke(this);
            }
            else if (message == this.endAnimationEvent)
            {
                this.observableCollider.SetActive(false);
            }
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