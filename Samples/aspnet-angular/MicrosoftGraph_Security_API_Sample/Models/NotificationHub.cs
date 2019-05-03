// -----------------------------------------------------------------------
// <copyright file="NotificationHub.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Models
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}