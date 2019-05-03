// -----------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Services.Interfaces
{
    public interface INotificationService
    {
        void SendNotificationToClient(List<NotificationViewModel> messages);
    }
}