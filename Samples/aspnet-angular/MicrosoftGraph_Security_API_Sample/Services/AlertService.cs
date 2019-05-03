// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Services
{
    public class AlertService : IAlertService
    {
        public Dictionary<string, IEnumerable<string>> GetStatuses()
        {
            try
            {
                // Enum to List
                List<string> statuses = Enum.GetNames(typeof(Microsoft.Graph.AlertStatus)).ToList();

                return statuses.ToFilter("AlertStatuses", new List<string>() { "Unknown", "UnknownFutureValue" });
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, IEnumerable<string>> GetSeverities()
        {
            try
            {
                // Enum to List
                var dict = Enum.GetValues(typeof(Microsoft.Graph.AlertSeverity))
                    .Cast<Microsoft.Graph.AlertSeverity>()
                    .ToDictionary(t => (int)t, t => t.ToString());

                var severities = dict.OrderByDescending(x => x.Key).Select(x => x.Value).ToList();

                return severities.ToFilter("AlertSeverities", new List<string>() { "Unknown", "UnknownFutureValue" });
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, IEnumerable<string>> GetFeedbacks()
        {
            try
            {
                // Enum to List
                List<string> statuses = Enum.GetNames(typeof(Microsoft.Graph.AlertFeedback)).ToList();

                return statuses.ToFilter("AlertFeedbacks", new List<string>() { "UnknownFutureValue" });
            }
            catch
            {
                return null;
            }
        }
    }
}