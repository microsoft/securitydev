// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Services
{
    public class NotificationService : INotificationService
    {
        private IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void SendNotificationToClient(List<NotificationViewModel> messages)
        {
            if (_hubContext != null)
            {
                _hubContext.Clients.All.SendAsync("Send", messages);
            }
        }
    }
}