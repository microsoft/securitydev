// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using System;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class SecurityActionResponse : SecurityActionBase
    {
        public string Id { get; set; }

        public DateTimeOffset? SubmittedDateTime { get; set; }

        public DateTimeOffset? StatusUpdateDateTime { get; set; }

        public Microsoft.Graph.SecurityVendorInformation SecurityVendorInformation { get; set; }

        public OperationStatus Status { get; set; }
    }
}