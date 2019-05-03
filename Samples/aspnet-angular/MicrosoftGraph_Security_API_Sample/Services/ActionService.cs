// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Services
{
    public class ActionService : IActionService
    {
        public Dictionary<string, IEnumerable<string>> GetStatuses()
        {
            // Enum to List
            List<string> statuses = Enum.GetNames(typeof(Microsoft.Graph.ActionState)).ToList();

            return statuses.ToFilter("ActionStatuses", new List<string>() { "None", "NotSupported" });
        }
    }
}