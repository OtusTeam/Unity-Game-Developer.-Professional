using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    public interface IVehicle
    {
        float Forward { get; set; }
        float Turn { get; set; }
        float SpeedKmh { get; }
    }
}
