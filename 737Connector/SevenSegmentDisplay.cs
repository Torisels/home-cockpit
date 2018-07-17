using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class SevenSegmentDisplay
    {
        private const byte FLAG_MAX_START = 0xDA;
        private const byte FLAG_MAX_STOP = 0xEA;
        private readonly int[] _usedDigits;

        private readonly int _count;
      



        //max7219 communication constants

        private Dictionary<int,int[]> _displayData;
        private readonly Dictionary<int, bool> CustomDigitOrder;
        private readonly Dictionary<int, List<int>> displayOrderDictionary;

        const byte max7219_reg_noop = 0x00;
        const byte max7219_reg_decodeMode = 0x09;
        const byte max7219_reg_intensity = 0x0a;
        const byte max7219_reg_scanLimit = 0x0b;
        const byte max7219_reg_shutdown = 0x0c;
        const byte max7219_reg_displayTest = 0x0f;



        public SevenSegmentDisplay(int count, Dictionary<int, bool> customDigitOrder, Dictionary<int, List<int>> displayOrderDictionary)
        {
            _count = count;
            CustomDigitOrder = customDigitOrder;
            this.displayOrderDictionary = displayOrderDictionary;

            _displayData = new Dictionary<int, int[]>();

        }

        public void GenerateDisplayData(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var intList = GetIntArray(data[i]);

            }
        }

        private void Fill()
        {
            for (int i = 0; i < _count; i++)
            {
                var finaldigits = new List<int>();
                if (CustomDigitOrder[i])
                {
                    //todo add this feature
                }
                else
                {
                    foreach (int displayIndex in displayOrderDictionary[i])
                    {
                        finaldigits.AddRange(GetIntArray(Globals.DisplayArray[displayIndex]));
                    }
                }

                _displayData[i] = finaldigits.ToArray();
            }
        }


        public static List<int> GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            if (num < 0)
            {
                listOfInts.Add(10);
                num = Math.Abs(num);
            }
            do
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            } while (num > 0);
            listOfInts.Reverse();
            return listOfInts;
//            return listOfInts.ToArray();
        }

        public byte[] GetData()
        {
            var ret = new List<byte>();
            ret.Add(FLAG_MAX_START);
            foreach (var display in _displayData)
            {
                for (int i = 0; i < display.Value.Length; i++)
                {
                    ret.Add(Convert.ToByte(i|display.Value[i]<<4));
                }
            }
            ret.Add(FLAG_MAX_STOP);
            return ret.ToArray();
        }
    }
}
