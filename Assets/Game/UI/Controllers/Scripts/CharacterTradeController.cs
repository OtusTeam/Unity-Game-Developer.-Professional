using GameElements;
using Prototype.GameEngine;

namespace Prototype.UI
{
    public sealed class CharacterTradeController
    { 
        private readonly ITradeManager tradeManager;

        private ITradeCharacter currentCharacter;

        private ITrader currentTrader;

        public CharacterTradeController(IGameSystem gameSystem)
        {
            this.tradeManager = gameSystem.GetService<ITradeManager>();
        }

        public void Setup(ITradeCharacter character, ITrader trader)
        {
            this.currentCharacter = character;
            this.currentTrader = trader;
        }

        public void SaleStoneClicked()
        {
            this.tradeManager.SaleStone(this.currentCharacter, this.currentTrader);
        }

        public void SaleWoodClicked()
        {
            this.tradeManager.SaleWood(this.currentCharacter, this.currentTrader);
        }
    }
}