using UnityEngine;

namespace Foundation
{
    public interface IPlayer
    {
        int Index { get; }
        ICharacterHealth Health { get; }
        ICharacterAgent Agent { get; }
        Vector3 Position { get; }
    }
}
