// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Requests;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Extensions
{
    public static class AlertExtensions
    {
        public static AlertDetailsViewModel ToAlertDetailsViewModel(this Alert alert)
        {
            return new AlertDetailsViewModel
            {
                Id = alert.Id,
                Title = alert.Title,
                Description = alert.Description,
                CreatedDateTime = alert.CreatedDateTime,
                Status = alert.Status?.ToString() ?? string.Empty,
                Severity = alert.Severity.ToString(),
                Feedback = alert.Feedback?.ToString() ?? string.Empty,
                Vendor = alert?.VendorInformation,
                RecommendedActions = alert?.RecommendedActions,
                Comments = alert.Comments?.ToList(),
                Triggers = alert.Triggers,
                Users = alert.UserStates,
                Hosts = alert.HostStates,
                NetworkConnections = alert.NetworkConnections,
                Files = alert.FileStates,
                Processes = alert.Processes,
                RegistryKeyUpdates = alert.RegistryKeyStates,
                CloudAppStates = alert.CloudAppStates,
                DetectionIds = alert.DetectionIds,
                MalwareStates = alert.MalwareStates,
                SourceMaterials = alert.SourceMaterials,
                Tags = alert.Tags,
                VulnerabilityStates = alert.VulnerabilityStates
            };
        }

        public static AlertHistoryState ToAlertHistoryState(this AlertUpdateRequest alertUpdateRequest, UserAccountDevice assignedTo, UserAccountDevice user)
        {
            if (!Enum.TryParse<AlertStatus>(alertUpdateRequest.Status, true, out var status))
            {
                throw new ArgumentOutOfRangeException(nameof(alertUpdateRequest.Status));
            }

            if (!Enum.TryParse<AlertFeedback>(alertUpdateRequest.Feedback, true, out var feedback))
            {
                throw new ArgumentOutOfRangeException(nameof(alertUpdateRequest.Feedback));
            }

            return new AlertHistoryState()
            {
                Status = status,
                Feedback = feedback,
                Comments = alertUpdateRequest.Comments,
                AssignedTo = assignedTo,
                User = user,
            };
        }

        public static Dictionary<string, IEnumerable<string>> ToFilter(this List<string> values, string name, IEnumerable<string> noIncludes)
        {
            if (values.Count() > 1)
            {
                if (noIncludes != null && noIncludes.Count() > 0)
                {
                    foreach (var noInclude in noIncludes)
                    {
                        var index = values.IndexOf(noInclude);
                        if (index >= 0)
                        {
                            values.RemoveAt(index);
                        }
                    }
                }
            }

            return new Dictionary<string, IEnumerable<string>>
            {
                { name, values }
            };
        }

        public static AlertStatisticResponse ToAlertStatisticResponse(this AlertStatisticModel alertStatisticModel)
        {
            AlertStatisticResponse alertStatisticResponse = new AlertStatisticResponse();

            if (alertStatisticModel?.SecureScore != null)
            {
                alertStatisticResponse.SecureScore = new SecureScoreStatistic(alertStatisticModel.SecureScore.Current, alertStatisticModel.SecureScore.Max, alertStatisticModel.SecureScore.ComparativeScores);
            }
            else
            {
                alertStatisticResponse.SecureScore = new SecureScoreStatistic();
            }

            alertStatisticResponse.AlertsByStatus = new List<AlertsByStatus>();

            if (alertStatisticModel?.NewAlerts != null)
            {
                var alertsByStatuses = new AlertsByStatus("New");

                foreach (var alert in alertStatisticModel?.NewAlerts)
                {
                    alertsByStatuses.Values.First(val => val.Name.Equals(alert.Key)).Amount = alert.Value;
                }

                alertStatisticResponse.AlertsByStatus.Add(alertsByStatuses);
            }

            if (alertStatisticModel?.InProgressAlerts != null)
            {
                var alertsByStatuses = new AlertsByStatus("InProgress");

                foreach (var alert in alertStatisticModel?.InProgressAlerts)
                {
                    alertsByStatuses.Values.First(val => val.Name.Equals(alert.Key)).Amount = alert.Value;
                }

                alertStatisticResponse.AlertsByStatus.Add(alertsByStatuses);
            }

            if (alertStatisticModel?.ResolvedAlerts != null)
            {
                var alertsByStatuses = new AlertsByStatus("Resolved");

                foreach (var alert in alertStatisticModel?.ResolvedAlerts)
                {
                    alertsByStatuses.Values.First(val => val.Name.Equals(alert.Key)).Amount = alert.Value;
                }

                alertStatisticResponse.AlertsByStatus.Add(alertsByStatuses);
            }

            alertStatisticResponse.AlertsByEntity = new AlertsByEntity();

            if (alertStatisticModel?.HostsWithTheMostAlerts != null)
            {
                alertStatisticResponse.AlertsByEntity.HostsWithTheMostAlerts = new List<Info>();
                foreach (var value in alertStatisticModel?.HostsWithTheMostAlerts)
                {
                    Info info = new Info(new Specification() { Title = value.Key.Key, FilterValue = value.Key.Value });
                    foreach (var valVal in value.Value)
                    {
                        info.Values.First(val => val.Name.Equals(valVal.Key.ToString())).Amount = valVal.Value;
                    }
                    alertStatisticResponse.AlertsByEntity.HostsWithTheMostAlerts.Add(info);
                }
            }

            if (alertStatisticModel?.IPWithTheMostAlerts != null)
            {
                alertStatisticResponse.AlertsByEntity.IPWithTheMostAlerts = new List<Info>();
                foreach (var value in alertStatisticModel?.IPWithTheMostAlerts)
                {
                    Info info = new Info(new Specification() { Title = value.Key.Value, FilterValue = value.Key.Value });
                    foreach (var valVal in value.Value)
                    {
                        info.Values.First(val => val.Name.Equals(valVal.Key.ToString())).Amount = valVal.Value;
                    }
                    alertStatisticResponse.AlertsByEntity.IPWithTheMostAlerts.Add(info);
                }
            }

            if (alertStatisticModel?.UsersWithTheMostAlerts != null)
            {
                alertStatisticResponse.AlertsByEntity.UsersWithTheMostAlerts = new List<Info>();
                foreach (var value in alertStatisticModel?.UsersWithTheMostAlerts)
                {
                    Info info = new Info(new Specification() { Title = value.Key.Key, FilterValue = value.Key.Value });
                    foreach (var valVal in value.Value)
                    {
                        info.Values.First(val => val.Name.Equals(valVal.Key.ToString())).Amount = valVal.Value;
                    }
                    alertStatisticResponse.AlertsByEntity.UsersWithTheMostAlerts.Add(info);
                }
            }

            if (alertStatisticModel?.DomainsWithTheMostAlerts != null)
            {
                alertStatisticResponse.AlertsByEntity.DomainsWithTheMostAlerts = new List<Info>();
                foreach (var value in alertStatisticModel?.DomainsWithTheMostAlerts)
                {
                    Info info = new Info(new Specification() { Title = value.Key.Value, FilterValue = value.Key.Value });
                    foreach (var valVal in value.Value)
                    {
                        info.Values.First(val => val.Name.Equals(valVal.Key.ToString())).Amount = valVal.Value;
                    }
                    alertStatisticResponse.AlertsByEntity.DomainsWithTheMostAlerts.Add(info);
                }
            }

            alertStatisticResponse.AlertsByProvider = new List<Info>();

            if (alertStatisticModel?.ProvidersWithTheMostAlerts != null)
            {
                foreach (var value in alertStatisticModel?.ProvidersWithTheMostAlerts)
                {
                    Info info = new Info(new Specification() { Title = value.Key.Key, FilterValue = value.Key.Value });
                    foreach (var valVal in value.Value)
                    {
                        info.Values.First(val => val.Name.Equals(valVal.Key.ToString())).Amount = valVal.Value;
                    }
                    alertStatisticResponse.AlertsByProvider.Add(info);
                }
            }

            return alertStatisticResponse;
        }
    }
}