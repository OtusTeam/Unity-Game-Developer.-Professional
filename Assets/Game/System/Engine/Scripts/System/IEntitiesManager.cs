using System;
using System.Collections.Generic;

namespace Prototype.GameEngine
{
    public interface IEntitiesManager
    {
        event Action<IEntity> OnEntityAdded;

        event Action<IEntity> OnEntityRemoved; 
        
        void AddEntity(IEntity entity);

        void RemoveEntity(IEntity entity);

        IEntity GetEntity(int id);
        
        IEnumerable<IEntity> GetEntities();
    }
}