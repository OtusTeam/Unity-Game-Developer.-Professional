using Zenject;

namespace Foundation
{
    public abstract class AbstractService<T> : MonoInstaller
        where T : class
    {
        ObserverHandleManager observers;
        protected ObserverHandleManager Observers { get {
                if (observers == null)
                    observers = new ObserverHandleManager();
                return observers;
            } }

        protected void Observe<O>(IObserverList<O> observable)
            where O : class
        {
            Observers.Observe(observable, this as O);
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            if (observers != null)
                observers.Clear();
        }

        public override void InstallBindings()
        {
            Container.Bind<T>().FromInstance(this as T);
        }
    }
}
