using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    static class Globals
    {
        public static int BoardsCount = 1;
        public static int MCUsCount = 4;
        public static int RegistersPerMCU = 4;
        public static int RegistersLength = 8;
        public static int[,] Registers = new int[BoardsCount * MCUsCount * RegistersPerMCU, RegistersPerMCU];
//        public static Connector conn = new Connector();
    }
}
