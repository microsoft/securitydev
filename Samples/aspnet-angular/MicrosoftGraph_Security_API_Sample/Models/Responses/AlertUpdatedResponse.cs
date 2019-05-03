// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.ViewModels;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class AlertUpdatedResponse
    {
        public AlertUpdatedResponse()
        {
        }

        public AlertUpdatedResponse(AlertDetailsViewModel alert, ResultQueriesViewModel queries)
        {
            Alert = alert;
            Queries = queries;
        }

        public AlertDetailsViewModel Alert { get; set; }

        public ResultQueriesViewModel Queries { get; set; }
    }
}