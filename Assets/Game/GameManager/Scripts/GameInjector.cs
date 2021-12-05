using Prototype.GameManagment;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class GameInjector : MonoInjector
    {
        [SerializeField]
        private GameManager gameManager;

        public override void InjectContextInto(object target)
        {
            this.gameManager.AddGameComponent(target);
        }
    }
}