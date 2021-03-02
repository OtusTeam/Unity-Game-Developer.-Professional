using System.Collections.Generic;

namespace Foundation
{
    public interface IEnemyManager
    {
        ICollection<IEnemy> AllEnemies { get; }
        ICollection<CoverPoint> AllCoverPoints { get; }

        void AddEnemy(IEnemy enemy);
        void RemoveEnemy(IEnemy enemy);
    }
}
