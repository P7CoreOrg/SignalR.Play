using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace SignalRHostApp.Hubs
{
    public class SignalRUserProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var subject = (from claim in connection.User.Claims
                where claim.Type == ClaimTypes.NameIdentifier
                select claim.Value).FirstOrDefault();
            return subject;
        }
    }
}