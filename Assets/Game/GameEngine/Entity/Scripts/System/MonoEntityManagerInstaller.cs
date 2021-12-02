using UnityEngine;

namespace GameEngine
{
    [RequireComponent(typeof(IEntityManager))]
    public sealed class MonoEntityManagerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var initialEntities = FindObjectsOfType<MonoEntity>();
            var entityManager = this.GetComponent<IEntityManager>();
            for (int i = 0, count = initialEntities.Length; i < count; i++)
            {
                var entity = initialEntities[i];
                entityManager.AddEntity(entity);
            }
        }
    }
}