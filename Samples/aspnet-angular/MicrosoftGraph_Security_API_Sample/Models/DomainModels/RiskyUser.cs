// -----------------------------------------------------------------------
// <copyright file="RiskyUser.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class RiskyUser
    {
        [JsonProperty(PropertyName = "isDeleted")]
        public bool isDeleted { get; set; }

        [JsonProperty(PropertyName = "isGuest")]
        public bool isGuest { get; set; }

        [JsonProperty(PropertyName = "isProcessing")]
        public bool isProcessing { get; set; }

        [JsonProperty(PropertyName = "riskLastUpdatedDateTime")]
        public DateTimeOffset riskLastUpdatedDateTime { get; set; }

        [JsonProperty(PropertyName = "riskLevel")]
        public string riskLevel { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "riskState")]
        public string riskState { get; set; }

        [JsonProperty(PropertyName = "riskDetail")]
        public string riskDetail { get; set; }

        [JsonProperty(PropertyName = "userDisplayName")]
        public string userDisplayName { get; set; }

        [JsonProperty(PropertyName = "userPrincipalName")]
        public string userPrincipalName { get; set; }
    }

    public class RiskyUsersResult
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty(PropertyName = "@odata.nextLink")]
        public string ODataNextLink { get; set; }

        /// <summary>
        /// Gets or sets active user count.
        /// </summary>
        public List<RiskyUser> value { get; set; }
    }
}