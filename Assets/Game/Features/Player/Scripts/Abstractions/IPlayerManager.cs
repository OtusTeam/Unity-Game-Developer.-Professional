using UnityEngine;

namespace Prototype
{
    public interface IPlayerManager
    {
        ICharacter GetCharacter();

        void Move(Vector3 moveVector);
    }
}