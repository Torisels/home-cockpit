using System;
using System.IO.Ports;
using System.Windows.Forms;
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;


namespace _737Connector
{
    public partial class Form1 : Form
    {
        public static Form1 UiChanger;

        // User-defined win32 event 
        const int WM_USER_SIMCONNECT = 0x0402;

        // SimConnect object 
        private SimConnect simconnect = null;
        private Connector connector = null;


//        private SerialPort port = new SerialPort("COM3",9600);

        enum DATA_REQUEST_ID
        {
            DATA_REQUEST,
            CONTROL_REQUEST,
            AIR_PATH_REQUEST
        };

        enum CLIENT_DATA_IDS
        {
            PMDG_NGX_DATA_ID = 0x4E477831,
            PMDG_NGX_DATA_DEFINITION = 0x4E477832,
            PMDG_NGX_CONTROL_ID = 0x4E477833,
            PMDG_NGX_CONTROL_DEFINITION = 0x4E477834,
        };

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
            UiChanger = this;
//            port.Open();
        }
        // Simconnect client will send a win32 message when there is 
        // a packet to process. ReceiveMessage must be called to 
        // trigger the events. This model keeps simconnect processing on the main thread. 

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                simconnect?.ReceiveMessage();
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
                    PrepareToRecieve();
                    

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

        private void PrepareToRecieve()
        {
            simconnect.MapClientDataNameToID("PMDG_NGX_Data", CLIENT_DATA_IDS.PMDG_NGX_DATA_ID);
            simconnect.AddToClientDataDefinition(CLIENT_DATA_IDS.PMDG_NGX_DATA_DEFINITION, 0, (uint)Marshal.SizeOf(typeof(PMDG.PMDG_NGX_Data)), 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.RequestClientData(CLIENT_DATA_IDS.PMDG_NGX_DATA_ID, DATA_REQUEST_ID.DATA_REQUEST, CLIENT_DATA_IDS.PMDG_NGX_DATA_DEFINITION, SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0, 0, 0);
            simconnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, PMDG.PMDG_NGX_Data>(CLIENT_DATA_IDS.PMDG_NGX_DATA_DEFINITION);
            simconnect.OnRecvClientData += simconnect_RecvClientDataEvent;
        }

        void simconnect_RecvClientDataEvent(SimConnect sender, SIMCONNECT_RECV_CLIENT_DATA data)
        {

            switch ((DATA_REQUEST_ID)data.dwRequestID)
            {
                case DATA_REQUEST_ID.DATA_REQUEST:
                    PMDG.PMDG_NGX_Data s1 = (PMDG.PMDG_NGX_Data)data.dwData[0];
                    Console.WriteLine(s1.MCP_Heading);
                    break;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (simconnect == null)
            {
                try
                {
                    // the constructor is similar to SimConnect_Open in the native API 
                    simconnect = new SimConnect("Managed Client Events", this.Handle, WM_USER_SIMCONNECT, null, 0);

                    simconnect.OnRecvOpen += simconnect_OnRecvOpen;
                    simconnect.OnRecvQuit += simconnect_OnRecvQuit;

                    // listen to exceptions 
                    simconnect.OnRecvException += simconnect_OnRecvException;

                    connector = new Connector(simconnect, SetText);

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
            }
        }

        private void buttonSendEvent_Click(object sender, EventArgs e)
        {
            connector.SendEvent(PMDG.PMDGEvents.EVT_MCP_FD_SWITCH_L,(uint)Connector.MOUSE_EVENTS.MOUSE_FLAG_LEFTSINGLE);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            closeConnection();
        }

        public void SetText(TextBox txtBox, string text)
        {
            if (txtBox.InvokeRequired)
            {
                Action<TextBox, string> method = SetText;
                UiChanger.BeginInvoke(method, txtBox, text);
            }
            else
            {
                UiChanger.textBoxMcpAlt.Text =text ;
                UiChanger.textBoxMcpAlt.SelectionStart = UiChanger.textBoxMcpAlt.Text.Length;
                UiChanger.textBoxMcpAlt.ScrollToCaret();
            }
        }

        private void buttonSendEventTextBox_Click(object sender, EventArgs e)
        {
         //   var splitted = textBoxEventEnter.Text.Split(' ');
            int evet = Convert.ToInt32(textBoxEventEnter.Text);
           // int val = Convert.ToInt16(splitted[1]);
            //Console.WriteLine(evet+" "+val);
            connector.SendEvent((PMDG.PMDGEvents)evet, (uint)Connector.MOUSE_EVENTS.MOUSE_FLAG_LEFTSINGLE);

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            byte[] a = Connector.StringToByte("12345");
//            port.Write(a, 0, 5);
            //            string hexValue = $"{a[2]:X}";
            //            Console.WriteLine(hexValue);

        }
    }






}
