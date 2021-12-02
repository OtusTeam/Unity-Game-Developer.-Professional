using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField]
        private TeamType teamType;

        public TeamType GetTeamType()
        {
            return this.teamType;
        }
    }
}