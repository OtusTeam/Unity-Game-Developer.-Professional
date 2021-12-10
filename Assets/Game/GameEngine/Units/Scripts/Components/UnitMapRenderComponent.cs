using System;
using Prototype.GameInterface;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Prototype.GameEngine
{
    public sealed class UnitMapRenderComponent : MapEntityRenderComponent
    {
        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private TeamConfig teamConfig;

        private readonly Lazy<TeamComponent> teamComponent;

        public UnitMapRenderComponent()
        {
            this.teamComponent = this.GetEntityComponentLazy<TeamComponent>();
        }

        protected override Color ProvideColor()
        {
            var teamId = this.teamComponent.Value.Id;
            var teamInfo = this.teamConfig.GetTeam(teamId);
            return teamInfo.color;
        }

        protected override Sprite ProvideIcon()
        {
            return this.icon;
        }
    }
}