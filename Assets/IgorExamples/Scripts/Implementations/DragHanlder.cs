using Foundation;
using UnityEngine;

namespace IgorExamples
{
    public sealed class DragHanlder : IInputHandler
    {
        private const int LEFT_MOUSE_BUTTON = 0;

        private const float MIN_MOUSE_DISTANCE = 1.0f;

        public bool RequestTarget { get; private set; }

        private Vector3 startMousePosition;

        void IInputHandler.OnIdleUpdate()
        {
            if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
            {
                this.startMousePosition = Input.mousePosition;
                return;
            }

            if (Input.GetMouseButton(LEFT_MOUSE_BUTTON) && this.IsDrag())
            {
                this.RequestTarget = true;
            }
        }

        void IInputHandler.OnTargetUpdate()
        {
            if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
            {
                this.RequestTarget = false;
            }
            else
            {
                DebugOnly.Message($"MOUSE POSITION {Input.mousePosition}");
            }
        }
        
        private bool IsDrag()
        {
            return Vector3.Distance(Input.mousePosition, this.startMousePosition) >= MIN_MOUSE_DISTANCE;
        }
    }
}