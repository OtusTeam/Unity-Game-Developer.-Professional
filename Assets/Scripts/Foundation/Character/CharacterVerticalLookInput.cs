using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class CharacterVerticalLookInput : AbstractBehaviour, IOnUpdate
    {
        public string InputActionName = "Look";

        public Transform EyesTransform;

        public float RotationSpeed;
        public float MinVerticalAngle = -50.0f;
        public float MaxVerticalAngle = 50.0f;
        
        private float angle;

        [Inject]
        IPlayer player = default;

        [Inject]
        IInputManager inputManager = default;

        [Inject]
        ISceneState sceneState = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            var input = inputManager.InputForPlayer(player.Index);
            var dir = input.Action(InputActionName).Vector2Value;

            var directionY = dir.y;
            if (!Mathf.Approximately(directionY, 0.0f))
            {
                angle += directionY * RotationSpeed * timeDelta;
                angle = Mathf.Clamp(angle, MinVerticalAngle, MaxVerticalAngle);

                Vector3 angles = EyesTransform.localEulerAngles;
                angles.x = angle;
                EyesTransform.localEulerAngles = angles;
            }
        }
    }
}