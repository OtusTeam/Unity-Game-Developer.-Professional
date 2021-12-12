using GameElements;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.GameInterface
{
    public sealed class MoneyWidget : MonoBehaviour,
        IGameReadyElement,
        IGameStartElement,
        IGameFinishElement
    {
        [SerializeField]
        private Text moneyText;

        private ICharacter player;

        void IGameReadyElement.ReadyGame(IGameSystem system)
        {
            var playerService = system.GetService<IPlayerManager>();
            this.player = playerService.GetCharacter();
            this.UpdateMoney(this.player.Money);
        }

        void IGameStartElement.StartGame(IGameSystem system)
        {
            this.player.OnMoneyChanged += this.OnMoneyChanged;
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.player.OnMoneyChanged -= this.OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            this.UpdateMoney(money);
        }

        private void UpdateMoney(int money)
        {
            this.moneyText.text = money.ToString();
        }
    }
}