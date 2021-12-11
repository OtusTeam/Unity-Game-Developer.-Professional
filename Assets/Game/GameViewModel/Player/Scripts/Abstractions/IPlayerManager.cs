using UnityEngine;

namespace Prototype.ViewModel
{
    public interface IPlayerManager
    {
        ICharacter GetCharacter();

        void Move(Vector3 moveVector);
    }
}