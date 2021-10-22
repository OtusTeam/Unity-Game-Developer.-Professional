using System.Collections.Generic;

namespace Foundation
{
    public sealed class PlayerManager : AbstractService<IPlayerManager>, IPlayerManager
    {
        public int NumPlayers { get; private set; }

        public ObserverList<IOnPlayerAdded> OnPlayerAdded { get; } = new ObserverList<IOnPlayerAdded>();
        public ObserverList<IOnPlayerRemoved> OnPlayerRemoved { get; } = new ObserverList<IOnPlayerRemoved>();

        public ObserverList<IOnPlayerDamaged> OnPlayerDamaged { get; } = new ObserverList<IOnPlayerDamaged>();
        public ObserverList<IOnPlayerDied> OnPlayerDied { get; } = new ObserverList<IOnPlayerDied>();
        public ObserverList<IOnPlayerHealed> OnPlayerHealed { get; } = new ObserverList<IOnPlayerHealed>();

        List<IPlayer> players = new List<IPlayer>();

        public void AddPlayer(IPlayer player, out int index, bool reuseSlots)
        {
            if (reuseSlots) {
                for (int i = 0; i < players.Count; i++) {
                    if (players[i] == null) {
                        players[i] = player;
                        ++NumPlayers;
                        index = i;
                        return;
                    }
                }
            }

            index = players.Count;
            players.Add(player);
            ++NumPlayers;

            foreach (var observer in OnPlayerAdded.Enumerate())
                observer.Do(index);
        }

        public void RemovePlayer(IPlayer player)
        {
            int index = players.IndexOf(player);
            if (index >= 0 && players[index] != null) {
                DebugOnly.Check(NumPlayers > 0, "Player counter has been damaged.");
                --NumPlayers;
                players[index] = null;

                foreach (var observer in OnPlayerAdded.Enumerate())
                    observer.Do(index);
            }
        }

        public IPlayer GetPlayer(int index)
        {
            if (index < 0 || index >= players.Count)
                return null;

            return players[index];
        }
    }
}
