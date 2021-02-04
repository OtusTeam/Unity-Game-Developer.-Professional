using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class AddExperienceButton : MonoBehaviour
    {
        public int PlayerIndex;
        public int Amount;

        [Inject] IExperienceManager experienceManager = default;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => experienceManager.AddExperience(PlayerIndex, Amount));
        }
    }
}
