using Zenject;

namespace Foundation
{
    public sealed class Player : AbstractService<IPlayer>, IPlayer
    {
        public int Index { get; private set; } = -1;
        [Inject] IPlayerManager playerManager = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Index = playerManager.AddPlayer(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerManager.RemovePlayer(this);
            Index = -1;
        }
    }
}
