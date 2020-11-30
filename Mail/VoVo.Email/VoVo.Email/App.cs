using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace VoVo.Email
{
    public class App
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<App> _logger;
        public App(ILogger<App> logger, IEmailSender emailSender)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailSender = emailSender;
        }
        public async Task Run(string[] args)
        {
            _logger.LogInformation("Starting...");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("igeekfan", "igeekfan@163.com"));
            message.To.Add(new MailboxAddress("Mrs. luoyunchong", "luoyunchong@foxmail.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"

            };
            await _emailSender.SendAsync(message);

            _logger.LogInformation("Finished!");
            await Task.CompletedTask;
        }
    }
}
