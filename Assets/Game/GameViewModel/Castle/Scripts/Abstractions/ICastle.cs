using UnityEngine;

namespace Prototype.ViewModel
{
    public interface ICastle
    {
        Sprite Icon { get; }
        
        string Name { get; }

        int Level { get; }
        
        int Income { get; }
    }
}