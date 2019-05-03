// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class SecurityActionBase
    {
        public string Name { get; set; }

        public Target Target { get; set; }

        public Reason Reason { get; set; }
    }

    public class Reason
    {
        public string Comment { get; set; }
        public string AlertId { get; set; }
    }

    public class Target
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}