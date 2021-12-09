using System;
using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public abstract class MapEntityRenderComponent : EntityComponent, IMapEntityRenderComponent,
        IGameInitElement
    {
        private readonly Lazy<PositionComponent> positionComponent;

        private readonly Lazy<SizeComponent> sizeComponent;

        private WorldArea worldArea;

        private int mapEntityId;

        public MapEntityRenderComponent()
        {
            this.positionComponent = this.GetEntityComponentLazy<PositionComponent>();
            this.sizeComponent = this.GetEntityComponentLazy<SizeComponent>();
        }
        
        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.worldArea = system.GetService<WorldArea>();
        }

        public void StartRender(IMapEntityLayer layer)
        {
            var args = this.ProvideArgs();
            layer.AddEntity(args, out this.mapEntityId);
        }

        public void UpdateRender(IMapEntityLayer layer)
        {
            var args = this.ProvideArgs();
            layer.UpdateEntity(this.mapEntityId, args);
        }

        public void FinishRender(IMapEntityLayer layer)
        {
            layer.RemoveEntity(this.mapEntityId);
        }

        protected abstract Color ProvideColor();

        protected abstract Sprite ProvideIcon();
        
        private MapEntityArgs ProvideArgs()
        {
            var worldPosition = this.positionComponent.Value.GetPosition();
            var normalizedPosition = this.worldArea.NormalizeVector(worldPosition);
            var normalizedUIPosition = new Vector2(normalizedPosition.x, normalizedPosition.z);

            var worldSize = this.sizeComponent.Value.GetSize();
            var normalizedSize = this.worldArea.NormalizeVector(worldSize);
            var normalizedUISize = new Vector2(normalizedSize.x, normalizedSize.z);

            var args = new MapEntityArgs
            {
                normalizedPosition = normalizedUIPosition,
                normalizedSize = normalizedUISize,
                icon = this.ProvideIcon(),
                color = this.ProvideColor()
            };
            return args;
        }
    }
}