// -----------------------------------------------------------------------
// <copyright file="SecureScore.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MicrosoftGraph_Security_API_Sample.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type Secure Score.
    /// </summary>
    public class SecureScore
    {
        /// <summary>
        /// Gets or sets active user count.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "activeUserCount", Required = Newtonsoft.Json.Required.Default)]
        public int? ActiveUserCount { get; set; }

        /// <summary>
        /// Gets or sets average comparative scores.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "averageComparativeScores", Required = Newtonsoft.Json.Required.Default)]
        public IEnumerable<AverageComparativeScore> AverageComparativeScores { get; set; }

        /// <summary>
        /// Gets or sets azure tenant id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "azureTenantId", Required = Newtonsoft.Json.Required.Default)]
        public string AzureTenantId { get; set; }

        /// <summary>
        /// Gets or sets control scores.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "controlScores", Required = Newtonsoft.Json.Required.Default)]
        public IEnumerable<ControlScore> ControlScores { get; set; }

        /// <summary>
        /// Gets or sets created date time.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "createdDateTime", Required = Newtonsoft.Json.Required.Default)]
        public DateTimeOffset? CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets current score.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "currentScore", Required = Newtonsoft.Json.Required.Default)]
        public double? CurrentScore { get; set; }

        /// <summary>
        /// Gets or sets enabled services.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enabledServices", Required = Newtonsoft.Json.Required.Default)]
        public IEnumerable<string> EnabledServices { get; set; }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Newtonsoft.Json.Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets licensed user count.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "licensedUserCount", Required = Newtonsoft.Json.Required.Default)]
        public int? LicensedUserCount { get; set; }

        /// <summary>
        /// Gets or sets max score.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "maxScore", Required = Newtonsoft.Json.Required.Default)]
        public double? MaxScore { get; set; }

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

    public class SecureScoreResult
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty(PropertyName = "@odata.nextLink")]
        public string ODataNextLink { get; set; }

        /// <summary>
        /// Gets or sets active user count.
        /// </summary>
        public List<SecureScore> Value { get; set; }
    }

    public class SecurityVendorInformation
    {
        /// <summary>
        /// Gets or sets provider
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "provider", Required = Required.Default)]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets provider version
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "providerVersion", Required = Required.Default)]
        public string ProviderVersion { get; set; }

        /// <summary>
        /// Gets or sets sub provider
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "subProvider", Required = Required.Default)]
        public string SubProvider { get; set; }

        /// <summary>
        /// Gets or sets vendor
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "vendor", Required = Required.Default)]
        public string Vendor { get; set; }

        /// <summary>
        /// Gets or sets additional data
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public IDictionary<string, object> AdditionalData { get; set; }
    }

    public class ControlScore
    {
        /// <summary>
        /// Gets or sets controlCategory.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "controlCategory", Required = Newtonsoft.Json.Required.Default)]
        public string ControlCategory { get; set; }

        /// <summary>
        /// Gets or sets controlName.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "controlName", Required = Newtonsoft.Json.Required.Default)]
        public string ControlName { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Newtonsoft.Json.Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "score", Required = Newtonsoft.Json.Required.Default)]
        public double? Score { get; set; }

        /// <summary>
        /// Gets or sets additional data.
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public IDictionary<string, object> AdditionalData { get; set; }
    }

    public class AverageComparativeScore
    {
        /// <summary>
        /// Gets or sets averageScore.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "averageScore", Required = Newtonsoft.Json.Required.Default)]
        public double? AverageScore { get; set; }

        /// <summary>
        /// Gets or sets basis.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "basis", Required = Newtonsoft.Json.Required.Default)]
        public string Basis { get; set; }

        /// <summary>
        /// Gets or sets additional data.
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}