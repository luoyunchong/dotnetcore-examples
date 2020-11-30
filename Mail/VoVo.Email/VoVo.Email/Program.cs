using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace VoVo.Email
{

    class Program
    {
        static async Task Main(string[] args)
        {
            // 创建ServiceCollection
            var services = new ServiceCollection();

            ConfigureServices(services);

            // 创建ServiceProvider
            var serviceProvider = services.BuildServiceProvider();
            // app程序运行入口
            await serviceProvider.GetService<App>().Run(args);

            //SendEmail();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // 配置日志
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            // 创建 config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
            services.Configure<MailKitOptions>(configuration.GetSection("MailKitOptions"));
            // 添加 services:
            services.AddTransient<IEmailSender, EmailSender>();
            // 添加 app
            services.AddTransient<App>();
        }


        //DEMO
        static void SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("igeekfan", ""));
            message.To.Add(new MailboxAddress("Mrs. luoyunchong", ""));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"

            };


            using (var client = new SmtpClient())
            {
                //当我们使用Smtp.qq.com时，服务协议不安全。需要指定
                /*
                *MailKit.Security.SslHandshakeException: 'An error occurred while attempting to establish an SSL or TLS connection.

                    This usually means that the SSL certificate presented by the server is not trusted by the system for one or more of
                    the following reasons:

                    1. The server is using a self-signed certificate which cannot be verified.
                    2. The local system is missing a Root or Intermediate certificate needed to verify the server's certificate.
                    3. A Certificate Authority CRL server for one or more of the certificates in the chain is temporarily unavailable.
                    4. The certificate presented by the server is expired or invalid.
                    5. The set of SSL/TLS protocols supported by the client and server do not match.

                    See https://github.com/jstedfast/MailKit/blob/master/FAQ.md#SslHandshakeException for possible solutions.
                 *
                 *
            */
                //client.SslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Ssl2 | SslProtocols.Ssl3 | SslProtocols.Tls;
                //client.Connect("smtp.qq.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                //client.Authenticate("luoyunchong@foxmail.com", "nnuxredfstvcbegc");



                client.SslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Ssl2 | SslProtocols.Ssl3 | SslProtocols.Tls;

                client.Connect("smtp.163.com", 25, SecureSocketOptions.StartTls);
                client.Authenticate("", "");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
