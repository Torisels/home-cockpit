﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockheedMartin.Prepar3D.SimConnect;

namespace _737Connector
{
    public class Connector
    {
        private SimConnect _simConnect;

        //delegate definitions
        public delegate void TextInvokerDelegate(TextBox tb, string s);
        public delegate void RichTextBoxInvokerDelegate(RichTextBox Rtb, string s);
        //delegate initializations
        private readonly TextInvokerDelegate _setTextInvoker;
        private readonly RichTextBoxInvokerDelegate _setRichTextBoxInvoker;

       

        private enum NOTIFICATION_GROUPS
        {
            MAINGROUP
        }
        enum DATA_REQUEST_ID
        {
            DATA_REQUEST
        }
        enum CLIENT_DATA_IDS
        {
            PMDG_NGX_DATA_ID = 0x4E477831,
            PMDG_NGX_DATA_DEFINITION = 0x4E477832,
            PMDG_NGX_CONTROL_ID = 0x4E477833,
            PMDG_NGX_CONTROL_DEFINITION = 0x4E477834,
        }

        public enum MOUSE_EVENTS:uint
        {
        MOUSE_FLAG_RIGHTSINGLE =  0x80000000,
        MOUSE_FLAG_MIDDLESINGLE=  0x40000000,
        MOUSE_FLAG_LEFTSINGLE  =  0x20000000,
        MOUSE_FLAG_RIGHTDOUBLE =  0x10000000,
        MOUSE_FLAG_MIDDLEDOUBLE = 0x08000000,
        MOUSE_FLAG_LEFTDOUBLE  =  0x04000000,
        MOUSE_FLAG_RIGHTDRAG   =  0x02000000,
        MOUSE_FLAG_MIDDLEDRAG  =  0x01000000,
        MOUSE_FLAG_LEFTDRAG    =  0x00800000,
        MOUSE_FLAG_MOVE        =  0x00400000,
        MOUSE_FLAG_DOWN_REPEAT=   0x00200000,
        MOUSE_FLAG_RIGHTRELEASE=  0x00080000,
        MOUSE_FLAG_MIDDLERELEASE =0x00040000,
        MOUSE_FLAG_LEFTRELEASE =  0x00020000,
        MOUSE_FLAG_WHEEL_FLIP =   0x00010000, // invert direction of mouse wheel
        MOUSE_FLAG_WHEEL_SKIP =   0x00008000,   // look at next 2 rect for mouse wheel commands
        MOUSE_FLAG_WHEEL_UP    =  0x00004000,
        MOUSE_FLAG_WHEEL_DOWN =   0x00002000
        }



        public Connector(SimConnect sc, TextInvokerDelegate setTextInvoker,RichTextBoxInvokerDelegate rd)
        {
           _setTextInvoker = setTextInvoker;
           // _setTextInvoker
           _setRichTextBoxInvoker = rd;
            try
            {
                _simConnect = sc;
                InitRecieve();//Initalize reciever
                RegisterEvents();//Register all events
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("ERROR WHEN INIT SIMCONNECT");
            }
        }
        private void InitRecieve()
        {
            _simConnect.MapClientDataNameToID("PMDG_NGX_Data", CLIENT_DATA_IDS.PMDG_NGX_DATA_ID);
            _simConnect.AddToClientDataDefinition(CLIENT_DATA_IDS.PMDG_NGX_DATA_DEFINITION, 0, (uint)Marshal.SizeOf(typeof(PMDG.PMDG_NGX_Data)), 0.0f, SimConnect.SIMCONNECT_UNUSED);
            _simConnect.RequestClientData(CLIENT_DATA_IDS.PMDG_NGX_DATA_ID, DATA_REQUEST_ID.DATA_REQUEST, CLIENT_DATA_IDS.PMDG_NGX_DATA_DEFINITION, SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0, 0, 0);
            _simConnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, PMDG.PMDG_NGX_Data>(CLIENT_DATA_IDS.PMDG_NGX_DATA_DEFINITION);
            _simConnect.OnRecvClientData += simconnect_RecvClientDataEvent;
        }
        public void RegisterEvents()
        {
            foreach (PMDG.PMDGEvents evnt in Enum.GetValues(typeof(PMDG.PMDGEvents)))
            {
                _simConnect.MapClientEventToSimEvent(evnt, $"#{(int)evnt}");
                _simConnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.MAINGROUP, evnt, false);
            }
            _simConnect.SetNotificationGroupPriority(NOTIFICATION_GROUPS.MAINGROUP, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);
        }


        public void SendEvent(PMDG.PMDGEvents evnt, uint value)
        {
            _simConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, evnt, value, PMDG.SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);       
        }

       

      

        void simconnect_RecvClientDataEvent(SimConnect sender, SIMCONNECT_RECV_CLIENT_DATA data)
        {

     
                    PMDG.PMDG_NGX_Data s1 = (PMDG.PMDG_NGX_Data)data.dwData[0];
            //                    _setTextInvoker(Form1.UiChanger.textBoxMcpAlt,s1.MCP_Altitude.ToString());
            //                     _setRichTextBoxInvoker(Form1.UiChanger.richTextBox1, s1.MCP_annunCMD_A.ToString()+ s1.MCP_annunCMD_B.ToString());
            //port.Write(IntStringToByte(s1.MCP_Altitude.ToString()),0,5);
            //            lock (Globals.Lock)
            //            {
            //            if (Globals.DataRecieved)
            //            {
            //                Globals.EventsData.Add(s1.MCP_annunCMD_A);
            //                Globals.DataRecieved = false;
            //            }
            Stopwatch stopwatch = Stopwatch.StartNew();
           
           // Globals.CMDA =Convert.ToBoolean( s1.MCP_annunCMD_A);
            Globals.AnnunArr[0] = s1.MCP_annunCMD_A;
            Globals.AnnunArr[1] = s1.MCP_annunATArm;
            Globals.AnnunArr[2] = s1.MCP_annunSPEED;
            Globals.AnnunArr[3] = s1.MCP_annunVNAV;
            Globals.AnnunArr[4] = s1.MCP_annunLVL_CHG;
            Globals.AnnunArr[5] = s1.MCP_annunLNAV;
            Globals.AnnunArr[6] = s1.MCP_annunCMD_B;
            Globals.AnnunArr[7] = s1.MCP_annunCMD_A;
            Globals.AnnunArr[8] = s1.MCP_annunCMD_A;
            Globals.AnnunArr[9] = s1.MCP_annunCMD_B;
            /*
             * Seven Segment Displays FROM PMDG
             */
            Globals.DisplayArray[0] = s1.MCP_Course[0];
            Globals.DisplayArray[1] = Convert.ToInt32(s1.MCP_IASMach);
            Globals.DisplayArray[2] = s1.MCP_Heading;
            Globals.DisplayArray[3] = s1.MCP_Altitude;
            Globals.DisplayArray[4] = s1.MCP_VertSpeed;
            Globals.DisplayArray[5] = s1.MCP_Course[1];
            Globals.DisplayArray[6] = Convert.ToInt32(s1.AIR_FltAltWindow);
            Globals.DisplayArray[7] = Convert.ToInt32(s1.AIR_LandAltWindow);
            /*
             *
             */
            //Thread.Sleep(100);
            stopwatch.Stop();
           // Console.WriteLine(stopwatch.ElapsedMilliseconds);
            var d = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("xd");


            //                Globals.EventsData.Add(s1.MCP_annunATArm);
            //                Globals.EventsData.Add(s1.MCP_annunSPEED);
            //                Globals.EventsData.Add(s1.MCP_annunVNAV);
            //                Globals.EventsData.Add(s1.MCP_annunLVL_CHG);
            //                Globals.EventsData.Add(s1.MCP_annunHDG_SEL);
            //                Globals.EventsData.Add(s1.MCP_annunLNAV);
            //                Globals.EventsData.Add(s1.);
            //                Globals.EventsData.Add(s1.MCP_annunAPP);
            //                Globals.EventsData.Add(s1.MCP_annunVS);
            //                Globals.EventsData.Add(s1.MCP_annunCMD_A);
            //                Globals.EventsData.Add(s1.MCP_annunCWS_A);
            //                Globals.EventsData.Add(s1.MCP_annunCMD_B);
            //                Globals.EventsData.Add(s1.MCP_annunCWS_B);
            //}

        }

        public static byte[] IntStringToByte(string s)
        {
            byte[] ret = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                ret[i] = Convert.ToByte(s[i]-'0');
            }
            return ret;
        }

        public static byte[] EncodeDigits(string s)
        {
            int len = s.Length;
            if (len % 2 == 1)
            {
                len++;
                s += "0";
            }
            byte[] ret = new byte[len/2];
            for (int i = 0; i < len; i++)
            {
                ret[i] = Encode(s[i] - '0', s[i + 1] - '0');
                i++;
            }
            return ret;
        }



        public static byte Encode(int first, int second)
        {
            return Convert.ToByte((first << 4) | second);
        }

        public int[] Decode(int todecode)
        {
            return new[] { todecode >> 4, todecode & 0b00001111 };
        }

        public int ByteArrayToLedPacket(byte[] arr)
        {
            int ret = 0;
            if (arr.Length > 8)
                throw new InvalidOperationException("Array is longer than expected");
            for (int i = 0; i < 8; i++)
            {
                ret |= arr[i] << i;
            }
            return ret;
        }


    }
}
