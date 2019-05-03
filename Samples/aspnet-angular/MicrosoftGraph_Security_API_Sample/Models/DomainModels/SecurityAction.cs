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
    public class SecurityAction
    {
        [JsonProperty(PropertyName = "actionReason")]
        public string ActionReason { get; set; }

        [JsonProperty(PropertyName = "appId")] public string AppId { get; set; }

        [JsonProperty(PropertyName = "azureTenantId")]
        public string AzureTenantId { get; set; }

        [JsonProperty(PropertyName = "clientContext")]
        public string ClientContext { get; set; }

        [JsonProperty(PropertyName = "completedDateTime")]
        public DateTimeOffset? CompletedDateTime { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public DateTimeOffset? CreatedDateTime { get; set; }

        [JsonProperty(PropertyName = "errorInfo")]
        public ResultInfo ErrorInfo { get; set; }

        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "lastActionDateTime")]
        public DateTimeOffset? LastActionDateTime { get; set; }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "parameters")]
        public List<Microsoft.Graph.KeyValuePair> Parameters { get; set; }

        [JsonProperty(PropertyName = "states")]
        public IEnumerable<SecurityActionState> States { get; set; }

        [JsonProperty(PropertyName = "status")]
        public OperationStatus Status { get; set; }

        [JsonProperty(PropertyName = "user")] public string User { get; set; }

        [JsonProperty(PropertyName = "vendorInformation")]
        public Microsoft.Graph.SecurityVendorInformation VendorInformation { get; set; }
    }

    public class ResultInfo
    {
        [JsonProperty(PropertyName = "code")] public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "subCode")]
        public string SubCode { get; set; }
    }

    public class SecurityActionResult
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty(PropertyName = "@odata.nextLink")]
        public string ODataNextLink { get; set; }

        /// <summary>
        /// Gets or sets active user count.
        /// </summary>
        public List<SecurityAction> Value { get; set; }
    }
}