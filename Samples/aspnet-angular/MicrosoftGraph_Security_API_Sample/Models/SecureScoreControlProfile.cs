// -----------------------------------------------------------------------
// <copyright file="SecureScoreControlProfile.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models
{
    /// <summary>
    /// The type Secure Score Control Profile.
    /// </summary>
    public class SecureScoreControlProfile
    {
        /// <summary>
        /// Gets or sets action type.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "actionType", Required = Newtonsoft.Json.Required.Default)]
        public string ActionType { get; set; }

        /// <summary>
        /// Gets or sets action url.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "actionUrl", Required = Newtonsoft.Json.Required.Default)]
        public string ActionUrl { get; set; }

        /// <summary>
        /// Gets or sets assigned to.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "assignedTo", Required = Newtonsoft.Json.Required.Default)]
        public string AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets azure tenant id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "azureTenantId", Required = Newtonsoft.Json.Required.Default)]
        public string AzureTenantId { get; set; }

        /// <summary>
        /// Gets or sets control category.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "controlCategory", Required = Newtonsoft.Json.Required.Default)]
        public string ControlCategory { get; set; }

        /// <summary>
        /// Gets or sets control name.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "controlName", Required = Newtonsoft.Json.Required.Default)]
        public string ControlName { get; set; }

        /// <summary>
        /// Gets or sets deprecated.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deprecated", Required = Newtonsoft.Json.Required.Default)]
        public bool? Deprecated { get; set; }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Newtonsoft.Json.Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets implementation cost.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "implementationCost", Required = Newtonsoft.Json.Required.Default)]
        public string ImplementationCost { get; set; }

        /// <summary>
        /// Gets or sets last modified date time.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "lastModifiedDateTime", Required = Newtonsoft.Json.Required.Default)]
        public DateTimeOffset? LastModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or sets max score.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "maxScore", Required = Newtonsoft.Json.Required.Default)]
        public double? MaxScore { get; set; }

        /// <summary>
        /// Gets or sets remediation.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "remediation", Required = Newtonsoft.Json.Required.Default)]
        public string Remediation { get; set; }

        /// <summary>
        /// Gets or sets remediation impact.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "remediationImpact", Required = Newtonsoft.Json.Required.Default)]
        public string RemediationImpact { get; set; }

        /// <summary>
        /// Gets or sets service.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "service", Required = Newtonsoft.Json.Required.Default)]
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets stack rank.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "stackRank", Required = Newtonsoft.Json.Required.Default)]
        public int? StackRank { get; set; }

        /// <summary>
        /// Gets or sets tenant note.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tenantNote", Required = Newtonsoft.Json.Required.Default)]
        public string TenantNote { get; set; }

        /// <summary>
        /// Gets or sets tenant set state.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tenantSetState", Required = Newtonsoft.Json.Required.Default)]
        public string TenantSetState { get; set; }

        /// <summary>
        /// Gets or sets threats.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "threats", Required = Newtonsoft.Json.Required.Default)]
        public IEnumerable<string> Threats { get; set; }

        /// <summary>
        /// Gets or sets tier.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tier", Required = Newtonsoft.Json.Required.Default)]
        public string Tier { get; set; }

        /// <summary>
        /// Gets or sets updated by.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "updatedBy", Required = Newtonsoft.Json.Required.Default)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets user impact.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userImpact", Required = Newtonsoft.Json.Required.Default)]
        public string UserImpact { get; set; }

        /// <summary>
        /// Gets or sets vendor information.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "vendorInformation", Required = Newtonsoft.Json.Required.Default)]
        public SecurityVendorInformation VendorInformation { get; set; }

        /// <summary>
        /// Gets or sets o data.type.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "@odata.type", Required = Newtonsoft.Json.Required.Default)]
        public string ODataType { get; set; }

        /// <summary>
        /// Gets or sets additional data.
        /// </summary>
        [JsonExtensionData(ReadData = true, WriteData = true)]
        public IDictionary<string, object> AdditionalData { get; set; }
    }

    /// <summary>
    /// The type Secure Score Control Profile result.
    /// </summary>
    public class SecureScoreControlProfileResult
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty(PropertyName = "@odata.nextLink")]
        public string ODataNextLink { get; set; }

        /// <summary>
        /// Gets or sets active user count.
        /// </summary>
        public List<SecureScoreControlProfile> Value { get; set; }
    }
}