using System;
using UnityEngine;

namespace Otus
{
    [RequireComponent(typeof(Collider))]
    public sealed class ObservableCollider : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;
        
        public event Action<Collider> OnTriggerExited;

        [SerializeField]
        private new Collider collider;

        private void OnTriggerEnter(Collider other)
        {
            this.OnTriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.OnTriggerExited?.Invoke(other);
        }

        public void SetActive(bool isActive)
        {
            this.collider.enabled = isActive;
        }

        private void Reset()
        {
            this.collider = this.GetComponent<Collider>();
        }
    }
}