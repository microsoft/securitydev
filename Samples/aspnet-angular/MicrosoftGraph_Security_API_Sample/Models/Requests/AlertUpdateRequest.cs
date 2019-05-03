// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Requests
{
    public class AlertUpdateRequest
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "userUpn")]
        public string UserUpn { get; set; }

        [JsonProperty(PropertyName = "feedback")]
        public string Feedback { get; set; }

        [JsonProperty(PropertyName = "assignedTo")]
        public string AssignedTo { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public IEnumerable<string> Comments { get; set; }
    }
}