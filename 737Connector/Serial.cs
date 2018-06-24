using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class Serial
    {
        public SerialPort Port;

        public Serial(string port, int baudrate)
        {
            Port = new SerialPort(port,baudrate);
            Port.ReadBufferSize = 64;
        }

        public void Connect()
        {
            Port.Open();
        }

        public void Write(byte[] buffer, int size, int offset = 0)
        {
            Port.Write(buffer, offset,size);
        }




        public static bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static bool IsBitSet(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
        public static byte BitsToRegisterBools(bool[] bools) //LSB is first, MSB is last
        {
            int a = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                a |= (Convert.ToInt32(bools[i])<<i);
            }
            return Convert.ToByte(a);
        }
        public static byte BitsToRegisterInts(int[] ints) //LSB is first, MSB is last
        {
            int a = 0;
            for (int i = 0; i < ints.Length; i++)
            {
                a |= ints[i]<< i;
            }
            return Convert.ToByte(a);
        }
        public static void PrintBits(byte val)
        {
            Console.WriteLine(Convert.ToString(val, 2).PadLeft(8, '0'));
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
