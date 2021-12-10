using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PlayerService : MonoBehaviour
    {
        public IEntity Character
        {
            get { return this.character; }
        }

        [SerializeField]
        private Entity character;
    }
}