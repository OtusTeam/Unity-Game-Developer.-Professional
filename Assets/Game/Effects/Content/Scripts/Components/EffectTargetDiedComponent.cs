using DynamicObjects;

namespace Otus.GameEffects
{
    public sealed class EffectTargetDiedComponent : EffectComponent
    {
        private readonly MethodDelegate dieListener;

        private IDynamicObject target;

        private IHandler targetHandler;

        public EffectTargetDiedComponent()
        {
            this.dieListener = new MethodDelegate(this.OnTargetDied);
        }

        public override void Activate(IDynamicObject target, IHandler handler)
        {
            this.ResetState();
            
            this.target = target;
            this.target.AddEventListener(ActionKey.DIE, this.dieListener);
            this.targetHandler = handler;
        }

        public override void Deactivate()
        {
            this.ResetState();
        }
        
        #region Callbacks

        private object OnTargetDied(object data)
        {
            this.targetHandler.Deactivate(this.target);
            return null;
        }

        #endregion
        
        private void ResetState()
        {
            this.targetHandler = null;
            if (this.target != null)
            {
                this.target.RemoveEventListener(ActionKey.DIE, this.dieListener);
                this.target = null;
            }
        }
    }
}