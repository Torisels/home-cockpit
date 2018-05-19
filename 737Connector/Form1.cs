using System;
using System.IO.Ports;
using System.Windows.Forms;
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;


namespace _737Connector
{
    public partial class Form1 : Form
    {
        // Instance of Form to change UI
        public static Form1 UiChanger;

        // User-defined win32 event 
        const int WM_USER_SIMCONNECT = 0x0402;

        // SimConnect object 
        private SimConnect simconnect = null;
        private Connector connector = null;

        //WinProccess to control SimConnect
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

        enum EnumDisplayStatus
        {
            a =1,
            b=2
        }

        public Form1()
        { 
            InitializeComponent();
            UiChanger = this;
            string stringValue = Enum.GetName(typeof(EnumDisplayStatus), 2);
            Console.WriteLine("sw");
            Console.WriteLine(stringValue);


            bool[] table = {true, true, true, false, true, false, true, true};

            byte a = Serial.BitsToRegisterBools(table);
            byte[] b = {0xFA, a};
            Serial.PrintBits(a);
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

                    connector = new Connector(simconnect, SetText,SetTextRich);

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
        public void SetTextRich(RichTextBox txtBox, string text)
        {
            if (txtBox.InvokeRequired)
            {
                Action<RichTextBox, string> method = SetTextRich;
                UiChanger.BeginInvoke(method, txtBox, text);
            }
            else
            {
                UiChanger.richTextBox1.Text = text;
                UiChanger.richTextBox1.SelectionStart = UiChanger.richTextBox1.Text.Length;
                UiChanger.richTextBox1.ScrollToCaret();
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
           
//            byte[] a = Connector.StringToByte("12345");
//            port.Write(a, 0, 5);
            //            string hexValue = $"{a[2]:X}";
            //            Console.WriteLine(hexValue);

        }

        private void richResponse_TextChanged(object sender, EventArgs e)
        {

        }
    }






}
