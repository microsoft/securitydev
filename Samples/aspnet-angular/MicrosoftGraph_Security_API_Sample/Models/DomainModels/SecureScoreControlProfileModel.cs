// -----------------------------------------------------------------------
// <copyright file="SecureScoreControlProfileModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class SecureScoreControlProfileModel
    {
        public string ControlCategory { get; set; }

        public int? Rank { get; set; }

        public string Title { get; set; }

        public double? MaxScore { get; set; }

        public string UserImpact { get; set; }

        public string ImplementationCost { get; set; }

        public DateTimeOffset? LastModifiedDateTime { get; set; }

        public string ActionUrl { get; set; }

        public bool? Deprecated { get; set; }

        public string Remediation { get; set; }

        public string RemediationImpact { get; set; }

        public string Service { get; set; }

        public string Tier { get; set; }

        public string AzureTenantId { get; set; }

        public string TenantSetState { get; set; }

        public string TenantNote { get; set; }

        public IEnumerable<string> Threats { get; set; }

        public IEnumerable<ControlStateUpdateModel> SecureStateUpdates { get; set; }
    }

    public class ControlStateUpdateModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "assignedTo", Required = Required.Default)]
        public string UpnAssignedTo { get; set; }

        public string DisplayNameAssignedTo { get; set; }

        public string State { get; set; }

        public string Comment { get; set; }

        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "updatedBy", Required = Required.Default)]
        public string UpnUpdatedBy { get; set; }

        public string DisplayNameUpdatedBy { get; set; }

        public string UpdatedDateTime { get; set; }

        public string AssignTo
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.DisplayNameAssignedTo)
                    ? this.DisplayNameAssignedTo
                    : !string.IsNullOrWhiteSpace(this.UpnAssignedTo)
                    ? this.UpnAssignedTo.Contains("@")
                    ? this.UpnAssignedTo.Split('@')[0]
                    : this.UpnAssignedTo
                    : string.Empty;
            }
        }

        public string UpdatedBy
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.DisplayNameUpdatedBy)
                    ? this.DisplayNameUpdatedBy
                    : !string.IsNullOrWhiteSpace(this.UpnUpdatedBy)
                    ? this.UpnUpdatedBy.Contains("@")
                    ? this.UpnUpdatedBy.Split('@')[0]
                    : this.UpnUpdatedBy
                    : string.Empty;
            }
        }
    }

    public class SecureScoreModel
    {
        public IEnumerable<AverageComparativeScoreModel> AverageComparativeScores { get; set; }

        public IEnumerable<ControlScoreModel> ControlScores { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }

        public double? CurrentScore { get; set; }

        public IEnumerable<string> EnabledServices { get; set; }

        public double? MaxScore { get; set; }

        public int LicensedUserCount { get; set; }

        public int ActiveUserCount { get; set; }

        public string Id { get; set; }
    }

    public class ControlScoreModel
    {
        public string ControlCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Score { get; set; }

        public string Count { get; set; }

        public string Total { get; set; }
    }

    public class AverageComparativeScoreModel
    {
        public string ComparedBy { get; set; }

        public string Value { get; set; }

        public double AverageScore { get; set; }
    }
}