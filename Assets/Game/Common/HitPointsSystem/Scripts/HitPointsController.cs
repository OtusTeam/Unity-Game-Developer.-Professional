using DynamicObjects;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Otus
{
    public sealed class HitPointsController : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [SerializeField]
        private int hitPoints;
        
        [Header("Die")]
        [SerializeField]
        private UnityEvent onDied;
        
        private void Awake()
        {
            this.entity.AddMethod(ActionKey.TAKE_DAMAGE, new MethodDelegate(this.OnTakeDamage));
        }

        private object OnTakeDamage(object data)
        {
            this.hitPoints -= (int) data;
            
            if (this.hitPoints <= 0)
            {
                this.onDied?.Invoke();
                this.entity.InvokeEvent(ActionKey.DIE);
            }
            
            return null;
        }
    }
}