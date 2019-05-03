// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.ViewModels;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class AlertDetailsResponse
    {
        public AlertDetailsResponse()
        {
        }

        public AlertDetailsResponse(AlertDetailsViewModel alertDetails, ResultQueriesViewModel queries)
        {
            AlertDetails = alertDetails;
            Queries = queries;
        }

        public AlertDetailsViewModel AlertDetails { get; set; }

        public ResultQueriesViewModel Queries { get; set; }
    }
}