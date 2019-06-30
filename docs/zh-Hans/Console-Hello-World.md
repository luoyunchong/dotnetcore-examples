
# 创建简单Hello World
- [C# 和 Visual Studio Code 入门教程](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/with-visual-studio-code)
- [vscode调试运行c#详细操作过程](https://blog.csdn.net/qq_40346899/article/details/80955788)
- **[使用Visual Studio Code开发.NET Core看这篇就够了](https://blog.csdn.net/qin_yu_2010/article/details/83978244)** 强烈推荐。

创建一个hello-word的console，会输出Hello World!

~~~bash
mkdir console-hello-world
cd console-hello-world
dotnet new console
dotnet run
~~~

console-hello-world.csproj

**OutputType** 标记指定我们要生成的可执行文件，即控制台应用程序。

**TargetFramework** 标记指定要定位的 .NET 实现代码。 在高级方案中，可以指定多个目标框架，并在单个操作中生成所有目标框架。

~~~xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.0</TargetFramework>
    <RootNamespace>console_hello_world</RootNamespace>
  </PropertyGroup>

</Project>

~~~

在 console-hello-world/bin/Debug/netcoreapp3.0中生成了console-hello-world.dll

~~~PowerShell
cd console-hello-world #要先在console-hello-world目录中
dotnet bin/Debug/netcoreapp3.0/console-hello-world.dll
Hello World
~~~

修改main函数

~~~c#
using System;

namespace console_hello_world
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine($"Hello {args[0]}!");
            }
            else
            {
                Console.WriteLine("Hello!");
            }
        }
    }
}
~~~

~~~PowerShell
$ dotnet run -- John
Hello John!
~~~