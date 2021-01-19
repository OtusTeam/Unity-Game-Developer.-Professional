using UnityEngine;
using TMPro;
using Foundation;
using Zenject;

namespace Experiments
{
    public sealed class Counter : AbstractBehaviour, IOnUpdate
    {
        public TextMeshProUGUI text;
        float counter;

        [Inject] ISceneState state;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(state.OnUpdate);
        }

        void IOnUpdate.Do(float deltaTime)
        {
            counter += deltaTime;
            text.text = $"{counter}";
        }
    }
}
