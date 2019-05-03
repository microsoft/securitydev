// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class AlertsResponse
    {
        public AlertsResponse(IEnumerable<AlertResultItemModel> alerts, ResultQueriesViewModel queries)
        {
            Alerts = alerts;
            Queries = queries;
        }

        public IEnumerable<AlertResultItemModel> Alerts { get; set; }

        public ResultQueriesViewModel Queries { get; set; }
    }
}