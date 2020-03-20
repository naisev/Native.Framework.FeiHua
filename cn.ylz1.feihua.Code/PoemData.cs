using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.ylz1.feihua.Code
{
    class PoemData
    {
        public string Key { get; set; }
        public string Verse { get; set; }
        public string Info { get; set; }
        private static PoemData[] pd;
        private static List<string> flag = new List<string>();
        public static void Init()
        {
            SQLiteConnection conn = new SQLiteConnection($"data source={Common.CQApi.AppDirectory}\\data.db;");
            conn.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand("Select * from Poem where 1=1", conn));
            DataSet set = new DataSet();
            adapter.Fill(set, "Poem");
            int count = set.Tables["Poem"].Rows.Count;
            pd = new PoemData[count];
            for (int i = 0; i < count; i++)
            {
                string key = set.Tables["Poem"].Rows[i].Field<string>("PoemKey");
                pd[i] = new PoemData
                {
                    Info = set.Tables["Poem"].Rows[i].Field<string>("Info"),
                    Key = key,
                    Verse = set.Tables["Poem"].Rows[i].Field<string>("Verse")
                };
                if (flag.Contains(key) == false)
                {
                    flag.Add(key);
                }
            }
            set.Dispose();
            adapter.Dispose();
            conn.Dispose();
        }
        public static PoemData VerseIsExist(string verse)
        {
            foreach (var item in pd)
            {
                if (item.Verse == verse) { return new PoemData { Key = item.Key, Verse = item.Verse, Info = item.Info }; };
            }
            return null;
        }

        public static string GetFlag()
        {
            return flag[new Random().Next(0, flag.Count)];
        }
    }
}
