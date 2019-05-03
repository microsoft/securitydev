// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using System.Collections.Generic;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class SubscriptionResponse
    {
        public SubscriptionResponse()
        {
        }

        public SubscriptionResponse(IEnumerable<SubscriptionResultModel> subscriptions, ResultQueriesViewModel queries)
        {
            Subscriptions = subscriptions;
            Queries = queries;
        }

        public IEnumerable<SubscriptionResultModel> Subscriptions { get; set; }

        public ResultQueriesViewModel Queries { get; set; }
    }
}