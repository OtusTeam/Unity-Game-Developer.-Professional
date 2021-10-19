using Foundation;
using UnityEngine;
using Zenject;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public sealed class CharacterAnimator : AbstractBehaviour, IOnUpdate
    {
        enum BodyAnim
        {
            IdleNoWeapon = 1000,
        }

        enum LegsAnim
        {
            IdleOrRunning = 2000,
        }

        public string BodyAnimKey = "BodyAnim";
        public string LegsAnimKey = "LegsAnim";
        public string VelocityXKey = "VelocityX";
        public string VelocityYKey = "VelocityY";
        public float AnimationSpeed = 5;

        [Header("Debug")]
        [HideInInspector] [ReadOnly] BodyAnim bodyAnim = BodyAnim.IdleNoWeapon;
        [HideInInspector] [ReadOnly] LegsAnim legsAnim = LegsAnim.IdleOrRunning;

        Animator animator;
        Vector3 prevPosition;
        int bodyAnimID;
        int legsAnimID;
        int velocityXID;
        int velocityYID;
        Vector2 prevVelocity;

        [Inject] ISceneState state = default;

        void Awake()
        {
            animator = GetComponent<Animator>();
            bodyAnimID = Animator.StringToHash(BodyAnimKey);
            legsAnimID = Animator.StringToHash(LegsAnimKey);
            velocityXID = Animator.StringToHash(VelocityXKey);
            velocityYID = Animator.StringToHash(VelocityYKey);
            animator.SetInteger(bodyAnimID, (int)bodyAnim);
            animator.SetInteger(legsAnimID, (int)legsAnim);
            prevPosition = transform.position;
            prevVelocity = Vector2.zero;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(state.OnUpdate);
        }

        void IOnUpdate.Do(float deltaTime)
        {
            var (wantedBodyAnim, wantedLegsAnim) = ChooseAnimation();

            if (bodyAnim != wantedBodyAnim) {
                bodyAnim = wantedBodyAnim;
                animator.SetInteger(bodyAnimID, (int)bodyAnim);
            }

            if (legsAnim != wantedLegsAnim) {
                legsAnim = wantedLegsAnim;
                animator.SetInteger(legsAnimID, (int)legsAnim);
            }

            var position = transform.position;
            var delta = position - prevPosition;
            prevPosition = position;

            var targetVelocity = new Vector2(delta.x, delta.z).normalized;
            var velocity = Vector2.MoveTowards(prevVelocity, targetVelocity, AnimationSpeed * deltaTime);
            prevVelocity = velocity;

            animator.SetFloat(velocityXID, velocity.x);
            animator.SetFloat(velocityYID, velocity.y);
        }

        (BodyAnim body, LegsAnim legs) ChooseAnimation()
        {
            return (BodyAnim.IdleNoWeapon, LegsAnim.IdleOrRunning);
        }
    }
}
