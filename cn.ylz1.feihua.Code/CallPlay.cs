using Native.Sdk.Cqp;
using Native.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.ylz1.feihua.Code
{
    class CallPlay
    {
        private static PlayStatu ps = new PlayStatu();

        public static string Create(long uid)
        {
            if (ps.Start)
            {
                return CQApi.CQCode_At(uid) + "飞花令已经创建啦！";
            }
            ps.Start = true;
            ps.CreateUid = uid;
            return CQApi.CQCode_At(uid) + "飞花令创建完成！\n请选手们回复【飞花令加入】参与！\n" + CQApi.CQCode_At(uid) + "参与人数达2人及以上，回复【飞花令开始】开始比赛！";
        }

        public static string Join(long uid)
        {
            //检查是否创建
            if (ps.Start == false)
            {
                return CQApi.CQCode_At(uid) + "飞花令还未创建！回复【飞花令创建】创建一场比赛";
            }

            //检查是否开始
            if (ps.Flag != "")
            {
                return CQApi.CQCode_At(uid) + "飞花令已经开始了！不能中途参加！";
            }

            //检查是否参加
            foreach (var tmp in ps.Players)
            {
                if (tmp.Uid == uid)
                {
                    return CQApi.CQCode_At(uid) + "你已经加入比赛！";
                }
            }
            ps.Players.Add(new Player(uid));
            return CQApi.CQCode_At(uid) + "加入比赛成功！" + CQApi.CQCode_At(ps.CreateUid) + "如果觉得可以开始比赛了，请回复【飞花令开始】开始比赛！";
        }

        public static string Start(long uid)
        {
            //检查是否创建
            if (ps.Start == false)
            {
                return CQApi.CQCode_At(uid) + "飞花令还未创建！回复【飞花令创建】创建一场比赛";
            }

            //检查是否开始
            if (ps.Flag != "")
            {
                return CQApi.CQCode_At(uid) + "飞花令已经开始了！";
            }

            //检查是否发起者
            if (ps.CreateUid != uid)
            {
                return CQApi.CQCode_At(uid) + "不是创建者无法开始飞花令！\n" + CQApi.CQCode_At(uid) + "如果觉得可以开始比赛了，请回复【飞花令开始】开始比赛！";
            }

            if (ps.Players.Count < 2)
            {
                return CQApi.CQCode_At(uid) + "人数不足！无法开始比赛！\n请选手们回复【飞花令加入】参与！";
            }

            ps.Flag = PoemData.GetFlag();
            return $"飞花令开始！\n系统出题，关键字为【{ps.Flag}】\n请回复带有【{ps.Flag}】的诗句！\n(直接回复诗句即可)\n" +
                $"现在请{CQApi.CQCode_At(ps.Players[0].Uid)}首先开始回复！\n(PS:如果暂时想不出来，请回复【飞花令跳过】)";
        }

        public static string Reply(string msg, long uid)
        {
            //检查是否创建
            if (ps.Start == false)
            {
                return string.Empty;
                //return CQApi.CQCode_At(uid) + "飞花令还未创建！回复【飞花令创建】创建一场比赛";
            }

            //检查是否开始
            if (ps.Flag == "")
            {
                return string.Empty;
                //return CQApi.CQCode_At(uid) + "飞花令还未开始！" + CQApi.CQCode_At(uid) + "如果觉得可以开始比赛了，请回复【飞花令开始】开始比赛！";
            }

            //检查是否参加
            bool isJoin = false;
            foreach (var tmp in ps.Players)
            {
                if (tmp.Uid == uid)
                {
                    isJoin = true;
                    break;
                }
            }
            if (isJoin == false)
            {
                return string.Empty;
                //return CQApi.CQCode_At(uid) + "你未参加本次飞花令比赛！";
            }

            //检查当前轮玩家
            int index = ps.LastPlayerIndex + 1;
            if (index >= ps.Players.Count) { index = 0; }
            if (ps.Players[index].Uid != uid)
            {
                return string.Empty;
                //return CQApi.CQCode_At(uid) + "还未轮到你回复！\n现在请" + CQApi.CQCode_At(ps.Players[index].Uid) + "回复";
            }

            //判断语句是否包含关键字
            msg = msg.Replace(" ", "");
            string verse = msg.StartsWith("飞花令回复") ? StrongString.GetRight(msg, "飞花令回复") : msg;
            if (verse.Length < 4 || verse.Length > 8 || verse.Contains(ps.Flag) == false)
            {
                return string.Empty;
                //return CQApi.CQCode_At(uid) + $"诗句不包含【{ps.Flag}】！";
            }

            //检查语句是否重复;
            bool isRepeat = false;
            foreach (var tmp in ps.Verses)
            {
                if (tmp == verse)
                {
                    isRepeat = true;
                    break;
                }
            }
            if (isRepeat == true)
            {
                return CQApi.CQCode_At(uid) + "该诗句已经有人说过了！";
            }

            //检查是否正确诗句
            PoemData pd = PoemData.VerseIsExist(verse);
            if (pd == null)
            {
                return CQApi.CQCode_At(uid) + "诗句数据库不包含该诗句！";
            }

            //加入回复
            ps.Verses.Add(verse);
            ps.Players[index].AllVerse.Add(verse);
            ps.Players[index].Score++;
            ps.Players[index].Verse = verse;
            ps.LastPlayerIndex = index;

            int nextIndex = ps.LastPlayerIndex + 1;
            if (nextIndex >= ps.Players.Count) { nextIndex = 0; }
            return CQApi.CQCode_At(uid) +
                $"回复成功！\n{CQApi.CQCode_At(uid)}积分+1，当前积分{ps.Players[ps.LastPlayerIndex].Score}\n“{verse}”出自{pd.Info}" +
                $"\n现在请{CQApi.CQCode_At(ps.Players[nextIndex].Uid)}回复\n(直接回复诗句即可)\n(PS:如果暂时想不出来，请回复【飞花令跳过】)";
        }

        public static string Leap(long uid)
        {
            //检查是否创建
            if (ps.Start == false)
            {
                return CQApi.CQCode_At(uid) + "飞花令还未创建！回复【飞花令创建】创建一场比赛";
            }

            //检查是否开始
            if (ps.Flag == "")
            {
                return CQApi.CQCode_At(uid) + "飞花令还未开始！" + CQApi.CQCode_At(uid) + "如果觉得可以开始比赛了，请回复【飞花令开始】开始比赛！";
            }

            //检查是否参加
            bool isJoin = false;
            foreach (var tmp in ps.Players)
            {
                if (tmp.Uid == uid)
                {
                    isJoin = true;
                    break;
                }
            }
            if (isJoin == false)
            {
                return CQApi.CQCode_At(uid) + "你未参加本次飞花令比赛！";
            }

            //检查当前轮玩家
            int index = ps.LastPlayerIndex + 1;
            if (index >= ps.Players.Count) { index = 0; }
            if (ps.Players[index].Uid != uid)
            {
                return CQApi.CQCode_At(uid) + "还未轮到你回复！\n现在请" + CQApi.CQCode_At(ps.Players[index].Uid) + "回复";
            }

            ps.LastPlayerIndex = index;
            int nextIndex = ps.LastPlayerIndex + 1;
            if (nextIndex >= ps.Players.Count) { nextIndex = 0; }
            return $"{CQApi.CQCode_At(uid)}你跳过了本轮飞花令。本轮不加积分。\n现在请{CQApi.CQCode_At(ps.Players[nextIndex].Uid)}回复\n回复格式为【飞花令回复 诗句】\n(PS:如果暂时想不出来，请回复【飞花令跳过】)";
        }

        public static string End(long uid)
        {
            //检查是否创建
            if (ps.Start == false)
            {
                return CQApi.CQCode_At(uid) + "飞花令还未创建！回复【飞花令创建】创建一场比赛";
            }

            //检查是否开始
            if (ps.Flag == "")
            {
                return CQApi.CQCode_At(uid) + "飞花令还未开始！" + CQApi.CQCode_At(uid) + "如果觉得可以开始比赛了，请回复【飞花令开始】开始比赛！";
            }

            //检查是否发起者
            if (ps.CreateUid != uid)
            {
                return CQApi.CQCode_At(uid) + "不是创建者无法结束飞花令！\n本次飞花令发起者：" + CQApi.CQCode_At(ps.CreateUid);
            }

            //结束，统计分数
            for (int i = 0; i < ps.Players.Count - 1; i++)
            {
                for (int j = 0; j < ps.Players.Count - i - 1; j++)
                {
                    if (ps.Players[j].Score < ps.Players[j + 1].Score)
                    {
                        Player tmp = ps.Players[j];
                        ps.Players[j] = ps.Players[j + 1];
                        ps.Players[j + 1] = tmp;
                    }
                }
            }

            //输出消息
            string result = "本次飞花令积分排行";
            for (int i = 0; i < ps.Players.Count; i++)
            {
                result += $"\n{i + 1}:{CQApi.CQCode_At(ps.Players[i].Uid)}[{ps.Players[i].Score}]";
            }
            ps = new PlayStatu();
            return result;
        }

        public static string Menu()
        {
            return "——飞花令菜单——\n飞花令创建：创建比赛\n飞花令加入：参加比赛\n飞花令开始：开始比赛\n" +
                "飞花令回复：回答诗句\n飞花令跳过：跳过本轮\n飞花令结束：结束比赛\n飞花令退出：中途退出";
        }

        public static string Exit(long uid)
        {
            //检查是否创建
            if (ps.Start == false)
            {
                return CQApi.CQCode_At(uid) + "飞花令还未创建！回复【飞花令创建】创建一场比赛";
            }

            //检查是否开始
            if (ps.Flag == "")
            {
                return CQApi.CQCode_At(uid) + "飞花令还未开始！" + CQApi.CQCode_At(uid) + "如果觉得可以开始比赛了，请回复【飞花令开始】开始比赛！";
            }

            //检查是否参加
            bool isJoin = false;
            foreach (var tmp in ps.Players)
            {
                if (tmp.Uid == uid)
                {
                    isJoin = true;
                    break;
                }
            }
            if (isJoin == false)
            {
                return CQApi.CQCode_At(uid) + "你未参加本次飞花令比赛！";
            }

            if (ps.LastPlayerIndex != -1)
            {
                int index = ps.Players.IndexOf(ps.Players.Where(p => p.Uid == uid).FirstOrDefault());
                if (index <= ps.LastPlayerIndex)
                {
                    ps.LastPlayerIndex--;
                }
            }
            ps.Players.Remove(ps.Players.Where(p => p.Uid == uid).First());
            if (ps.Players.Count <= 1)
            {
                return "由于飞花令当前参赛人数过少，强制结束\n" + End(ps.CreateUid);
            }
            int nextIndex = ps.LastPlayerIndex + 1;
            if (nextIndex >= ps.Players.Count) { nextIndex = 0; }
            return $"退出飞花令成功！\n现在请{CQApi.CQCode_At(ps.Players[nextIndex].Uid)}回复\n回复格式为【飞花令回复 诗句】\n(PS:如果暂时想不出来，请回复【飞花令跳过】)";
        }
    }
}
