using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignlarDemo
{
    public class MessageHub : Hub<IChatClient>
    {
        public MessageHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            string userId=Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //将同一个人的连接ID绑定到同一个分组，推送时就推送给这个分组
            await Groups.AddToGroupAsync(Context.ConnectionId, "userId");

        }

        public async Task SendMessageAsync(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);

            await Clients.Client(Context.ConnectionId).ReceiveMessage(DateTime.Now.ToString());

        }

        [HubMethodName("SendMessageToCaller-HubMethodName")]
        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.ReceiveMessage(message);
        }
        public Task SendMessageToUser(string user,string message)
        {
            return Clients.User(user).ReceiveMessage(message);
        }
        public Task SendMessageToGroup(string message)
        {
            return Clients.Group("SignalR Users").ReceiveMessage(message);
        }
    }

    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);

        Task Notify(string message);
    }

}