using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Foundation;

namespace Experiments
{
    public sealed class DamagePlayerButton : MonoBehaviour, IAttacker
    {
        public int PlayerIndex;
        public float Amount;

        [Inject] IPlayerManager playerManager = default;
        IPlayer IAttacker.Player => null;

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                playerManager.GetPlayer(PlayerIndex).Health.Damage(this, Amount));
        }
    }
}
