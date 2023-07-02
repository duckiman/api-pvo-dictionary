using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace pvo_dictionary_api.Common.Enum
{
    public enum RoomStatusEnum : byte
    {
        [Description("VacantDirty")] VacantDirty = 0,
        [Description("VacantClean")] VacantClean = 1,
        [Description("VacantReady")] VacantReady = 2,
        [Description("Occupied")] Occupied = 3,
        [Description("ExpectedArrival")] ExpectedArrival = 4,
    }
}
