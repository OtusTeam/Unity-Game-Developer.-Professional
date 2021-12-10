using Popups;

namespace Prototype.UI
{
    public sealed class GameResourcePopupArgs : IPopupArgs
    {
        public IGameResource GameResource { get; }

        public GameResourcePopupArgs(IGameResource gameResource)
        {
            this.GameResource = gameResource;
        }
    }
}