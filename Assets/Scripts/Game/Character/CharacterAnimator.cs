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
            IdleOrRunning_NoWeapon = 1000,
            IdleOrRunning_Pistol = 1001,
            Crouching_NoWeapon = 1002,
            Crouching_Pistol = 1003,
        }

        enum LegsAnim
        {
            IdleOrRunning = 2000,
            Crouching = 2001,
        }

        [Header("Debug")]
        [HideInInspector] [ReadOnly] BodyAnim bodyAnim = BodyAnim.IdleOrRunning_NoWeapon;
        [HideInInspector] [ReadOnly] LegsAnim legsAnim = LegsAnim.IdleOrRunning;

        Animator animator;
        Vector3 prevPosition;
        int bodyAnimID;
        int legsAnimID;
        int velocityXID;
        int velocityYID;

        [Inject] ISceneState state = default;
        [InjectOptional] ICharacterWeapon characterWeapon = default;
        [InjectOptional] ICharacterCrouchInput characterCrouch = default;

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

            delta = transform.InverseTransformDirection(delta);

            var velocity = new Vector2(delta.x, delta.z).normalized;
            animator.SetFloat(velocityXID, velocity.x);
            animator.SetFloat(velocityYID, velocity.y);
        }

        (BodyAnim body, LegsAnim legs) ChooseAnimation()
        {
            Weapon weapon = Weapon.None;
            if (characterWeapon != null)
                weapon = characterWeapon.CurrentWeapon;

            if (characterCrouch != null && characterCrouch.Crouching) {
                switch (weapon) {
                    case Weapon.Pistol:
                        return (BodyAnim.Crouching_Pistol, LegsAnim.Crouching);
                    default:
                        return (BodyAnim.Crouching_NoWeapon, LegsAnim.Crouching);
                }
            }

            switch (weapon) {
                case Weapon.Pistol:
                    return (BodyAnim.IdleOrRunning_Pistol, LegsAnim.IdleOrRunning);
                default:
                    return (BodyAnim.IdleOrRunning_NoWeapon, LegsAnim.IdleOrRunning);
            }
        }
    }
}
