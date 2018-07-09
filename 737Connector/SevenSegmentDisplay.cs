using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class SevenSegmentDisplay
    {
        private const uint FLAG_NEW_MAX = 0xAF;
        private readonly int[] _usedDigits;
        const int NUMBER_OF_MCUS = 3;

        public SevenSegmentDisplay(int[] usedDigits)
        {
            _usedDigits = usedDigits;
        }

        public void GenerateDisplayData(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var intList = GetIntArray(data[i]);

            }
        }

        public static  List<int> GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            do
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            } while (num > 0);
            listOfInts.Reverse();
            return listOfInts;
//            return listOfInts.ToArray();
        }
    }
}
