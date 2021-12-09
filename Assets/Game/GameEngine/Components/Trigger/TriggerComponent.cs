using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class TriggerComponent : MonoBehaviour
    {
        public event Action<IEntity> OnTriggerEntered;

        public event Action<IEntity> OnTriggerExited;

        [SerializeField]
        private new ObservableCollider collider;

        private void OnEnable()
        {
            this.collider.OnTriggerEntered += this.OnEntered;
            this.collider.OnTriggerExited += this.OnExited;
        }

        private void OnEntered(Collider collider)
        {
            if (collider.TryGetComponent(out IEntity entity))
            {
                this.OnTriggerEntered?.Invoke(entity);
            }
        }

        private void OnExited(Collider collider)
        {
            if (collider.TryGetComponent(out IEntity entity))
            {
                this.OnTriggerExited?.Invoke(entity);
            }
        }

        private void OnDisable()
        {
            this.collider.OnTriggerEntered += this.OnEntered;
            this.collider.OnTriggerExited += this.OnExited;
        }
    }
}