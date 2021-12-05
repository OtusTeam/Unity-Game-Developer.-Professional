using System;

namespace Prototype.UI
{
    public interface ITradeCharacter
    {
        event Action<int> OnMoneyChanged;

        event Action<int> OnWoodChanged;

        event Action<int> OnStoneChanged;
        
        int Money { get; }
        
        int Wood { get; }
        
        int Stone { get; }
    }
}