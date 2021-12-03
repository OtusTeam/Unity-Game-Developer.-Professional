using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public TeamId Id
        {
            get { return this.teamId; }
        }

        [SerializeField]
        private TeamId teamId;
    }
}