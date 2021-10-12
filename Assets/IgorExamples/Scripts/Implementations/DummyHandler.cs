using UnityEngine;

namespace IgorExamples
{
    public sealed class DummyHandler : IInputHandler
    {
        public bool RequestTarget { get; }

        public void OnIdleUpdate()
        {
            Debug.Log("Dummy");
        }

        public void OnTargetUpdate()
        {
        }
    }
}