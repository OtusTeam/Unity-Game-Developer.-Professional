namespace Foundation
{
    public interface IPlayerManager
    {
        int NumPlayers { get; }

        int AddPlayer(IPlayer player, bool reuseSlots = true);
        void RemovePlayer(IPlayer player);

        IPlayer GetPlayer(int index);
    }
}
