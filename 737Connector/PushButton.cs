using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class PushButton : SimAction
    {
        

        public PushButton(int id,Connector connector,List<int> activeRegisters,List<HashSet<int>> UsedBitsPerRegister):base(connector,activeRegisters,UsedBitsPerRegister)
        {
            Id = id;
            Type = "PushButton";
            IsSingleInstance = true;
        }
        public override void Update(byte[] registers)
        {
            List<byte> NewRegisters = new List<byte>();

            foreach (var index in _activeRegistersIndexes)
            {
                NewRegisters.Add(Convert.ToByte(~registers[index]));
            }
            for (int i = 0; i < NewRegisters.Count; i++)
            {
                var bitsInIthRegister = new BitArray(NewRegisters[i]);
                for (int j = 0; j < bitsInIthRegister.Count; j++)
                {
                    if(bitsInIthRegister[j]&&UsedBits[i].Contains(j))
                        _connector.SendEvent((PMDG.PMDGEvents)Globals.Registers[_activeRegistersIndexes[i],j],PMDG.MOUSE_FLAG_LEFTSINGLE);
                }
            }
            _currentRegisters = NewRegisters;
        }
    }
}
