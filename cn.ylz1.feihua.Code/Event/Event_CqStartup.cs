using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp;

namespace cn.ylz1.feihua.Code.Event
{
    /// <summary>
	/// Type=1001 酷Q启动, 事件实现
	/// </summary>
    public class Event_CqStartup : ICQStartup
    {
        public void CQStartup(object sender, CQStartupEventArgs e)
        {
            Common.CQApi = e.CQApi;
            Common.CQLog = e.CQLog;
        }
    }
}
