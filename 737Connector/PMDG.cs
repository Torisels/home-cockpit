using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class PMDG
    {
        //------------------------------------------------------------------------------
        //
        //  PMDG 737NGX external connection SDK
        //  Copyright (c) 2011 Precision Manuals Development Group
        // 
        //------------------------------------------------------------------------------

        // SimConnect data area definitions
        public const string PMDG_NGX_DATA_NAME	 = "PMDG_NGX_Data";
        public const int PMDG_NGX_DATA_ID = 0x4E477831;
        public const int  PMDG_NGX_DATA_DEFINITION = 0x4E477832;
        public const string PMDG_NGX_CONTROL_NAME = "PMDG_NGX_Control";
        public const int PMDG_NGX_CONTROL_ID = 0x4E477833;
        public const int PMDG_NGX_CONTROL_DEFINITION = 0x4E477834;

        public enum IDs
        {
            PMDG_NGX_DATA_ID = 0x4E477831,
            PMDG_NGX_DATA_DEFINITION = 0x4E477832
        }
        public enum DATA_REQUEST_ID
        {
            DATA_REQUEST,
            CONTROL_REQUEST,
            AIR_PATH_REQUEST
        }

    }
}
