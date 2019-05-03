// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Services.Interfaces
{
    public interface IDemoExample
    {
        bool UseMockData { get; set; }

        IEnumerable<string> GetProviders();

        IEnumerable<string> GetCategories();

        Task<IEnumerable<SecurityActionResponse>> GetSecurityActionsAsync();

        Task<IEnumerable<SecurityActionResponse>> AddSecurityActionsAsync(SecurityAction action);

        List<AlertHistoryState> GetAlertHistoryStates(Alert alert, UserAccountDevice assignedTo, UserAccountDevice user);

        List<AlertHistoryState> AddAlertHistoryState(string alertId, AlertHistoryState alertHistoryState);

        IEnumerable<Device> GetDevices();

        Dictionary<string, IEnumerable<string>> GetActionFilters();
    }
}