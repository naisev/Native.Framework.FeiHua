using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using System.IO;
using System.Collections.Generic;

namespace cn.ylz1.feihua.Code.Event
{
    public class Event_GroupMsg : IGroupMessage
    {
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            if (e.Message.Text == "飞花令创建")
            {
                e.FromGroup.SendGroupMessage(CallPlay.Create(e.FromQQ.Id));
            }
            else if (e.Message.Text == "飞花令开始")
            {
                e.FromGroup.SendGroupMessage(CallPlay.Start(e.FromQQ.Id));
            }
            else if (e.Message.Text == "飞花令加入")
            {
                e.FromGroup.SendGroupMessage(CallPlay.Join(e.FromQQ.Id));
            }
            else if (e.Message.Text == "飞花令跳过")
            {
                e.FromGroup.SendGroupMessage(CallPlay.Leap(e.FromQQ.Id));
            }
            else if (e.Message.Text == "飞花令结束")
            {
                e.FromGroup.SendGroupMessage(CallPlay.End(e.FromQQ.Id));
            }
            else if (e.Message.Text.StartsWith("飞花令回复"))
            {
                e.FromGroup.SendGroupMessage(CallPlay.Reply(e.Message.Text, e.FromQQ.Id));
            }
            else if (e.Message.Text == "飞花令")
            {
                e.FromGroup.SendGroupMessage(CallPlay.Menu());
            }
        }
    }
}