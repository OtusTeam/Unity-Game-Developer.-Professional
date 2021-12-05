using System;
using GameElements;
using Popups;
using Prototype.UI;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class TradeEnterController : MonoBehaviour, IGameContextElement
    {
        public IGameSystem GameSystem { get; set; }

        [SerializeField]
        private TradePoint[] tradingPoints;

        private void OnEnable()
        {
            foreach (var point in this.tradingPoints)
            {
                point.OnEntityEntered += this.OnPointEntered;
            }
        }

        private void OnDisable()
        {
            foreach (var point in this.tradingPoints)
            {
                point.OnEntityEntered -= this.OnPointEntered;
            }
        }

        private void OnPointEntered(TradePoint point, IEntity entity)
        {
            if (!this.IsCharacter(entity, out ITradeCharacter character))
            {
                return;
            }
            
            var trader = new Trader(point);
            
            var popupManager = this.GameSystem.GetService<IPopupManager>();
            var popupArgs = new TradingPopup.Args(character, trader);
            popupManager.ShowPopup<TradingPopup>(popupArgs);
        }

        private bool IsCharacter(IEntity entity, out ITradeCharacter tradeCharacter)
        {
            tradeCharacter = null;
            if (entity.TryGetComponent(out MoneyComponent moneyComponent))
            {
                return false;
            }

            if (entity.TryGetComponent(out ResourceComponent resourceComponent))
            {
                return false;
            }

            tradeCharacter = new TradeCharacter(moneyComponent, resourceComponent);
            return true;
        }
    }
}