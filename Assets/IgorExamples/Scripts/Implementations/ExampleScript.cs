using UnityEngine;

namespace IgorExamples
{
    public sealed class ExampleScript : MonoBehaviour
    {
        private InputManager inputManager;

        private IInputHandler locker;

        private void Awake()
        {
            this.inputManager = new InputManager();
            this.locker = new LockHandler();
        }

        private void Start()
        {
            this.inputManager.PushHandler(new DummyHandler());
            this.inputManager.PushHandler(new DragHanlder());
        }

        private void Update()
        {
            this.inputManager.Update();
        }
        
#if UNITY_EDITOR
        private void OnGUI()
        {
            if (GUILayout.Button("LOCK"))
            {
                this.inputManager.PushHandler(this.locker, isTarget: true);
            }
            else if (GUILayout.Button("UNLOCK"))
            {
                this.inputManager.PopHandler(this.locker);
            }
        }
#endif
    }
}