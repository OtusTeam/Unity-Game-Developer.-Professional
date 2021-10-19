using UnityEngine;

namespace Foundation
{
    public interface ICharacterAgent
    {
        void Move(Vector2 dir);
        void Stop();
    }
}
