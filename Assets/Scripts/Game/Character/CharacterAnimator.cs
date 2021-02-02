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

        [Header("Debug")]
        [HideInInspector] [ReadOnly] BodyAnim bodyAnim = BodyAnim.IdleNoWeapon;
        [HideInInspector] [ReadOnly] LegsAnim legsAnim = LegsAnim.IdleOrRunning;

        Animator animator;
        Vector3 prevPosition;
        int bodyAnimID;
        int legsAnimID;
        int velocityXID;
        int velocityYID;

        [Inject] ISceneState state = default;

        void Awake()
        {
            animator = GetComponent<Animator>();
            bodyAnimID = Animator.StringToHash("BodyAnim");
            legsAnimID = Animator.StringToHash("LegsAnim");
            velocityXID = Animator.StringToHash("VelocityX");
            velocityYID = Animator.StringToHash("VelocityY");
            animator.SetInteger(bodyAnimID, (int)bodyAnim);
            animator.SetInteger(legsAnimID, (int)legsAnim);
            prevPosition = transform.position;
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

            var velocity = new Vector2(delta.x, delta.z).normalized;
            animator.SetFloat(velocityXID, velocity.x);
            animator.SetFloat(velocityYID, velocity.y);
        }

        (BodyAnim body, LegsAnim legs) ChooseAnimation()
        {
            return (BodyAnim.IdleNoWeapon, LegsAnim.IdleOrRunning);
        }
    }
}
