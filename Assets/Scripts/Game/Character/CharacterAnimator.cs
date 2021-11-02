using Foundation;
using System.Collections.Generic;
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
            IdleOrRunning_Baseball = 1004,
            Crouching_NoWeapon = 1002,
            Crouching_Pistol = 1003,
            Crouching_Baseball = 1005,
            Attack_Baseball = 1006,
        }

        enum LegsAnim
        {
            IdleOrRunning = 2000,
            Crouching = 2001,
        }

        [Header("Debug")]
        [ReadOnly] BodyAnim bodyAnim = BodyAnim.IdleOrRunning_NoWeapon;
        [ReadOnly] LegsAnim legsAnim = LegsAnim.IdleOrRunning;

        [Header("Weapons")]
        public List<AbstractWeapon> BaseballBats;
        public List<AbstractWeapon> Pistols;

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
            AbstractWeapon weapon = null;
            bool attacking = false;
            if (characterWeapon != null) {
                weapon = characterWeapon.CurrentWeapon;
                attacking = characterWeapon.AttackingWeapon != null;
            }

            if (characterCrouch != null && characterCrouch.Crouching) {
                if (BaseballBats != null && weapon != null && BaseballBats.Contains(weapon)) {
                    if (attacking)
                        return (BodyAnim.Attack_Baseball, LegsAnim.Crouching);
                    else
                        return (BodyAnim.Crouching_Baseball, LegsAnim.Crouching);
                }

                if (Pistols != null && weapon != null && Pistols.Contains(weapon)) {
                    if (attacking)
                        characterWeapon.EndAttack(applyCooldown: true);
                    return (BodyAnim.Crouching_Pistol, LegsAnim.Crouching);
                }

                return (BodyAnim.Crouching_NoWeapon, LegsAnim.Crouching);
            }

            if (BaseballBats != null && weapon != null && BaseballBats.Contains(weapon)) {
                if (attacking)
                    return (BodyAnim.Attack_Baseball, LegsAnim.IdleOrRunning);
                else
                    return (BodyAnim.IdleOrRunning_Baseball, LegsAnim.IdleOrRunning);
            }

            if (Pistols != null && weapon != null && Pistols.Contains(weapon)) {
                if (attacking)
                    characterWeapon.EndAttack(applyCooldown: true);
                return (BodyAnim.IdleOrRunning_Pistol, LegsAnim.IdleOrRunning);
            }

            return (BodyAnim.IdleOrRunning_NoWeapon, LegsAnim.IdleOrRunning);
        }
    }
}
