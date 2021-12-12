using GameElements;
using Prototype.GameEngineAdapter;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class CastleCardController : MonoBehaviour,
        IGameInitElement
    {
        [SerializeField]
        private Card card;

        private ICastlesManager castlesManager;

        private ICastle targetCastle;
        
        public void Show(UIArguments args)
        {
            var castleId = args.Get<int>(UIArgumentName.CASTLE_ID);
            this.targetCastle = this.castlesManager.GetCastle(castleId);

            this.card.SetTitle(this.targetCastle.Name);
            this.card.SetIcon(this.targetCastle.Icon);
            this.card.SetProperty(0, $"Level: {this.targetCastle.Level}");
            this.card.SetProperty(1, $"Income: {this.targetCastle.Income}");
        }

        public void Hide()
        {
            this.targetCastle = null;
        }

        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.castlesManager = system.GetService<ICastlesManager>();
        }
    }
}