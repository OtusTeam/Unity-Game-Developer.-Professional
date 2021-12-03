using System;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public abstract class MapEntityRenderComponent : EntityComponent, IMapEntityRenderComponent
    {
        private readonly Lazy<PositionComponent> positionComponent;

        private readonly Lazy<SizeComponent> sizeComponent;

        private readonly Lazy<WorldArea> worldArea;

        private int mapEntityId;

        public MapEntityRenderComponent()
        {
            this.positionComponent = this.GetComponentLazy<PositionComponent>();
            this.sizeComponent = this.GetComponentLazy<SizeComponent>();
            this.worldArea = this.GetServiceLazy<WorldArea>();
        }

        public void StartRender(MapEntityLayer layer)
        {
            var args = this.ProvideArgs();
            layer.AddEntity(args, out this.mapEntityId);
        }

        public void UpdateRender(MapEntityLayer layer)
        {
            var args = this.ProvideArgs();
            layer.UpdateEntity(this.mapEntityId, args);
        }

        public void FinishRender(MapEntityLayer layer)
        {
            layer.RemoveEntity(this.mapEntityId);
        }

        protected abstract Color ProvideColor();

        protected abstract Sprite ProvideIcon();
        
        private MapEntityLayer.Args ProvideArgs()
        {
            var worldArea = this.worldArea.Value;

            var worldPosition = this.positionComponent.Value.GetPosition();
            var normalizedPosition = worldArea.NormalizeVector(worldPosition);
            var normalizedUIPosition = new Vector2(normalizedPosition.x, normalizedPosition.z);

            var worldSize = this.sizeComponent.Value.GetSize();
            var normalizedSize = worldArea.NormalizeVector(worldSize);
            var normalizedUISize = new Vector2(normalizedSize.x, normalizedSize.z);

            var args = new MapEntityLayer.Args
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