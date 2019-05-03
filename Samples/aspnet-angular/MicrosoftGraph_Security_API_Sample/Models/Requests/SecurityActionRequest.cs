// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.DomainModels;

namespace MicrosoftGraph_Security_API_Sample.Models.Requests
{
    public class SecurityActionRequest : SecurityActionBase
    {
        public string User { get; set; }

        public string Vendor { get; set; }

        public string Provider { get; set; }
    }
}