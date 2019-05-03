// -----------------------------------------------------------------------
// <copyright file="RiskyUsersResponse.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class RiskyUsersResponse
    {
        public string Id { get; set; }
        public bool isDeleted { get; set; }
        public bool isGuest { get; set; }
        public bool isProcessing { get; set; }
        public string riskLevel { get; set; }
        public string riskState { get; set; }
        public string riskDetail { get; set; }
        public DateTimeOffset? riskLastUpdatedDateTime { get; set; }
        public string userDisplayName { get; set; }
        public string userPrincipalName { get; set; }
    }
}