using UnityEngine;

namespace Foundation
{
    public interface IPlayer
    {
        int Index { get; }
        ICharacterHealth Health { get; }
        Vector3 Position { get; }
    }
}
