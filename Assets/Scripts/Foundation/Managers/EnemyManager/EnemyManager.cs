using System.Collections.Generic;

namespace Foundation
{
    public sealed class EnemyManager : AbstractService<IEnemyManager>, IEnemyManager
    {
        public ICollection<IEnemy> AllEnemies => enemies;
        List<IEnemy> enemies = new List<IEnemy>();

        public void AddEnemy(IEnemy enemy)
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
