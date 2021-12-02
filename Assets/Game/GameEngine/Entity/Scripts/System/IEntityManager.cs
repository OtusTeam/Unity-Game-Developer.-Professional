using System;
using System.Collections.Generic;

namespace GameEngine
{
    public interface IEntityManager
    {
        event Action<IEntity> OnEntityAdded;

        event Action<IEntity> OnEntityRemoved; 
        
        void AddEntity(IEntity entity);

        void RemoveEntity(IEntity entity);

        IEnumerable<IEntity> GetEntities();
    }
}