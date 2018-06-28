using System.Collections.Generic;

namespace _737Connector
{
    abstract class SimAction
    {
        public int Id;
        public string Type {get; set; }
        public bool IsSingleInstance {get; set; }
        protected List<int> _activeRegistersIndexes;
        protected List<byte> _currentRegisters = new List<byte>();
        protected readonly Connector _connector;
        protected readonly List<HashSet<int>> UsedBits;



        protected SimAction(Connector connector, List<int> activeRegistersIndexes, List<HashSet<int>> usedBits)
        {
            _connector = connector;
            _activeRegistersIndexes = activeRegistersIndexes;
            UsedBits = usedBits;
        }


        public abstract void Update(byte[] registers);
    }
}
