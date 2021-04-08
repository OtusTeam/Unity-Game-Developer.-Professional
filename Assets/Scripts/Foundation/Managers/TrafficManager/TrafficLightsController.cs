using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class TrafficLightsController : AbstractBehaviour, IOnUpdate
    {
        [Inject] ISceneState sceneState = default;

        public float SwitchTime;

        public TrafficLights[] TrafficLights;
        public int ActiveTrafficLight;

        float timeLeft;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
            UpdateActiveTrafficLights();
            timeLeft = SwitchTime;
        }

        void UpdateActiveTrafficLights()
        {
            int index = 0;
            foreach (var light in TrafficLights) {
                light.SetGreen(index == ActiveTrafficLight);
                ++index;
            }
        }

        void IOnUpdate.Do(float deltaTime)
        {
            if (timeLeft > 0.0f) {
                timeLeft -= deltaTime;
                if (timeLeft > 0.0f)
                    return;
            }

            ActiveTrafficLight = (ActiveTrafficLight + 1) % TrafficLights.Length;
            timeLeft = SwitchTime;
            UpdateActiveTrafficLights();
        }
    }
}
