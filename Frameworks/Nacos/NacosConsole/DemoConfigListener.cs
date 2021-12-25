using Nacos.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NacosConsole;

class DemoConfigListener : IListener
{
    public void ReceiveConfigInfo(string configInfo)
    {
        Console.WriteLine($"================收到配置变更信息了 ===》{configInfo}");
    }
}
