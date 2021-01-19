using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Foundation
{
    public sealed class PushStateButton : MonoBehaviour
    {
        [Inject] ISceneStateManager manager = default;
        public SceneState State;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => manager.Push(State));
        }
    }
}
