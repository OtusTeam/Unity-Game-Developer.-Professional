using UnityEngine;

namespace IgorExamples
{
    public sealed class LockHandler : IInputHandler
    {
        public bool RequestTarget { get; }

        public LockHandler()
        {
            this.RequestTarget = true;
        }

        public void OnIdleUpdate()
        {
        }

        public void OnTargetUpdate()
        {
            Debug.Log("Lock");
        }
    }
}