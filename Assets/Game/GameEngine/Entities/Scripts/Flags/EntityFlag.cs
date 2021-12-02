using System;

namespace Prototype.GameEngine
{
    [Flags]
    public enum EntityFlag : uint
    {
        NONE,
        UNIT,
        RESOURCE,
    }
}