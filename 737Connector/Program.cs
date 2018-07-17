using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockheedMartin.Prepar3D.SimConnect;

namespace _737Connector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        const int WM_USER_SIMCONNECT = 0x0402;
        public static SimConnect simconnect = null;
        public static Connector Connector = null;
        [STAThread]
        static void Main()
        {
            
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }



    }
}
