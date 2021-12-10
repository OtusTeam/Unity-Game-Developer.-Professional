using GameElements;
using Prototype.GameEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.GameInterface
{
    public sealed class MoneyWidget : MonoBehaviour,
        IGameReadyElement,
        IGameFinishElement
    {
        [SerializeField]
        private Text moneyText;

        private MoneyStorageComponent moneyStorage;
        
        void IGameReadyElement.ReadyGame(IGameSystem system)
        {
            var player = system.GetService<PlayerService>();
            this.moneyStorage = player.Character.GetEntityComponent<MoneyStorageComponent>();
            this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
            
            this.UpdateMoney(this.moneyStorage.Money);
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
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