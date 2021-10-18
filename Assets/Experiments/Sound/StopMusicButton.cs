using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class StopMusicButton : MonoBehaviour
    {
        [Inject] ISoundManager soundManager = default;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => soundManager.StopMusic());
        }
    }
}
