namespace Foundation
{
    public interface IPlayerManager
    {
        int NumPlayers { get; }

        ObserverList<IOnPlayerAdded> OnPlayerAdded { get; }
        ObserverList<IOnPlayerRemoved> OnPlayerRemoved { get; }

        void AddPlayer(IPlayer player, out int index, bool reuseSlots = true);
        void RemovePlayer(IPlayer player);

        IPlayer GetPlayer(int index);
    }
}
