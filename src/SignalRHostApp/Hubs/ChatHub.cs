using System.Collections.Generic;
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
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            var subject = (from claim in Context.User.Claims
                where claim.Type == ClaimTypes.NameIdentifier
                select claim.Value).FirstOrDefault();

                await base.OnConnectedAsync();
        }
    }
}
