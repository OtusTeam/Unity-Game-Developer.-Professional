using DynamicObjects;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Otus
{
    public sealed class WeaponVisibleController : MonoBehaviour
    {
        [Inject]
        private IDynamicObject weapon;

        [SerializeField]
        private UnityEvent onShowEvent;

        [SerializeField]
        private UnityEvent onHideEvent;

        private void Awake()
        {
            this.weapon.AddMethod(ActionKey.SHOW, new MethodDelegate(this.Show));
            this.weapon.AddMethod(ActionKey.HIDE, new MethodDelegate(this.Hide));
        }

        private object Show(Args args)
        {
            this.onShowEvent?.Invoke();
            return null;
        }

        private object Hide(Args args)
        {
            this.onHideEvent?.Invoke();
            return null;
        }
    }
}