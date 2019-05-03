// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Services.Interfaces
{
    public interface IGraphService
    {
        Dictionary<string, IEnumerable<string>> GetProvidersFilter(IEnumerable<string> providers, string name);

        string GraphUrlVersion { get; set; }

        string NotificationUri { get; set; }

        string GetAccessToken();

        Task<string> GetMyEmailAddressAsync();

        Task<UserAccountDevice> GetUserDetailsAsync(string principalName, bool populatePicture = false, bool populateManager = false, bool populateDevices = false, bool populateRiskyUser = false);

        Task<Device> GetDeviceDetailsAsync(string id);

        Task<Alert> UpdateAlertAsync(Alert alert, AlertUpdateRequest updateAlertModel);

        Task<Alert> GetAlertDetailsAsync(string alertId);

        Task<Tuple<IEnumerable<Alert>, string>> GetAlertsAsync(AlertFilterModel filters, Dictionary<string, string> odredByParams = null);

        Task<Tuple<Subscription, string>> SubscribeAsync(AlertFilterModel filters);

        Task<IGraphServiceSubscriptionsCollectionPage> GetListSubscriptionsAsync();

        Task<AlertStatisticModel> GetStatisticAsync(int topAlertAmount);

        Task<IEnumerable<SecureScore>> GetSecureScoresAsync(string queryParameter);

        Task<IEnumerable<SecurityActionResponse>> GetSecurityActionsAsync();

        Task<SecurityActionResponse> AddSecurityActionsAsync(SecurityAction action);

        Dictionary<string, IEnumerable<string>> GetCategoriesFilter(Tuple<IEnumerable<Alert>, string> alerts);

        Dictionary<string, IEnumerable<string>> GetCategoriesFilter(IEnumerable<string> categories);

        Dictionary<string, IEnumerable<string>> GetProvidersFilter(Tuple<IEnumerable<Alert>, string> alerts, string name = "Alert");

        Task<Dictionary<string, IEnumerable<string>>> GetUserPrincipalNameFilterAsync();

        Task<IEnumerable<SecureScoreControlProfileModel>> GetSecureScoreControlProfilesAsync();
    }
}