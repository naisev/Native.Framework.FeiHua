using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.ylz1.feihua.Code
{
    class PlayStatu
    {
        public bool Start { get; set; }
        public List<Player> Players { get; set; }
        public List<string> Verses { get; set; }
        public string Flag { get; set; }
        public int LastPlayerIndex { get; set; }
        public long CreateUid { get; set; }

        public PlayStatu()
        {
            Start = false;
            Players = new List<Player>();
            Verses = new List<string>();
            Flag = string.Empty;
            LastPlayerIndex = -1;
        }
    }
}
