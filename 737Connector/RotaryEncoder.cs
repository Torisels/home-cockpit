using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class RotaryEncoder
    {
        private readonly object _lockobj;
        private readonly int _pos1;
        private readonly int _pos2;

        private int _oldState = 3;
        private int _position;
        private int _positionExt;
        private int _lastpos = 0;

        private const int LATCH_STATE = 3;

        private readonly sbyte[] _knobdir ={
            0, -1,  1,  0,
            1,  0,  0, -1,
            -1,  0,  0,  1,
            0,  1, -1,  0  };

        public RotaryEncoder(object lockObject,int bitPos1,int bitPos2)
        {
            _lockobj = lockObject;
            _pos1 = bitPos1;
            _pos2 = bitPos2;
        }

        public void tick(int register)
        {
            int thisState = GetBit(register, _pos1) | GetBit(register, _pos2) << 1;

            if (_oldState != thisState)
            {
                
                int a = thisState | (_oldState << 2);
        
                _position += _knobdir[a];

                //               
                if (thisState == LATCH_STATE)
                {
                    _positionExt = _position >> 2;
                    Console.WriteLine(_positionExt);
                }
                _oldState = thisState;
            } 
        }

        public static int GetBit(int b, int bitNumber)
        {
           return Convert.ToInt32(Serial.IsBitSet(b,bitNumber));
        }

        public int GetValue()
        {
            return _positionExt;
        }

    }
}
