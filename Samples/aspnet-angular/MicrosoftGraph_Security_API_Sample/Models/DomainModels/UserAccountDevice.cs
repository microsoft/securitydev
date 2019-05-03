// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class UserAccountDevice
    {
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Upn { get; set; }

        public string JobTitle { get; set; }

        public UserAccountDevice Manager { get; set; }

        public string OfficeLocation { get; set; }

        public string ContactVia { get; set; }

        public string Picture { get; set; }

        public string EmailRole { get; set; }

        public string RiskScore { get; set; }

        public string LogonId { get; set; }

        public RiskyUser RiskyUser { get; set; }

        public string DomainName { get; set; }

        public IEnumerable<Device> RegisteredDevices { get; set; }

        public IEnumerable<Device> OwnedDevices { get; set; }
    }
}