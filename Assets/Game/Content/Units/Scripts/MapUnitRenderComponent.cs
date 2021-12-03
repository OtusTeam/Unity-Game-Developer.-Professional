using System;
using Prototype.GameInterface;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Prototype.GameEngine
{
    public sealed class MapUnitRenderComponent : MonoEntityComponent, IMapRenderer
    {

        [SerializeField]
        private Sprite icon;
        
        [Serializable]
        private sealed class TeamInfo
        {
            [SerializeField]
            public TeamType team;

            [SerializeField]
            public Color color;
        }
        
        public void Render(RectTransform plane)
        {
            var positionComponent = this.Entity.GetComponent<PositionComponent>();
            positionComponent.GetPosition();

            var sizeComponent = this.Entity.GetComponent<SizeComponent>();
            sizeComponent.GetSizeX();
            sizeComponent.GetSizeZ();
                

            var teamComponent = this.Entity.GetComponent<TeamComponent>();
            teamComponent.GetTeamType();
        }
    }
}