using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    [RequireComponent(typeof(Collider))]
    public sealed class ObservableCollider : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;

        public event Action<Collider> OnTriggerExited;
        
        private void OnTriggerEnter(Collider other)
        {
            this.OnTriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.OnTriggerExited?.Invoke(other);
        }
    }
}