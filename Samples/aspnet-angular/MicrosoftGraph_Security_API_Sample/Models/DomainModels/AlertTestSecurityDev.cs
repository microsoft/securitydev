// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class AlertTestSecurityDev : Microsoft.Graph.Alert
    {
        [JsonProperty(PropertyName = "historyStates")]
        public IEnumerable<AlertHistoryState> HistoryStates { get; set; }
    }
}