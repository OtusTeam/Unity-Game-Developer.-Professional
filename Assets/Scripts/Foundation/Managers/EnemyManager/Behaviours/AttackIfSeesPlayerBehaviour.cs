using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class AttackIfSeesPlayerBehaviour : EnemyBehaviour
    {
        public float Cooldown = 1.0f;

        [Inject] IEnemy enemy = default;
        float cooldownLeft;

        public override bool CheckUpdateAI(float deltaTime)
        {
            if (cooldownLeft > 0.0f) {
                cooldownLeft -= deltaTime;
                return false;
            }

            return enabled && cooldownLeft <= 0.0f;
        }

        public override void UpdateAI(float deltaTime)
        {
            if (enemy.SeenPlayer != null) {
                if (enemy.TryAttackPlayer(enemy.SeenPlayer))
                    cooldownLeft = Cooldown;
            }
        }
    }
}
