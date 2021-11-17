using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class FollowPlayerBehaviour : EnemyBehaviour
    {
        [Inject] IEnemy enemy = default;
        [Inject] ICharacterAgent agent = default;

        public override bool CheckUpdateAI(float deltaTime)
        {
            return enabled && enemy.SeenPlayer != null;
        }

        public override void UpdateAI(float deltaTime)
        {
            if (enemy.SeenPlayer != null)
                agent.NavigateTo(enemy.SeenPlayer.Position);
        }

        public override void DeactivateAI()
        {
            agent.Stop();
        }
    }
}
