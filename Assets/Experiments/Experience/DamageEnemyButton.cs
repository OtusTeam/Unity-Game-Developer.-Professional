using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class DamageEnemyButton : MonoBehaviour, IAttacker
    {
        public int PlayerIndex;
        public float Amount;

        [Inject] IPlayerManager playerManager = default;
        [Inject] ICharacterHealth health = default;
        IPlayer IAttacker.Player => playerManager.GetPlayer(PlayerIndex);
        IEnemy IAttacker.Enemy => null;
        AbstractCharacterEffect IAttacker.Effect => null;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => health.Damage(this, Amount));
        }
    }
}
