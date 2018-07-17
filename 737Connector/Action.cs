using System.Collections.Generic;

namespace _737Connector
{
    abstract class SimAction
    {
        public int Id;
        public string Type {get; set; }
        public bool IsSingleInstance {get; set; }
        protected List<int> ActiveRegistersIndexes;
     
        protected readonly Connector Connector;
      
        protected readonly Dictionary<int, HashSet<int>> UsedBitsDictionary;


        protected SimAction(Connector connector,  Dictionary<int, HashSet<int>> usedBitsDictionary)
        {
            Connector = connector;
            UsedBitsDictionary = usedBitsDictionary;
        }


        public abstract void Update(byte[] registers);
    }
}
