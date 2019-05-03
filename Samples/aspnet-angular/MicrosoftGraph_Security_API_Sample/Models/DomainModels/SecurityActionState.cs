// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using Newtonsoft.Json;
using System;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class SecurityActionState
    {
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "appId")]
        public string AppId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public OperationStatus Status { get; set; }

        [JsonProperty(PropertyName = "updatedDateTime")]
        public DateTimeOffset UpdatedDateTime { get; set; }
    }
}