// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using System;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class ActionResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? LastActionDateTime { get; set; }
        public string Provider { get; set; }
        public IEnumerable<Microsoft.Graph.KeyValuePair> Parameters { get; set; }
        public IEnumerable<SecurityActionState> States { get; set; }
    }
}