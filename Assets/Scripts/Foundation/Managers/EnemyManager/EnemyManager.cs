using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    public sealed class EnemyManager : AbstractService<IEnemyManager>, IEnemyManager
    {
        public ICollection<IEnemy> AllEnemies => enemies;
        List<IEnemy> enemies = new List<IEnemy>();

        public ICollection<CoverPoint> AllCoverPoints => coverPoints;
        [SerializeField] List<CoverPoint> coverPoints = new List<CoverPoint>();

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
