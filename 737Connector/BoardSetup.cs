
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class BoardSetup
    {
        //each board has 4 chips
        

        private readonly int _boardId;

        public BoardSetup(int boardId)
        {
            _boardId = boardId;
        }
        /*
         * FLAG_SETUP_DATA_BEGIN,DDRA,PORTA,DDRB,PORTB,DDRC,PORTC,DDRD,PORTD,USE_ANALOG_FLAG[,NUMBER_OF_ANALOG,ANALOG_BITMASK FLAG],FLAG_SETUP_DATA_FINISHED
         */
        public byte[] PrepareDataArray(int mcuId)
        {
            List<byte> analogProperties = new List<byte>();

            bool useAnalog = getAnalogProperties(mcuId, ref analogProperties);

            int buffer_size = 11;

            if (useAnalog)
                buffer_size += 2;

            byte[] to_send = new byte[buffer_size];

            List<byte> sendList= new List<byte>();
            //sendList.Add(BoardSetupConstants.FLAG_PC_SETUP_RX_MODE);
            sendList.Add(BoardSetupConstants.FLAG_NEW_SLAVE);
            sendList.Add(Convert.ToByte(mcuId));
            sendList.AddRange(getRegistersForMCU(mcuId));
            sendList.Add(Convert.ToByte(useAnalog));
            if(useAnalog)
                sendList.AddRange(analogProperties);
            sendList.Add(BoardSetupConstants.FLAG_SETUP_DATA_TRANSFER_FINISHED);

            if(sendList.Count != buffer_size)   
                throw new InvalidOperationException();

            return sendList.ToArray();
        }

        private List<byte> getRegistersForMCU(int mcuId)
        {
            return new List<byte>();
        }
        private bool getAnalogProperties(int mcuId,ref List<byte> AnalogProperties)
        {
            throw new NotImplementedException();
            bool useAnalog = false;
            return useAnalog;
        }

    }

    static class BoardSetupConstants
    {
        public static byte FLAG_PC_SETUP_RX_MODE = 0xAB;
        public static byte FLAG_NEW_SLAVE = 0xDD;
        public static byte FLAG_SETUP_DATA_TRANSFER_FINISHED = 0xCE;
    }
}
