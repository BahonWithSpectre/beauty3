using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beauty3.Sign
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user)
        {
            await Clients.All.SendAsync("ReceiveMessage", user);
        }

        public async Task SendUser(string user)
        {
            await Clients.All.SendAsync("ReceiveUser", user);
        }
    }
}
