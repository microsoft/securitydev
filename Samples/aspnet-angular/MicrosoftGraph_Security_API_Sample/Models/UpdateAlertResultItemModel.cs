// -----------------------------------------------------------------------
// <copyright file="UpdateAlertResultItemModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MicrosoftGraph_Security_API_Sample.Models
{
    public class UpdateAlertResultItemModel
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string Severity { get; set; }

        public string Status { get; set; }

        public string Feedback { get; set; }

        public string Provider { get; set; }

        public string AssignedTo { get; set; }

        public string Comments { get; set; }
    }
}