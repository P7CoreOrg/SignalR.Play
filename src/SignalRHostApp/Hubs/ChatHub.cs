using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRHostApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private ConnectionMapping<string> _connectionMapping;

        public ChatHub(ConnectionMapping<string> connectionMapping)
        {
            _connectionMapping = connectionMapping;
        }
        public async Task SendMessage(string who, string message)
        {
            var connections = _connectionMapping.GetConnections(who);
            foreach (var connectionId in connections)
            {
               await Clients.Client(connectionId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
            }
           // await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            _connectionMapping.Add(Context.UserIdentifier,Context.ConnectionId);

            var name = Context.User.Identity.Name;
            var subject = (from claim in Context.User.Claims
                where claim.Type == ClaimTypes.NameIdentifier
                select claim.Value).FirstOrDefault();

                await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectionMapping.Remove(Context.UserIdentifier, Context.ConnectionId);
        }
    }
}
