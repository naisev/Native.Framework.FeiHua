using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

namespace cn.ylz1.feihua.Code.Event
{
    /// <summary>
	/// Type=1003 应用被启用, 事件实现
	/// </summary>
    public class Event_CqAppEnable : IAppEnable
    {

        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            if (File.Exists(e.CQApi.AppDirectory + "data.db")==false)
            {
                File.WriteAllBytes(e.CQApi.AppDirectory + "data.db", Properties.Resources.data);
            }
            PoemData.Init();
        }
    }
}
