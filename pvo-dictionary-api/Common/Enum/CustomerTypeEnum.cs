using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace pvo_dictionary_api.Common.Enum
{
    public enum CustomerTypeEnum : byte
    {
        [Description("New")] New = 0,
        [Description("Familiar")] Familiar = 1,
        [Description("VIP")] VIP = 2,
    }
}
