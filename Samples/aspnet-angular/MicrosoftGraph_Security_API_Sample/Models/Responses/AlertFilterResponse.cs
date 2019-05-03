// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class AlertFilterResponse
    {
        public string Name { get; set; }

        public IEnumerable<string> Values { get; set; }
    }
}