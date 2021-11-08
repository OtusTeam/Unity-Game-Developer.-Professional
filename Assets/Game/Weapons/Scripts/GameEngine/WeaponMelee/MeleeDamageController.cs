using UnityEngine;

namespace Otus
{
    public sealed class MeleeDamageController : MonoBehaviour
    {
        [SerializeField]
        private int damage;
        
        [SerializeField]
        private ObservableCollider observableCollider;
        
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
            if (collider.TryGetComponent(out DamageComponent eneny))
            {
                eneny.TakeDamage(this.damage);
            }
        }
    }
}