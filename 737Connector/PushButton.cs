using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class PushButton
    {
        List<int> ActiveRegisters = new List<int>();
        Dictionary<int,byte> ActiveRegistersBitMasksDictionary = new Dictionary<int, byte>();
        private readonly Connector _connector;


        private List<byte> CurrentRegisters = new List<byte>();

        public PushButton(Dictionary<int,byte> activeRegistersBitMasks, Connector Connector)
        {
            ActiveRegistersBitMasksDictionary = activeRegistersBitMasks;
            _connector = Connector;
        }
        public void Update(byte[] registers)
        {
            List<byte> NewRegisters = new List<byte>();
//            List<int> Delta = new List<int>();

            foreach (var index in ActiveRegisters)
            {
                NewRegisters.Add(Convert.ToByte(~registers[index]));
            }

            if(NewRegisters.Count!=CurrentRegisters.Count)
                throw new InRowChangingEventException();

            for (int i = 0; i < NewRegisters.Count; i++)
            {
                var array = new BitArray(/*NewRegisters[i] ^ */CurrentRegisters[i]);//todo to be changed
                for (int j = 0; j < array.Count; j++)
                {
                    if(array[i])
                        _connector.SendEvent((PMDG.PMDGEvents)getEventByRegister(ActiveRegisters[i],j),PMDG.MOUSE_FLAG_LEFTSINGLE);
                }
            }
            CurrentRegisters = NewRegisters;

//            BitArray barray = new BitArray(Delta);
//
//            for (int i = 0; i <barray.Count; i++)
//            {
//                if(barray[i])
//                    _connector.SendEvent((PMDG.PMDGEvents)1,PMDG.MOUSE_FLAG_LEFTSINGLE);
//            }


        }


        public int getEventByRegister(int registerId, int bitPos)
        {
            return 1;//todo to be implemented

        }
    }
}
