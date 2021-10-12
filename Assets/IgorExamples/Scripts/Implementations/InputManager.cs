using System.Collections.Generic;

namespace IgorExamples
{
    public sealed class InputManager : IInputManager
    {
        private readonly List<IInputHandler> handlers;

        private IInputHandler targetHandler;

        private bool targetMode;

        public InputManager()
        {
            this.handlers = new List<IInputHandler>();
        }
        
        public void PushHandler(IInputHandler handler, bool isTarget = false)
        {
            if (isTarget)
            {
                this.targetMode = true;
                this.targetHandler = handler;
            }
            
            if (!this.handlers.Contains(handler))
            {
                this.handlers.Add(handler);
            }
        }

        public void PopHandler(IInputHandler handler)
        {
            this.handlers.Remove(handler);
            if (this.targetHandler == handler)
            {
                this.ResetLock();
            }
        }

        public void Update()
        {
            if (this.targetMode)
            {
                this.UpdateTargetState();
            }
            else
            {
                this.UpdateIdleState();
            }
        }
        
        private void UpdateIdleState()
        {
            if (this.handlers.Count <= 0)
            {
                return;
            }
            
            for (int i = 0, count = this.handlers.Count; i < count; i++)
            {
                var handler = this.handlers[i];
                if (handler.RequestTarget)
                {
                    this.targetMode = true;
                    this.targetHandler = handler;
                    this.targetHandler.OnTargetUpdate();
                    break;
                }

                handler.OnIdleUpdate();
            }
        }

        private void UpdateTargetState()
        {
            if (this.targetHandler.RequestTarget)
            {
                this.targetHandler.OnTargetUpdate();
            }
            else
            {
                this.ResetLock();
            }
        }

        private void ResetLock()
        {
            this.targetMode = false;
            this.targetHandler = null;
        }
    }
}