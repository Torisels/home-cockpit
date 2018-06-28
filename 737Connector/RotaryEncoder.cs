using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class RotaryEncoder
    {

        private  int _reg1;
        private  int _reg2;

        private  int _pos1;
        private  int _pos2;

        private readonly int _eventId;
        private readonly Connector _connector;

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

        public RotaryEncoder(int eventId, Connector connector, int reg1, int reg2, int pos1, int pos2)
        {
            _eventId = eventId;
            _connector = connector;
            _reg1 = reg1;
            _reg2 = reg2;
            _pos1 = pos1;
            _pos2 = pos2;
        }

        public void tick(byte[] registers)
        {
            var thisState = GetBit(registers[_reg1], _pos1) | GetBit(registers[_reg2], _pos2) << 1;

            if (_oldState != thisState)
            {
        
                _position += _knobdir[thisState | (_oldState << 2)];

                             
                if (thisState == LATCH_STATE)
                {
                    //Console.WriteLine("Last Pos:"+_positionExt);
                    int _tempPos = _position >> 2;
                    //Console.WriteLine(_tempPos);
                    if(_tempPos>_positionExt)
                        Form1.Connector.SendEvent((PMDG.PMDGEvents) _eventId,PMDG.MOUSE_FLAG_LEFTSINGLE);
                    else if(_tempPos<_positionExt)
                        Form1.Connector.SendEvent((PMDG.PMDGEvents) _eventId, PMDG.MOUSE_FLAG_RIGHTSINGLE);
                    _positionExt = _tempPos;
                }
                _oldState = thisState;
            } 
        }

        public static int GetBit(int b, int bitNumber)
        {
           return Convert.ToInt32(Serial.IsBitSet(b,bitNumber));
        }

        public int getPos()
        {
            return _positionExt;
        }
        

    }
}
