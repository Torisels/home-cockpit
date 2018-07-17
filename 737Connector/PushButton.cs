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
        public PushButton(int id,Connector connector,Dictionary<int,HashSet<int>> UsedBitsDict):base(connector,UsedBitsDict)
        {
            Id = id;
            Type = "PushButton";
            IsSingleInstance = true;
        }
        public override void Update(byte[] registers)
        {
            
            foreach (var index in UsedBitsDictionary)
            {               
                var bitsInIndexedRegister = new BitArray(~registers[index.Key]);
                for (int j = 0; j < bitsInIndexedRegister.Count; j++)
                {
                    if (bitsInIndexedRegister[j] && index.Value.Contains(j))
                        Connector.SendEvent((PMDG.PMDGEvents)Globals.EventsMap[index.Key, j], PMDG.MOUSE_FLAG_LEFTSINGLE);
                }
            }
           
        }
    }
}
