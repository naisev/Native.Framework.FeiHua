using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

namespace cn.ylz1.feihua.Code.Event
{
    /// <summary>
	/// Type=1002 酷Q退出, 事件实现
	/// </summary>
    public class Event_CqExit : ICQExit
    {
        public void CQExit(object sender, CQExitEventArgs e)
        {
        }
    }
}
