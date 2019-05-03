// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Services.Interfaces
{
    public interface IActionService
    {
        Dictionary<string, IEnumerable<string>> GetStatuses();
    }
}