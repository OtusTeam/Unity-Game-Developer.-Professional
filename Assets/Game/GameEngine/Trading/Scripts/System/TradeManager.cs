using Prototype.UI;
using UnityEngine;

namespace Prototype.GameEngine
{
    public interface ITradeManager
    {
        void SaleWood(ITradeCharacter character, ITrader trader);
        
        void SaleStone(ITradeCharacter character, ITrader trader);
    }
    
    public sealed class TradeManager : MonoBehaviour, ITradeManager
    {
        public void SaleWood(ITradeCharacter character, ITrader trader)
        {
            var myCharacter = (TradeCharacter) character;
            myCharacter.SaleWood(trader);
        }

        public void SaleStone(ITradeCharacter character, ITrader trader)
        {
            var myCharacter = (TradeCharacter) character;
            myCharacter.SaleStone(trader);
        }
    }
}