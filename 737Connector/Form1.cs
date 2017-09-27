﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FSUIPC;
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;


namespace _737Connector
{
    //    public partial class Form1 : Form
    //    {
    //        SimConnect _simconnect = null;
    //        const int WM_USER_SIMCONNECT = 0x0402;
    //       
    //        enum EVENT_ID
    //        {
    //            EVT_OH_YAW_DAMPER = 69632 + 63
    //        };
    //        enum GROUP_ID
    //        {
    //            GROUP_1,
    //        };
    //
    //        public Form1()
    //        {
    //            InitializeComponent();
    //        }
    //
    //
    //        public void SimConnectOpen()
    //        {
    //            try
    //            {
    //                _simconnect = new SimConnect("PMDG NGX Test", this.Handle, 0, null, 0);
    //                _simconnect.MapClientDataNameToID(PMDG.PMDG_NGX_DATA_NAME,PMDG.IDs.PMDG_NGX_DATA_ID);
    //                _simconnect.RequestClientData(PMDG.IDs.PMDG_NGX_DATA_ID,PMDG.DATA_REQUEST_ID.DATA_REQUEST,PMDG.IDs.PMDG_NGX_DATA_DEFINITION,SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0,0,0);
    //                _simconnect.MapClientEventToSimEvent(EVENT_ID.EVT_OH_YAW_DAMPER, "#69695");
    //                _simconnect.TransmitClientEvent(0, EVENT_ID.EVT_OH_YAW_DAMPER, 1, SCWrapper.GROUP_PRIORITY.SIMCONNECT_GROUP_PRIORITY_HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
    //                //  _simconnect.MapClientEventToSimEvent(EVENT_ID.EVT_OH_YAW_DAMPER,"#69695");
    //                //  _simconnect.AddClientEventToNotificationGroup(GROUP_ID.GROUP_1, EVENT_ID.EVT_OH_YAW_DAMPER, false);
    //                // _simconnect.TransmitClientEvent(0,EVENT_ID.EVT_OH_YAW_DAMPER,1, GROUP_ID.GROUP_1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
    //                while (true)
    //                {
    //                    _simconnect.ReceiveDispatch();
    //                }
    //            }
    //            catch (COMException ex)
    //            {
    //                Console.WriteLine(ex.Message);
    //                // A connection to the SimConnect server could not be established 
    //            }
    //        }
    //
    //        private void button2_Click(object sender, EventArgs e)
    //        {
    //            _simconnect.TransmitClientEvent(0, EVENT_ID.EVT_OH_YAW_DAMPER, 1, SCWrapper.GROUP_PRIORITY.SIMCONNECT_GROUP_PRIORITY_HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
    //        }
    //    }

    public partial class Form1 : Form
    {

        // User-defined win32 event 
        const int WM_USER_SIMCONNECT = 0x0402;

        // SimConnect object 
        SimConnect simconnect = null;

        enum EVENTS
        {
            PITOT_TOGGLE,
            FLAPS_INC,
            FLAPS_DEC,
            FLAPS_UP,
            FLAPS_DOWN,
            EVT_OH_YAW_DAMPER = 69632 + 63
        };

        enum NOTIFICATION_GROUPS
        {
            GROUP0,
        }

        public Form1()
        {
            InitializeComponent();

            setButtons(true, false);
        }
        // Simconnect client will send a win32 message when there is 
        // a packet to process. ReceiveMessage must be called to 
        // trigger the events. This model keeps simconnect processing on the main thread. 

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (simconnect != null)
                {
                    simconnect.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void setButtons(bool bConnect, bool bDisconnect)
        {
            buttonConnect.Enabled = bConnect;
            buttonDisconnect.Enabled = bDisconnect;
        }

        private void closeConnection()
        {
            if (simconnect != null)
            {
                // Dispose serves the same purpose as SimConnect_Close() 
                simconnect.Dispose();
                simconnect = null;
                displayText("Connection closed");
            }
        }

        // Set up all the SimConnect related event handlers 
        private void initClientEvent()
        {
            try
            {
                // listen to connect and quit msgs 
                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);

                // listen to exceptions 
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);

                // listen to events 
                simconnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(simconnect_OnRecvEvent);

                // subscribe to pitot heat switch toggle 
                simconnect.MapClientEventToSimEvent(EVENTS.PITOT_TOGGLE, "PITOT_HEAT_TOGGLE");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.PITOT_TOGGLE, false);


                simconnect.MapClientEventToSimEvent(EVENTS.EVT_OH_YAW_DAMPER, "#69695");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.EVT_OH_YAW_DAMPER, false);
                // subscribe to all four flaps controls 

                simconnect.TransmitClientEvent(1, EVENTS.EVT_OH_YAW_DAMPER, 1, SCWrapper.GROUP_PRIORITY.SIMCONNECT_GROUP_PRIORITY_HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_UP, "FLAPS_UP");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_UP, false);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_DOWN, "FLAPS_DOWN");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_DOWN, false);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_INC, "FLAPS_INCR");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_INC, false);
                simconnect.MapClientEventToSimEvent(EVENTS.FLAPS_DEC, "FLAPS_DECR");
                simconnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, EVENTS.FLAPS_DEC, false);

                // set the group priority 
                simconnect.SetNotificationGroupPriority(NOTIFICATION_GROUPS.GROUP0, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

            }
            catch (COMException ex)
            {
                displayText(ex.Message);
            }
        }

        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            displayText("Connected to Prepar3D");
        }

        // The case where the user closes Prepar3D 
        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            displayText("Prepar3D has exited");
            closeConnection();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            displayText("Exception received: " + data.dwException);
        }

        void simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent)
        {
            switch (recEvent.uEventID)
            {
                case (uint)EVENTS.PITOT_TOGGLE:

                    displayText("PITOT switched");
                    break;

                case (uint)EVENTS.FLAPS_UP:

                    displayText("Flaps Up");
                    break;

                case (uint)EVENTS.FLAPS_DOWN:

                    displayText("Flaps Down");
                    break;

                case (uint)EVENTS.FLAPS_INC:

                    displayText("Flaps Inc");
                    break;

                case (uint)EVENTS.FLAPS_DEC:

                    displayText("Flaps Dec");
                    break;
            }
        }

        // The case where the user closes the client 
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeConnection();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
          
        }

        // Response number 
        int response = 1;

        // Output text - display a maximum of 10 lines 
        string output = "\n\n\n\n\n\n\n\n\n\n";

        void displayText(string s)
        {
            // remove first string from output 
            output = output.Substring(output.IndexOf("\n") + 1);

            // add the new string 
            output += "\n" + response++ + ": " + s;

            // display it 
            richResponse.Text = output;
        }

        private void buttonConnect_Click_1(object sender, EventArgs e)
        {
            if (simconnect == null)
            {
                try
                {
                    // the constructor is similar to SimConnect_Open in the native API 
                    simconnect = new SimConnect("Managed Client Events", this.Handle, WM_USER_SIMCONNECT, null, 0);

                    setButtons(false, true);

                    initClientEvent();

                }
                catch (COMException ex)
                {
                    displayText("Unable to connect to Prepar3D " + ex.Message);
                }
            }
            else
            {
                displayText("Error - try again");
                closeConnection();

                setButtons(true, false);
            }
        }

        private void buttonDisconnect_Click_1(object sender, EventArgs e)
        {
            closeConnection();
            setButtons(true, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.EVT_OH_YAW_DAMPER, , SCWrapper.GROUP_PRIORITY.SIMCONNECT_GROUP_PRIORITY_HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
        }
    }






}