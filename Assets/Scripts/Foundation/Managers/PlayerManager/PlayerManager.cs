using System.Collections.Generic;

namespace Foundation
{
    public sealed class PlayerManager : AbstractService<IPlayerManager>, IPlayerManager
    {
        public int NumPlayers { get; private set; }

        List<IPlayer> players = new List<IPlayer>();

        public int AddPlayer(IPlayer player, bool reuseSlots)
        {
            if (reuseSlots) {
                for (int i = 0; i < players.Count; i++) {
                    if (players[i] == null) {
                        players[i] = player;
                        ++NumPlayers;
                        return i;
                    }
                }
            }

            int index = players.Count;
            players.Add(player);
            ++NumPlayers;

            return index;
        }

        public void RemovePlayer(IPlayer player)
        {
            int index = players.IndexOf(player);
            if (index >= 0 && players[index] != null) {
                DebugOnly.Check(NumPlayers > 0, "Player counter has been damaged.");
                --NumPlayers;
                players[index] = null;
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
