using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class TakeCoverBehaviour : EnemyBehaviour
    {
        [Inject] IEnemy enemy = default;
        [Inject] IEnemyManager enemyManager = default;
        [Inject] ICharacterAgent agent = default;
        CoverPoint nearestCoverPoint = null;

        public override bool CheckUpdateAI(float deltaTime)
        {
            return enabled && enemy.SeenPlayer != null;
        }

        public override void ActivateAI()//
        {
            float nearestDistance = 0.0f;

            foreach (var obj in enemyManager.AllCoverPoints) {
                float sqrDistance = (obj.transform.position - enemy.Position).sqrMagnitude;
                if (nearestCoverPoint == null || sqrDistance < nearestDistance) {
                    nearestCoverPoint = obj;
                    nearestDistance = sqrDistance;
                }
            }
        }

        public override void UpdateAI(float deltaTime)
        {
            if (enemy.SeenPlayer != null) {
                var pos = nearestCoverPoint.transform.position;
                agent.NavigateTo(new Vector2(pos.x, pos.z));
            }
        }

        public override void DeactivateAI()
        {
            agent.Stop();
        }
    }
}
