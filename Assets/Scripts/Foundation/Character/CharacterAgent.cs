using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Foundation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class CharacterAgent : AbstractService<ICharacterAgent>, ICharacterAgent, IOnUpdate
    {
        NavMeshAgent agent;
        [Inject] ISceneState state = default;

        public Transform CharacterTransform;
        public bool UpdatePosition;
        public bool UpdateRotation;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void Move(Vector2 dir)
        {
            agent.Move(new Vector3(dir.x, 0.0f, dir.y));
        }

        public void Stop()
        {
            agent.isStopped = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(state.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (UpdatePosition) {
                CharacterTransform.position = transform.position;
                transform.localPosition = Vector3.zero;
            }

            if (UpdateRotation) {
                CharacterTransform.rotation = transform.rotation;
                transform.localRotation = Quaternion.identity;
            }
        }
    }
}
