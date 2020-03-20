using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.ylz1.feihua.Code
{
    class Player
    {
        public long Uid { get; set; }
        public int Score { get; set; }
        public string Verse { get; set; }
        public List<string> AllVerse { get; set; }
        public Player(long uid)
        {
            Uid = uid;
            Score = 0;
            Verse = string.Empty;
            AllVerse = new List<string>();
        }
    }
}
