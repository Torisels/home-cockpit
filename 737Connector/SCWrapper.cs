using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class SCWrapper
    {
        public enum GROUP_PRIORITY
        {
            SIMCONNECT_GROUP_PRIORITY_HIGHEST = 1,
            SIMCONNECT_GROUP_PRIORITY_HIGHEST_MASKABLE = 10000000,
            ID_PRIORITY_STANDARD = 1900000000,
            SIMCONNECT_GROUP_PRIORITY_DEFAULT = 2000000000
        };
    }
}
