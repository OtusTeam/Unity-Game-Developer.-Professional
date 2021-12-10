using System.Collections.Generic;

namespace GameElements
{
    public interface IGameServiceGroup
    {
        IEnumerable<object> GetServices();
    }
}