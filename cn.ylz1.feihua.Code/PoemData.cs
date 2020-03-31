using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.ylz1.feihua.Code
{
    public class PoemData
    {
        public string Key { get; set; }
        public string Verse { get; set; }
        public string Info { get; set; }
        public static List<PoemData> pd { get; private set; }
        private static List<string> flag = new List<string>();

        /// <summary>
        /// 启用插件，将词库读入内存
        /// </summary>
        public static void Init()
        {
            SQLiteConnection conn = new SQLiteConnection($"data source={Common.CQApi.AppDirectory}\\data.db;");
            conn.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand("Select * from Poem where 1=1", conn));
            DataSet set = new DataSet();
            adapter.Fill(set, "Poem");
            int count = set.Tables["Poem"].Rows.Count;
            pd = new List<PoemData>();
            for (int i = 0; i < count; i++)
            {
                string key = set.Tables["Poem"].Rows[i].Field<string>("PoemKey");
                pd.Add(new PoemData
                {
                    Info = set.Tables["Poem"].Rows[i].Field<string>("Info"),
                    Key = key,
                    Verse = set.Tables["Poem"].Rows[i].Field<string>("Verse")
                });
                if (flag.Contains(key) == false)
                {
                    flag.Add(key);
                }
            }
            set.Dispose();
            adapter.Dispose();
            conn.Close();
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

        public static int ImportData(DataSet _set,ImportInfo info)
        {
            try
            {
                //读入缓存
                info.Info = "导入缓存中...";
                foreach (DataRow item in _set.Tables["Poem"].Rows)
                {
                    string key = item.Field<string>("PoemKey");
                    pd.Add(new PoemData
                    {
                        Info = item.Field<string>("Info"),
                        Key = key,
                        Verse = item.Field<string>("Verse")
                    });
                    if (flag.Contains(key) == false)
                    {
                        flag.Add(key);
                    }
                }

                //写入词库
                SQLiteConnection conn = new SQLiteConnection($"data source={Common.CQApi.AppDirectory}\\data.db;");
                conn.Open();
                SQLiteCommand sc = new SQLiteCommand();
                sc.Connection = conn;
                int count = _set.Tables["Poem"].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    info.Info = $"正在导入{i + 1}/{count}\n请勿关闭酷Q或停用插件！";
                    sc.CommandText = $"INSERT INTO Poem VALUES('{_set.Tables["Poem"].Rows[i].Field<string>("PoemKey")}','" +
                        $"{_set.Tables["Poem"].Rows[i].Field<string>("Verse")}','" +
                        $"{_set.Tables["Poem"].Rows[i].Field<string>("Info")}');";
                    sc.ExecuteNonQuery();
                }
                sc.Dispose();
                conn.Close();
                conn.Dispose();
                return count;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void AddData(PoemData _pd)
        {
            SQLiteConnection conn = new SQLiteConnection($"data source={Common.CQApi.AppDirectory}\\data.db;");
            conn.Open();

            //写入词库
            SQLiteCommand sc = new SQLiteCommand();
            sc.Connection = conn;
            sc.CommandText += $"INSERT INTO Poem VALUES('{_pd.Key}','" +
                    $"{_pd.Verse}','" +
                    $"{_pd.Info}');";
            pd.Add(new PoemData
            {
                Info = _pd.Info,
                Key = _pd.Key,
                Verse = _pd.Verse
            });
            sc.ExecuteNonQuery();
            sc.Dispose();
            conn.Close();
            conn.Dispose();
        }
        public static void DelData(PoemData _pd)
        {
            SQLiteConnection conn = new SQLiteConnection($"data source={Common.CQApi.AppDirectory}\\data.db;");
            conn.Open();

            //写入词库
            SQLiteCommand sc = new SQLiteCommand();
            sc.Connection = conn;
            sc.CommandText += $"DELETE FROM Poem WHERE PoemKey='{_pd.Key}' and Verse='{_pd.Verse}' and Info='{_pd.Info}'";
            sc.ExecuteNonQuery();
            sc.Dispose();
            conn.Close();
            conn.Dispose();
            //缓存写入
            pd.Remove(pd.Where(p => p.Info == _pd.Info && p.Verse == _pd.Verse && p.Key == _pd.Key).FirstOrDefault());
        }
    }
}
