// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class AlertHistoryState
    {
        [JsonProperty(PropertyName = "appId")]
        public string AppId { get; set; }

        [JsonProperty(PropertyName = "assignedTo")]
        public UserAccountDevice AssignedTo { get; set; }

        [JsonProperty(PropertyName = "feedback")]
        public AlertFeedback? Feedback { get; set; }

        [JsonProperty(PropertyName = "updatedDateTime")]
        public DateTimeOffset? UpdatedDateTime { get; set; }

        [JsonProperty(PropertyName = "user")]
        public UserAccountDevice User { get; set; }

        [JsonProperty(PropertyName = "status")]
        public AlertStatus? Status { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public IEnumerable<string> Comments { get; set; }
    }
}