using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameEngine
{
    [CreateAssetMenu(
        fileName = "TeamConfig",
        menuName = "GameEngine/Teams/New TeamConfig"
    )]
    public sealed class TeamConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        private Dictionary<TeamId, TeamInfo> infoMap;

        public TeamInfo GetTeam(TeamId id)
        {
            return this.infoMap[id];
        }

        [SerializeField]
        private TeamInfo[] infos;
        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.infoMap = new Dictionary<TeamId, TeamInfo>();
            foreach (var info in this.infos)
            {
                this.infoMap[info.id] = info;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        [Serializable]
        public sealed class TeamInfo
        {
            [SerializeField]
            public TeamId id;

            [SerializeField]
            public Color color;
        }
    }
}