using Prototype.GameManagment;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class GameInjector : MonoInjector
    {
        [SerializeField]
        private GameManager gameManager;

        public override void InjectContext(object target)
        {
            this.gameManager.InjectGame(target);
        }
    }
}