using Zenject;

namespace Foundation
{
    public sealed class Player : AbstractService<IPlayer>, IPlayer
    {
        int index = -1;
        public int Index => index;

        [Inject] IPlayerManager playerManager = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            playerManager.AddPlayer(this, out index);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerManager.RemovePlayer(this);
            index = -1;
        }
    }
}
