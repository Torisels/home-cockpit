using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class Serial
    {


        public static bool IsBitSet(byte b, int pos)
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
    }
}
