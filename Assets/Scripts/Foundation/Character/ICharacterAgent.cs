using UnityEngine;

namespace Game
{
    public interface ICharacterAgent
    {
        void Move(Vector2 dir);
        void Stop();
    }
}
