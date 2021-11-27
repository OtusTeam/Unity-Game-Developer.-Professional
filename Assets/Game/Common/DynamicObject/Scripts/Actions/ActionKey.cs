using System;

namespace DynamicObjects
{
    [Serializable]
    public enum ActionKey
    {
        TAKE_DAMAGE = -1,
        DIE = -2,
        SHOW = -3,
        HIDE = -4,
        ATTACK = -5,
        COLLECT = -6,
        MOVE = -7
    }
}