using cn.ylz1.feihua.Code.Event;
using cn.ylz1.feihua.UI;
using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Native.Core
{
	/// <summary>
	/// 酷Q应用主入口类
	/// </summary>
	public class CQMain
	{
		/// <summary>
		/// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
		/// </summary>
		/// <param name="container">用于注册的 IOC 容器 </param>
		public static void Register (IUnityContainer unityContainer)
		{
            // 注入 Type=1001 的回调
            unityContainer.RegisterType<ICQStartup, Event_CqStartup>("酷Q启动事件");
            // 注入 Type=1002 的回调
            unityContainer.RegisterType<ICQExit, Event_CqExit>("酷Q关闭事件");
            // 注入 Type=1003 的回调
            unityContainer.RegisterType<IAppEnable, Event_CqAppEnable>("应用已被启用");
            // 注入 Type=1004 的回调
            unityContainer.RegisterType<IAppDisable, Event_CqAppDisable>("应用将被停用");
            //注入群消息回调
            unityContainer.RegisterType<IGroupMessage, Event_GroupMsg>("群消息处理");
			//注入菜单回调
			unityContainer.RegisterType<IMenuCall, Menu_OpenWindow>("飞花令设置");
		}
	}
}
